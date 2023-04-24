using Core.Entities;
using Core.Utilities.Results.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICityService
    {
        IResult AddCity(City city);
        Task<List<City>> GetAll();
        IQueryable<DDL> GetCityDDL(Expression<Func<City, bool>> predicate, bool isGuid, string defaultText, string defaultValue, string selectedValue, int take, string? Params);
    }
}
