using Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Core.DataAccess.Pagination;

namespace Core.DataAccess
{
    public interface IEntityRepository<T>
        where T : class, IEntity, new()
        //IList donersem degisiklik yapamam, sadece okuma islemi yapmami saglar.
    {
        int Add(T entity);
        Task<T> AddAsync(T entity);


        void Update(T entity);
        Task<T> UpdateAsync(T entity);


        void Delete(T entity);
        Task DeleteAsync(T entity);


        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null); //getall
        Task<List<T>> GetAllAsync(List<Expression<Func<T, bool>>> predicates, List<Expression<Func<T, object>>> includePropertries); //list don
        

        T Get(Expression<Func<T, bool>> filter);

        Task<List<T>> GetPagedList<TKey>(int skipCount, int maxResultCount, Expression<Func<T, bool>> predicate = null, Expression<Func<T, TKey>> orderBy = null, bool isAscending = true, params Expression<Func<T, object>>[] includeProperties);

        //Task<List<T>> GetPagedViewList<TKey>(int skipCount, int maxResultCount, Expression<Func<T, bool>> predicate = null, Expression<Func<T, TKey>> orderBy = null, bool isAscending = true);
        Task<List<T>> GetPagedViewList(int skipCount, int maxResultCount, Expression<Func<T, bool>> predicate = null, string? orderBy = null, bool isAscending = true);

        Task<IQueryable<T>> DetailsAsync(params Expression<Func<T, object>>[] propertySelectors);
        IQueryable<T> AddDetails(IQueryable<T> query, Expression<Func<T, object>>[] propertySelectors);

        Task<DbSet<T>> GetDbAsync();

        IQueryable<T> GetQueryable(params Expression<Func<T, object>>[] includeProperties);

        Task<T> GetByIdAsync(int id);
        T? GetById(int id);

        Task<T> GetByGuidAsync(Guid guid);
        T? GetByGuid(Guid guid);






        


        #region MyRegion
        //IEnumerable<T> GetData(Expression<Func<T, bool>> predicate = null, int take = 0, string sortOrderBy = null);//kendi repolarina yaz order olanlari
        //Task<List<T>> GetDataAsync(Expression<Func<T, bool>> predicate, int take, string sortOrderBy);
        //IEnumerable<T> GetDataSql(string sql, int pageIndex, int take, string orderBy);
        //Task<PaginatedList<T>> GetDataPagedAsync(Expression<Func<T, bool>> predicate, int pageIndex, int take, string orderBy);

        //IQueryable<T> GetSortedData(IQueryable<T> myData, string orderBy);
        //Task<List<T>> GetSortedDataAsync(IQueryable<T> myData, string orderBy, int take);


        //IQueryable<DDL> GetDDL(Expression<Func<T, bool>> predicate, bool isGuid, string defaultText, string defaultValue, string selectedValue, int take, string? Params);
        //Task<List<DDL>> GetDDLAsync(Expression<Func<T, bool>> predicate, bool isGuid, string defaultText, string defaultValue, string selectedText, string selectedValue, int take, string? Params);
        #endregion
    }
}
