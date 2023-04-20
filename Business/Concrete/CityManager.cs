using Business.Abstract;
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

        public async Task<List<City>> GetAll()
        {
            var cities = _unitOfWork.cityDal.GetAll();
            return cities.ToList();
        }
    }
}
