using Core.DataAccess.EntityFramework;
using Core.Entities;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCityDal : EfEntityRepositoryBase<City, ApartmentDuesManagementContext>, ICityDal
    {
        public EfCityDal(ApartmentDuesManagementContext dbContext, ApartmentDuesManagementContext context) : base(dbContext, context)
        {
        }

       
    }
}
