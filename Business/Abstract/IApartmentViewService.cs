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
        Task<PaginatedList<VwApartment>> GetDataPagedAsync(Expression<Func<VwApartment, bool>> predicate, int pageIndex, int take, string orderBy);




        //Task<List<VwApartment>> GetPagedList(int skipCount, int maxResultCount, Expression<Func<VwApartment, bool>> predicate = null, Expression<Func<VwApartment, string>> orderBy= null, bool isAscending = true);
        Task<List<VwApartment>> GetPagedList(int skipCount, int maxResultCount, Expression<Func<VwApartment, bool>> predicate = null, string orderBy= null, bool isAscending = true);
        //Task<List<DDL>> GetApartmentListDDLAsync(string defaultText, string defaultValue, string selectedText, string selectedValue, int take);
        // Task<PaginatedList<VwApartment>> GetDataPagedAsync(Expression<Func<VwApartment, bool>> predicate, int pageIndex, int take, string orderBy);
        


    }
}
