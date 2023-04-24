using Core.Entities;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete.EntityFramework.Context;
using LinqKit;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Core.DataAccess.Pagination;

namespace Business.Abstract
{
    public interface IApartmentViewService
    {
        Task<PaginatedList<VwApartment>> GetDataPagedAsync(Expression<Func<VwApartment, bool>> predicate, int PageIndex, int take, string orderBy);

        IEnumerable<VwApartment> GetData(Expression<Func<VwApartment, bool>> predicate, int take, string OrderBy);
        Task<List<VwApartment>> GetDataAsync(Expression<Func<VwApartment, bool>> predicate, int take, string OrderBy);
        IEnumerable<VwApartment> GetDataSql(string sql, int pageIndex, int take, string orderBy);
        Task<List<VwApartment>> GetDataSqlAsync(string sql, int pageIndex, int take, string orderBy);
        IQueryable<VwApartment> GetSortedData(IQueryable<VwApartment> myData, string orderBy);
        Task<List<VwApartment>> GetSortedDataAsync(IQueryable<VwApartment> myData, string orderBy);
        //IQueryable<DDL> GetDDL(Expression<Func<VwApartment, bool>> predicate, bool isGuid, string defaultText, string defaultValue, string selectedValue, int take, string? Params);
        Task<List<VwApartment>> GetPagedList(int skipCount, int maxResultCount, Expression<Func<VwApartment, bool>> predicate = null, string orderBy= null, bool isAscending = true);


    }
}
