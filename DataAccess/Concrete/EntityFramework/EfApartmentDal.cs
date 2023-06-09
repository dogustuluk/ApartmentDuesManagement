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
    public class EfApartmentDal : EfEntityRepositoryBase<Apartment, ApartmentDuesManagementContext>, IApartmentDal
    {
        public EfApartmentDal(ApartmentDuesManagementContext dbContext) : base(dbContext)
        {
        }
    }
}
