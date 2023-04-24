using Business.Abstract;
using Core.Entities;
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
    public class CountyManager : ICountyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CountyManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IQueryable<DDL> GetCountyDDL(Expression<Func<County, bool>> predicate, string DDLText, string DDLValue, bool isGUID,
            string DefaultText, string DefaultValue, string SelectedValue, int Take, string OrderBy, string? Params)
        {
            return _unitOfWork.countyDal.GetDDL(predicate, DDLText, DDLValue, isGUID, DefaultText, DefaultValue, SelectedValue, Take, OrderBy, Params);
        }

        public Task<List<DDL>> GetDDLAsync(Expression<Func<County, bool>> predicate, string DDLText, string DDLValue, bool isGUID, string DefaultText, string DefaultValue, string SelectedValue, int Take, string OrderBy, string? Params)
        {
            return _unitOfWork.countyDal.GetDDLAsync(predicate, DDLText, DDLValue, isGUID, DefaultText, DefaultValue, SelectedValue, Take, OrderBy, Params);
        }
    }
}
