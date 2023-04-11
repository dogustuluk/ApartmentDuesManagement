using Core.DataAccess;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete.EntityFramework.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Core.DataAccess.Pagination;

namespace DataAccess.Abstract
{
    public interface IVwApartmentDal : IEntityRepository<VwApartment>
    {
        Task<PaginatedList<VwApartment>> GetDataPagedAsync(Expression<Func<VwApartment, bool>> predicate, int pageIndex, int take, string orderBy);

    }
}
