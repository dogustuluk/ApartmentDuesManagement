using DataAccess.Concrete.EntityFramework.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Core.DataAccess.Pagination;

namespace Business.Abstract
{
    public interface IApartmentFlatViewService
    {
        Task<PaginatedList<VwApartmentFlat>> GetDataPagedAsync(Expression<Func<VwApartmentFlat, bool>> predicate, int PageIndex, int take, string orderBy);
    }
}
