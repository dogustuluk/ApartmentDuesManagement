using Business.Abstract;
using Core.DataAccess;
using Core.Entities;
using DataAccess.Concrete.EntityFramework.Context;
using DataAccess.Concrete.UnitOfWork;
using Entities.Concrete.EntityFramework.Context;
using LinqKit;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ApartmentViewManager : IApartmentViewService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApartmentViewManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Pagination.PaginatedList<VwApartment>> GetDataPagedAsync(Expression<Func<VwApartment, bool>> predicate, int pageIndex, int take, string orderBy)
        {
            return await _unitOfWork.vwApartmentDal.GetDataPagedAsync(predicate, pageIndex, take, orderBy);
        }






        //public async Task<List<DDL>> GetApartmentListDDLAsync(string defaultText, string defaultValue, string selectedText, string selectedValue, int take)
        //{
        //    var predicate = PredicateBuilder.New<VwApartment>(true);
        //    var ddlList = await _unitOfWork.vwApartmentDal.GetDDLAsync
        //        (
        //        predicate,
        //        false,
        //        defaultText,
        //        defaultValue,
        //        selectedText,
        //        selectedValue,
        //        take,
        //        null
        //        );
        //    return ddlList;
        //}

        //public async Task<List<VwApartment>> GetPagedList(int skipCount, int maxResultCount, Expression<Func<VwApartment, bool>> predicate = null, Expression<Func<VwApartment, string>> orderBy = null, bool isAscending = true)
        //{
        //    return await _unitOfWork.vwApartmentDal.GetPagedViewList(skipCount, maxResultCount, predicate, orderBy, isAscending);
        //}
        public async Task<List<VwApartment>> GetPagedList(int skipCount, int maxResultCount, Expression<Func<VwApartment, bool>> predicate = null, string? orderBy = null, bool isAscending = true)
        {
            return await _unitOfWork.vwApartmentDal.GetPagedViewList(skipCount, maxResultCount, predicate,  orderBy, isAscending);
        }
    }
}
