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
      //  IResult AddCity(City city);
        Task<List<City>> GetAll();
        IQueryable<DDL> GetCityDDL(Expression<Func<City, bool>> predicate, string DDLText, string DDLValue, bool isGUID,
            string DefaultText, string DefaultValue, string SelectedValue, int Take, string OrderBy, string? Params);
        Task<List<DDL>> GetDDLAsync(Expression<Func<City, bool>> predicate, string DDLText, string DDLValue, bool isGUID,
            string DefaultText, string DefaultValue, string SelectedValue, int Take, string OrderBy, string? Params);
    }
}
