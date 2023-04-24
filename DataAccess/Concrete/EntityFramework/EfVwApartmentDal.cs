using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using Core.Entities;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete.EntityFramework.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Core.DataAccess.Pagination;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfVwApartmentDal : EfEntityRepositoryBase<VwApartment, ApartmentDuesManagementContext>, IVwApartmentDal
    {//base'de tanimla metotlari

        public EfVwApartmentDal(ApartmentDuesManagementContext dbContext, ApartmentDuesManagementContext context) : base(dbContext, context)
        {
        }
    }
}
