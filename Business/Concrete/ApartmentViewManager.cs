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

        public IEnumerable<VwApartment> GetData(Expression<Func<VwApartment, bool>> predicate, int take, string OrderBy)
        {
            return _unitOfWork.vwApartmentDal.GetData(predicate, take, OrderBy);
        }

        public async Task<List<VwApartment>> GetDataAsync(Expression<Func<VwApartment, bool>> predicate, int take, string OrderBy)
        {
            return await _unitOfWork.vwApartmentDal.GetDataAsync(predicate, take, OrderBy);
        }

        public async Task<Pagination.PaginatedList<VwApartment>> GetDataPagedAsync(Expression<Func<VwApartment, bool>> predicate, int PageIndex, int take, string orderBy)
        {
            return await _unitOfWork.vwApartmentDal.GetDataPagedAsync(predicate, PageIndex, take, orderBy);
        }

        public IEnumerable<VwApartment> GetDataSql(string sql, int pageIndex, int take, string orderBy)
        {
            return _unitOfWork.vwApartmentDal.GetDataSql(sql, pageIndex, take, orderBy);
        }

        public async Task<List<VwApartment>> GetDataSqlAsync(string sql, int pageIndex, int take, string orderBy)
        {
            return await _unitOfWork.vwApartmentDal.GetDataSqlAsync(sql, pageIndex, take, orderBy);
        }

        public IQueryable<DDL> GetDDL(Expression<Func<VwApartment, bool>> predicate, bool isGuid, string defaultText, string defaultValue, string selectedValue, int take, string? Params)
        {
            return _unitOfWork.vwApartmentDal.GetDDL(predicate,isGuid,defaultText,defaultValue,selectedValue,take, Params);
        }

        public async Task<List<VwApartment>> GetPagedList(int skipCount, int maxResultCount, Expression<Func<VwApartment, bool>> predicate = null, string? orderBy = null, bool isAscending = true)
        {
            return await _unitOfWork.vwApartmentDal.GetPagedViewList(skipCount, maxResultCount, predicate,  orderBy, isAscending);
        }

        public IQueryable<VwApartment> GetSortedData(IQueryable<VwApartment> myData, string orderBy)
        {
            return _unitOfWork.vwApartmentDal.GetSortedData(myData, orderBy);
        }

        public async Task<List<VwApartment>> GetSortedDataAsync(IQueryable<VwApartment> myData, string orderBy)
        {
            return await _unitOfWork.vwApartmentDal.GetSortedDataAsync(myData, orderBy);
        }
    }
}
