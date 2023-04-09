using Core.Entities;
using LinqKit;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        private readonly DbSet<TEntity> _dbSet;

        public EfEntityRepositoryBase(TContext dbContext)
        {
            _dbSet = dbContext.Set<TEntity>();
        }

        public int Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                return context.SaveChanges();
            }
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            using (var context = new TContext())
            {
                await context.Set<TEntity>().AddAsync(entity);
                return entity;
            }
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            using (var context = new TContext())
            {
                await context.Set<TEntity>().AddRangeAsync(entities);
            }
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            using (var context = new TContext())
            {
                return await context.Set<TEntity>().AnyAsync(predicate);
            }
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            using (var context = new TContext())
            {
                return await (predicate == null ? context.Set<TEntity>().CountAsync() : context.Set<TEntity>().CountAsync(predicate));
            }
        }

        public void Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public async Task DeleteAsync(TEntity entity)
        {
            using (var context = new TContext())
            {
                await Task.Run(() =>
                {
                    context.Set<TEntity>().Remove(entity);
                });
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
           {
                return filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public async Task<IList<TEntity>> GetListAsync(IList<Expression<Func<TEntity, bool>>> predicates, IList<Expression<Func<TEntity, object>>> includeProperties)
        {
            using (var context = new TContext())
            {
                IQueryable<TEntity> query = context.Set<TEntity>();
                if (predicates != null && predicates.Any())
                {
                    foreach (var predicate in predicates)
                    {
                        query = query.Where(predicate);
                    }
                }
                if (includeProperties != null && includeProperties.Any())
                {
                    foreach (var includeProperty in includeProperties)
                    {
                        query = query.Include(includeProperty);
                    }
                }
                return await query.AsNoTracking().ToListAsync();
            }
        }

        public void Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            using (var context = new TContext())
            {
                await Task.Run(() =>
                {
                    context.Set<TEntity>().Update(entity);
                });
                return entity;
            }
        }

        public async Task<IList<TEntity>> FromSqlRawAsync(string sql, params object[] parameters)
        {
            using (var context = new TContext())
            {
                return await context.Set<TEntity>().FromSqlRaw(sql, parameters).ToListAsync();
            }
        }

        /// <summary>
        /// dbSet<typeparamref name="TEntity"/> nesnesini kullanarak sayfalama islemi yapar.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="skipCount">atlama yapilacak kayit sayisi</param>
        /// <param name="maxResultCount">sayfada gozukecek maksimum kayit sayisi</param>
        /// <param name="predicate">filtreleme yapmak icin</param>
        /// <param name="orderBy">siralama yapmak icin</param>
        /// <param name="isAscending">siralamanin yonunun belirtmek icin</param>
        /// <param name="includeProperties">ilgili tablolarin eklenmesini saglar</param>
        /// <returns>List<typeparamref name="TEntity"/></returns>
        public async Task<List<TEntity>> GetPagedList<TKey>(int skipCount, int maxResultCount, Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, TKey>> orderBy = null, bool isAscending = true, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = GetQueryable(includeProperties);
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (orderBy != null)
            {
                query = isAscending ? query.OrderBy(orderBy)
                                    : query.OrderByDescending(orderBy);
            }
            query = query.Skip(skipCount).Take(maxResultCount);//sayfa basina dusen item sayisi.
            //toplam sayfa sayisini bulma eksik, onu yap
            return await query.ToListAsync();
        }

        public async Task<List<TEntity>> GetPagedViewList<TKey>(int skipCount, int maxResultCount, Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, TKey>> orderBy = null, bool isAscending = true)
        {
            var query = GetQueryable();
            if (predicate != null)
            {
                query = query.Where(predicate); //filtreleme icin
            }
            if (orderBy != null)
            {
                query = isAscending ? query.OrderBy(orderBy)
                                    : query.OrderByDescending(orderBy);
            }
            query = query.Skip(skipCount).Take(maxResultCount);//sayfa basina dusen item sayisi.
            //toplam sayfa sayisini bulma eksik, onu yap
            return await query.ToListAsync();
        }

        /// <summary>
        /// AddDetails metodu ile hazirlanmis olan sorguyu geri dondurmemizi saglar. veri yukleme performansini arttirmak icin yapildi. onceden hazirlanmis sorgunun uzerine ilgili tablolarin eklenmesini(include) saglar.
        /// </summary>
        /// <param name="propertySelectors"></param>
        /// <returns></returns>
        public async Task<IQueryable<TEntity>> DetailsAsync(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            return await Task.FromResult(AddDetails(GetQueryable(), propertySelectors));
            //var query = AddDetails(GetQueryable(), propertySelectors);
            //return (IQueryable<TEntity>)await query.ToListAsync();
        }

        /// <summary>
        /// Yapilan IQueryable<typeparamref name="TEntity"/> sorgusuna istenen tablolarin(include) eklenmesini saglar. Onceden hazirlanmis bir IQueryable sorgusuna tablolarin include edilmesini saglar.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="propertySelectors"></param>
        /// <returns></returns>
        public IQueryable<TEntity> AddDetails(IQueryable<TEntity> query, Expression<Func<TEntity, object>>[] propertySelectors)
        {
            if (propertySelectors == null || !propertySelectors.Any())
            {
                return query;
            }

            foreach (var propertySelector in propertySelectors)
            {
                query = query.Include(propertySelector);
            }
            return query;
        }

        public async Task<IQueryable<TEntity>> GetQueryableAsync()
        {
            return await Task.FromResult(GetQueryable());
        }

        public async Task<DbSet<TEntity>> GetDbAsync()
        {
            return await Task.FromResult(_dbSet);

        }

        /// <summary>
        /// Tabloya ait sorgulari yapabilmemize olanak saglar. include parametresini verirsek, yapilan sorguya verilen tablolari(inlude) eklenmesini saglar.
        /// </summary>
        /// <param name="includeProperties"></param>
        public IQueryable<TEntity> GetQueryable(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            #region eski
            //using (var context = new TContext())
            //{
            //    IQueryable<TEntity> query = context.Set<TEntity>();
            //    if (includeProperties != null && includeProperties.Any())
            //    {
            //        query = AddDetails(query, includeProperties);
            //    }
            //    return query;
            //}
            #endregion

            IQueryable<TEntity> query = _dbSet;

            if (includeProperties != null && includeProperties.Any())
            {
                query = AddDetails(query, includeProperties);
            }

            return query;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            using (var context = new TContext())
            {
                return await context.Set<TEntity>().FindAsync(id);
            }
        }

        public async Task<TEntity> GetByGuid(Guid guid)
        {
            using (var context = new TContext())
            {
                return await context.Set<TEntity>().FindAsync(guid);
            }
        }

        public async Task<float> SumAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            throw new NotImplementedException();

        }
        public async Task<float> AvgAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }

        public float GetValue(TEntity entity)
        {
            throw new NotImplementedException();

        }

        public async Task<TEntity> GetValueAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TEntity>> GetListAsync2(IQueryable<TEntity> query)
        {
            return await query.ToListAsync();
        }

        public IQueryable<TEntity> Filter(IQueryable<TEntity> query, Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return query;
        }
        public async Task<List<SelectListItem>> DDl(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, string>> orderBy, Expression<Func<TEntity, SelectListItem>> selector)
        {
            var ddl = GetQueryable().Where(filter).OrderBy(orderBy).Select(selector);
            return await ddl.ToListAsync();
        }
        //gelen datalari filtreleyen ayri bir metot yaz
    }
}