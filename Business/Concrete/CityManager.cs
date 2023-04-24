using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Concrete.EntityFramework.Context;
using DataAccess.Concrete.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
