﻿using Business.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using DataAccess.Concrete.UnitOfWork;
using Entities.Concrete.EntityFramework.Context;
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
        private readonly IUnitOfWork2 _unitOfWork;

        public ApartmentViewManager(IUnitOfWork2 unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<SelectListItem>> GetCounties(Expression<Func<VwApartment, bool>> filter, Expression<Func<VwApartment, string>> orderBy, Expression<Func<VwApartment, SelectListItem>> selector)
        {
            var ddl = await _unitOfWork.vwApartmentDal.DDl(filter, orderBy, selector);
            return ddl;
        }

        public async Task<List<VwApartment>> GetPagedList(int skipCount, int maxResultCount, Expression<Func<VwApartment, bool>> predicate = null, Expression<Func<VwApartment, int>> orderBy = null, bool isAscending = true)
        {
            return await _unitOfWork.vwApartmentDal.GetPagedViewList(skipCount, maxResultCount, predicate, orderBy, isAscending);
        }
    }
}