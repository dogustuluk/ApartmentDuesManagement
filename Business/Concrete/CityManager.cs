using Business.Abstract;
using Business.Constants;
using Core.Entities;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Concrete.EntityFramework.Context;
using DataAccess.Concrete.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Business.Concrete
{
    public class CityManager : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CityManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IResult AddCity(City city)
        {
            var newCity = _unitOfWork.cityDal.Add(city);
            if (newCity != null)
            {
                return new SuccessResult(Messages.CityMessages.AddedCity);
                
            }

            return new ErrorResult(Messages.GeneralMessages.GeneralError);
        }

        public Task<List<City>> GetAll()
        {
            var cities = _unitOfWork.cityDal.GetAll();
            return (Task<List<City>>)cities;
        }

        public IQueryable<DDL> GetCityDDL(Expression<Func<City, bool>> predicate, string DDLText, string DDLValue, bool isGUID,
            string DefaultText, string DefaultValue, string SelectedValue, int Take, string OrderBy, string? Params)
        {
            return _unitOfWork.cityDal.GetDDL(predicate,DDLText,DDLValue,isGUID,DefaultText,DefaultValue,SelectedValue,Take,OrderBy,Params);
        }

        public Task<List<DDL>> GetDDLAsync(Expression<Func<City, bool>> predicate, string DDLText, string DDLValue, bool isGUID, string DefaultText, string DefaultValue, string SelectedValue, int Take, string OrderBy, string? Params)
        {
            return _unitOfWork.cityDal.GetDDLAsync(predicate, DDLText, DDLValue, isGUID, DefaultText, DefaultValue, SelectedValue, Take, OrderBy, Params);
        }
    }
}
