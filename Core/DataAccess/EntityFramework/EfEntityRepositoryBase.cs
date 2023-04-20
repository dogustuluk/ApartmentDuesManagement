﻿using Core.Entities;
using LinqKit;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Core.DataAccess.Pagination;

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


        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                return filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public async Task<List<TEntity>> GetAllAsync(List<Expression<Func<TEntity, bool>>> predicates, List<Expression<Func<TEntity, object>>> includeProperties)
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

        public async Task<List<TEntity>> GetPagedViewList(int skipCount, int maxResultCount, Expression<Func<TEntity, bool>> predicate = null, string? orderBy = null, bool isAscending = true)
        {
            var query = GetQueryable();
            if (predicate != null)
            {
                query = query.Where(predicate); //filtreleme icin
            }
            //if (orderBy != null)
            //{
            //    query = query.OrderBy(orderBy);
            //}

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

        public async Task<DbSet<TEntity>> GetDbAsync()
        {
            return await Task.FromResult(_dbSet);

        }

        //kaldir
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
        public TEntity? GetById(int id)
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().Find(id);
            }

        }

        public async Task<TEntity> GetByGuidAsync(Guid guid)
        {
            using (var context = new TContext())
            {
                return await context.Set<TEntity>().FindAsync(guid);
            }
        }
        public TEntity? GetByGuid(Guid guid)
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().Find(guid);
            }
        }

        public async Task<PaginatedList<TEntity>> GetDataPagedAsync(Expression<Func<TEntity, bool>> predicate, int PageIndex, int take, string orderBy)
        {
            using (var context = new TContext())
            {
                var query = context.Set<TEntity>().AsQueryable();

                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                var count = await query.CountAsync();

                if (!string.IsNullOrEmpty(orderBy)) //null kontrolune gerek yok
                {
                    // query = query.OrderBy(orderBy); //linq.dynamic kutuphanesi ile string order by alinabilir.
                    if (int.TryParse(orderBy, out int orderByInt))
                    {
                        query = query.OrderBy(x=> orderByInt);
                    }
                    else
                    {
                        query = query.OrderBy(orderBy);
                    }
                }

                var items = await query.Skip((PageIndex - 1) * take).Take(take).ToListAsync();

                return new PaginatedList<TEntity>(items, count, PageIndex, take);
            }
        }

        public IEnumerable<TEntity> GetData(Expression<Func<TEntity, bool>> predicate, int take, string OrderBy)
        {
            using (var context = new TContext())
            {
                var query = context.Set<TEntity>().AsQueryable();

                if (predicate != null)
                {
                    query = query.Where(predicate);
                }
                if (!string.IsNullOrEmpty(OrderBy))
                {
                    query = query.OrderBy(OrderBy);
                }
                if (take > 0)
                {
                    query = query.Take(take);
                }
                return query.ToList();
            }
        }

        public async Task<List<TEntity>> GetDataAsync(Expression<Func<TEntity, bool>> predicate, int take, string OrderBy)
        {
            using (var context = new TContext())
            {
                var query = context.Set<TEntity>().AsQueryable();

                if (predicate != null)
                {
                    query = query.Where(predicate);
                }
                if (!string.IsNullOrEmpty(OrderBy))
                {
                    query = query.OrderBy(OrderBy);
                }
                if (take > 0)
                {
                    query = query.Take(take);
                }
                return await query.ToListAsync();
            }
        }

        public IEnumerable<TEntity> GetDataSql(string sql, int pageIndex, int take, string orderBy)
        {
            using (var context = new TContext())
            {
                if (string.IsNullOrEmpty(sql))
                {
                    throw new ArgumentNullException(nameof(sql));
                }
                var query = context.Set<TEntity>().FromSqlRaw(sql);
                if (!string.IsNullOrEmpty(orderBy))
                {
                    query = query.OrderBy(orderBy);
                }
                query = query.Skip((pageIndex - 1) * take).Take(take);
                return query.ToList();
            }
        }

        public async Task<List<TEntity>> GetDataSqlAsync(string sql, int pageIndex, int take, string orderBy)
        {
            using (var context = new TContext())
            {
                if (string.IsNullOrEmpty(sql))
                {
                    throw new ArgumentNullException(nameof(sql));
                }
                var query = context.Set<TEntity>().FromSqlRaw(sql);
                if (!string.IsNullOrEmpty(orderBy))
                {
                    query = query.OrderBy(orderBy);
                }
                query = query.Skip((pageIndex - 1) * take).Take(take);
                return await query.ToListAsync();
            }
        }

        public IQueryable<TEntity> GetSortedData(IQueryable<TEntity> myData, string orderBy)
        {
            using (var context = new TContext())
            {
                if (!string.IsNullOrEmpty(orderBy))
                {
                    myData = myData.OrderBy(orderBy);
                }
                return myData;
            }
        }

        public async Task<List<TEntity>> GetSortedDataAsync(IQueryable<TEntity> myData, string orderBy)
        {
            using (var context = new TContext())
            {
                if (!string.IsNullOrEmpty(orderBy))
                {
                    myData = myData.OrderBy(orderBy);
                }
                return await myData.ToListAsync();
            }
        }

        public IQueryable<DDL> GetDDL(Expression<Func<TEntity, bool>> predicate, bool isGuid, string defaultText, string defaultValue, string selectedValue, int take, string? Params)
        {
            using (var context = new TContext())
            {
                var query = context.Set<TEntity>().Where(predicate).Take(take);

                var ddlList = query.Select(e => new DDL
                {
                    DefaultText = defaultText,
                    DefaultValue = defaultValue,
                    SelectedValue = selectedValue,
                    SelectedText = "Sec"    //incele
                });

                var defaultItem = new DDL
                {
                    DefaultText = defaultText,
                    DefaultValue = defaultValue,
                    SelectedValue = selectedValue,
                    SelectedText = "Sec" // Varsayılan öğe için metin gösterilmez
                };
                ddlList = ddlList.DefaultIfEmpty(defaultItem);

                return ddlList;
            }
        }












        //hepsini repoya ozel yap


        //public Task<List<TEntity>> GetDataAsync(Expression<Func<TEntity, bool>> predicate, int take, string sortOrderBy)
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerable<TEntity> GetDataSql(string sql, int pageIndex, int take, string orderBy)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<Pagination.PaginatedList<TEntity>> GetDataPagedAsync(Expression<Func<TEntity, bool>> predicate, int pageIndex, int take, string orderBy)
        //{

        //    using (var context = new TContext())
        //    {
        //        var query = context.Set<TEntity>().Where(predicate);
        //        var orderQuery = query.OrderBy();
        //        var paginatedList = await Pagination.PaginatedList<TEntity>.CreateAsync(orderQuery, pageIndex, take);
        //        return paginatedList;
        //    }
        //}

        //public IQueryable<TEntity> GetSortedData(IQueryable<TEntity> myData, string orderBy)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<TEntity>> GetSortedDataAsync(IQueryable<TEntity> myData, string orderBy, int take)
        //{
        //    throw new NotImplementedException();
        //}

        //public IQueryable<DDL> GetDDL(Expression<Func<TEntity, bool>> predicate, bool isGuid, string defaultText, string defaultValue, string selectedValue, int take, string? Params)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<List<DDL>> GetDDLAsync(Expression<Func<TEntity, bool>> predicate, bool isGuid, string defaultText, string defaultValue, string selectedText ,string selectedValue, int take, string? Params)
        //{
        //    using (var context = new TContext())
        //    {
        //        var query = context.Set<TEntity>().Where(predicate).Take(take);

        //        var ddlList = new List<DDL>() { new DDL {DefaultText = defaultText, DefaultValue = defaultValue } };

        //        if (!string.IsNullOrEmpty(selectedValue) && selectedValue != defaultValue)
        //        {
        //            ddlList.Add(new DDL { DefaultText = selectedText, DefaultValue = selectedValue });
        //        }

        //        var selectQuery = query.Select(entity => new DDL
        //        {
        //            SelectedText = selectedText,
        //            SelectedValue = selectedValue
        //        });
        //        ddlList.AddRange(await selectQuery.ToListAsync());

        //        return ddlList;
        //    }
        //}
    }
}