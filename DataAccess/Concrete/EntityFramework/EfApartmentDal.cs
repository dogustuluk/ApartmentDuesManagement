using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using Core.Entities;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfApartmentDal : EfEntityRepositoryBase<Apartment, ApartmentDuesManagementContext>, IApartmentDal
    {
        public EfApartmentDal(ApartmentDuesManagementContext dbContext) : base(dbContext)
        {
        }
        #region MyRegion
        //public IEnumerable<Apartment> GetData(Expression<Func<Apartment, bool>> predicate = null, int take = 0, string sortOrderBy = null)
        //{
        //    using (var context = new ApartmentDuesManagementContext())
        //    {
        //        var query = context.Set<Apartment>().AsQueryable();

        //        if (predicate != null)
        //        {
        //            query = query.Where(predicate);
        //        }


        //        if (!string.IsNullOrEmpty(sortOrderBy))
        //        {
        //            query = query.OrderBy(sortOrderBy);
        //        }

        //        if (take > 0)
        //        {
        //            query = query.Take(take);

        //        }
        //        return query.ToList();
        //    }
        //}
        #endregion

        public IEnumerable<Apartment> GetData(Expression<Func<Apartment, bool>> predicate = null, int take = 0, string sortOrder = null)
        {
            using (var context = new ApartmentDuesManagementContext())
            {
                var apartments = context.Set<Apartment>();
                IQueryable<Apartment> query = apartments;

                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                if (!string.IsNullOrEmpty(sortOrder))
                {
                    if (sortOrder.StartsWith("-"))
                    {
                        sortOrder = sortOrder.Substring(1);
                        query = query.OrderByDescending(x => x.GetType().GetProperty(sortOrder).GetValue(x));
                    }
                    else
                    {
                        query = query.OrderBy(x => x.GetType().GetProperty(sortOrder).GetValue(x));
                    }
                }

                if (take > 0)
                {
                    query = query.Take(take);
                }

                return query.ToList();
            }
        }


    }
}
