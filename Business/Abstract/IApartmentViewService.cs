using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete.EntityFramework.Context;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IApartmentViewService
    {
        Task<List<VwApartment>> GetPagedList(int skipCount, int maxResultCount, Expression<Func<VwApartment, bool>> predicate = null, Expression<Func<VwApartment, int>> orderBy= null, bool isAscending = true);
        Task<List<SelectListItem>> GetCounties(Expression<Func<VwApartment, bool>> filter, Expression<Func<VwApartment, string>> orderBy, Expression<Func<VwApartment, SelectListItem>> selector);
    }
}
