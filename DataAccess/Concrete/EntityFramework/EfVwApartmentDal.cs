using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Core.DataAccess.Pagination;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfVwApartmentDal : EfEntityRepositoryBase<VwApartment, ApartmentDuesManagementContext>, IVwApartmentDal
    {
        private readonly ApartmentDuesManagementContext dbContext;

        public EfVwApartmentDal(ApartmentDuesManagementContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<PaginatedList<VwApartment>> GetDataPagedAsync(Expression<Func<VwApartment, bool>> predicate, int pageIndex, int take, string orderBy)
        {
            var query = dbContext.Set<VwApartment>().AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            var count = await query.CountAsync();
            
            if (!string.IsNullOrEmpty(orderBy)) //null kontrolune gerek yok
            {
                query = query.OrderBy(orderBy); //linq.dynamic kutuphanesi ile string order by alinabilir.
            }

            var items = await query.Skip((pageIndex - 1) * take).Take(take).ToListAsync();

            return new PaginatedList<VwApartment>(items, count, pageIndex, take);
        }

        public IEnumerable<VwApartment> GetData(Expression<Func<VwApartment, bool>> predicate = null, int take = 0, string sortOrder = null)
        {
            using (var context = new ApartmentDuesManagementContext())
            {
                var apartments = context.Set<VwApartment>();
                IQueryable<VwApartment> query = apartments;

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
