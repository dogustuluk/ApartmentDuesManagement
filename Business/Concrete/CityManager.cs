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

        public async Task<List<City>> GetAll()
        {
            var cities = _unitOfWork.cityDal.GetAll();
            return cities.ToList();
        }

        public IQueryable<DDL> GetCityDDL(Expression<Func<City, bool>> predicate, bool isGuid, string defaultText, string defaultValue, string selectedValue, int take, string? Params)
        {
            IQueryable<DDL> ddlList = _unitOfWork.cityDal.GetDDL(predicate, isGuid, defaultText, defaultValue, selectedValue, take, Params);
            return ddlList;
        }
    }
}
