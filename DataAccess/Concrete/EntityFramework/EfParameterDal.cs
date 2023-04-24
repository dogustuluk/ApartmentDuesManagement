using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfParameterDal : EfEntityRepositoryBase<Parameter, ApartmentDuesManagementContext>, IParameterDal
    {
        public EfParameterDal(ApartmentDuesManagementContext dbContext, ApartmentDuesManagementContext context) : base(dbContext, context)
        {
        }
    }
}
