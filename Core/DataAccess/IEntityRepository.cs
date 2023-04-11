using Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> 
        where T : class, IEntity, new()

    {
        int Add(T entity);
        Task<T> AddAsync(T entity);
        
        void Update(T entity);
        Task<T> UpdateAsync(T entity);
       
        void Delete(T entity);
        Task DeleteAsync(T entity);
       
        IEnumerable<T> GetList(Expression<Func<T, bool>> filter = null); //getall
        //IEnumerable<T> GetData(Expression<Func<T, bool>> filter = null, int take=0, string sortOrderBy=null);
        Task<List<T>> GetListAsync(List<Expression<Func<T, bool>>> predicates, List<Expression<Func<T, object>>> includePropertries); //list don
        
        T Get(Expression<Func<T, bool>> filter);
        
        
        Task<IList<T>> FromSqlRawAsync(string sql, params object[] parameters);
        
        //sayfalama icin
        Task<List<T>> GetPagedList<TKey> (int skipCount, int maxResultCount, Expression<Func<T, bool>> predicate= null, Expression<Func<T, TKey>> orderBy=null, bool isAscending=true, params Expression<Func<T, object>>[] includeProperties);
        
        Task<List<T>> GetPagedViewList<TKey>(int skipCount, int maxResultCount, Expression<Func<T, bool>> predicate = null, Expression<Func<T, TKey>> orderBy = null, bool isAscending = true);
        
        Task<IQueryable<T>> DetailsAsync(params Expression<Func<T, object>>[] propertySelectors);
        IQueryable<T> AddDetails(IQueryable<T> query, Expression<Func<T, object>>[] propertySelectors);
        
        Task<DbSet<T>> GetDbAsync();

        Task<IQueryable<T>> GetQueryableAsync();
        IQueryable<T> GetQueryable(params Expression<Func<T, object>>[] includeProperties);
        //sayfalama icin
        Task<T> GetByIdAsync(int id);

        Task<T> GetByGuid(Guid guid);
        IQueryable<T> Filter(IQueryable<T> query, Expression<Func<T, bool>> predicate);
       



    }
}
