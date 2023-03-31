using Core.Entities;
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

        //cozum sor
        async Task<TEntity> IEntityRepository<TEntity>.GetAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            using (var context = new TContext())
            {
                IQueryable<TEntity> query = context.Set<TEntity>();
                query = query.Where(predicate);
                if (includeProperties.Any())
                {
                    foreach (var includeProperty in includeProperties)
                    {
                        query = query.Include(includeProperty);
                    }
                }
                return await query.AsNoTracking().SingleOrDefaultAsync(); //firstAsync kullanirsam yavaslama olacaktir.
            }
        }
        public async Task<TEntity> GetAsyncV2(IList<Expression<Func<TEntity, bool>>> predicates, IList<Expression<Func<TEntity, object>>> includeProperties)
        {
            using (var context = new TContext())
            {
                IQueryable<TEntity> query = context.Set<TEntity>();
                if (predicates != null && predicates.Any())
                {
                    foreach (var predicate in predicates) //isactive=true, isdeleted=false,...
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
                return await query.AsNoTracking().SingleOrDefaultAsync();
            }
        }
        public async Task<TEntity> GetAsyncV3(IList<Expression<Func<TEntity, bool>>> predicates, IList<Expression<Func<TEntity, object>>> includeProperties, Expression<Func<TEntity, int>> selectProperty)
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
                if (selectProperty != null)
                {
                    query = query.Select(selectProperty).Cast<TEntity>(); //metot TEntity donuyor, select kisminda ise IQueryable donuyor. return etmeden once Cast islemi ekleyerek int tipini TEntity'e donustur.
                }
                return await query.AsNoTracking().SingleOrDefaultAsync();
            }
        }
        public async Task<TEntity> GetAsyncV4<TResult>(IList<Expression<Func<TEntity, bool>>> predicates, IList<Expression<Func<TEntity, object>>> includeProperties, Expression<Func<TEntity, TResult>> selectProperty)
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
                if (selectProperty != null)
                {
                    query = query.Select(selectProperty).Cast<TEntity>(); //metot TEntity donuyor, select kisminda ise IQueryable donuyor. return etmeden once Cast islemi ekleyerek int tipini TEntity'e donustur.
                }
                return await query.AsNoTracking().SingleOrDefaultAsync();
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
        }
    }