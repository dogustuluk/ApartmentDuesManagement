using Business.Abstract;
using Core.DataAccess;
using DataAccess.Concrete.EntityFramework.Context;
using DataAccess.Concrete.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ApartmentFlatViewManager : IApartmentFlatViewService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApartmentFlatViewManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Pagination.PaginatedList<VwApartmentFlat>> GetDataPagedAsync(Expression<Func<VwApartmentFlat, bool>> predicate, int PageIndex, int take, string orderBy)
        {
            return await _unitOfWork.vwApartmentFlatDal.GetDataPagedAsync(predicate, PageIndex, take, orderBy);
        }
    }
}
