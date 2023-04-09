﻿using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete.EntityFramework.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfVwApartmentDal : EfEntityRepositoryBase<VwApartment, ApartmentDuesManagementContext>, IVwApartmentDal
    {
        public EfVwApartmentDal(ApartmentDuesManagementContext dbContext) : base(dbContext)
        {
        }
    }
}