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
    {
        private readonly ApartmentDuesManagementContext dbContext;

        public EfVwApartmentDal(ApartmentDuesManagementContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<VwApartment> GetData(Expression<Func<VwApartment, bool>> predicate, int take, string OrderBy)
        {

            var query = dbContext.Set<VwApartment>().AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (!string.IsNullOrEmpty(OrderBy))
            {
                query = query.OrderBy(OrderBy);
            }
            if (take > 0)
            {
                query = query.Take(take);
            }
            return query.ToList();

        }

        public async Task<List<VwApartment>> GetDataAsync(Expression<Func<VwApartment, bool>> predicate, int take, string OrderBy)
        {
            var query = dbContext.Set<VwApartment>().AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (!string.IsNullOrEmpty(OrderBy))
            {
                query = query.OrderBy(OrderBy);
            }
            if (take > 0)
            {
                query = query.Take(take);
            }
            return await query.ToListAsync();
        }

        public async Task<PaginatedList<VwApartment>> GetDataPagedAsync(Expression<Func<VwApartment, bool>> predicate, int PageIndex, int take, string orderBy)
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

            var items = await query.Skip((PageIndex - 1) * take).Take(take).ToListAsync();

            return new PaginatedList<VwApartment>(items, count, PageIndex, take);
        }

        public IEnumerable<VwApartment> GetDataSql(string sql, int pageIndex, int take, string orderBy)
        {
            if (string.IsNullOrEmpty(sql))
            {
                throw new ArgumentNullException(nameof(sql));
            }
            var query = dbContext.Set<VwApartment>().FromSqlRaw(sql);
            if (!string.IsNullOrEmpty(orderBy))
            {
                query = query.OrderBy(orderBy);
            }
            query = query.Skip((pageIndex - 1) * take).Take(take);
            return query.ToList();
        }

        public async Task<List<VwApartment>> GetDataSqlAsync(string sql, int pageIndex, int take, string orderBy)
        {
            if (string.IsNullOrEmpty(sql))
            {
                throw new ArgumentNullException(nameof(sql));
            }
            var query = dbContext.Set<VwApartment>().FromSqlRaw(sql);
            if (!string.IsNullOrEmpty(orderBy))
            {
                query = query.OrderBy(orderBy);
            }
            query = query.Skip((pageIndex - 1) * take).Take(take);
            return await query.ToListAsync();
        }

        public IQueryable<DDL> GetDDL(Expression<Func<VwApartment, bool>> predicate, bool isGuid, string defaultText, string defaultValue, string selectedValue, int take, string? Params)
        { //kodu incele
            var query = dbContext.Set<VwApartment>()
                         .Where(predicate)
                         .OrderBy(x => x.CityName)
                         .Take(take)
                         .Select(x => new DDL
                         {
                             DefaultText = defaultText,
                             DefaultValue = defaultValue,
                             SelectedValue = selectedValue ?? defaultValue,
                             SelectedText = selectedValue == null ? defaultText : x.CityName
                         });

            if (isGuid)
            {
                query = query.OrderBy(x => x.DefaultText);
            }

            return query;
        }

        public IQueryable<VwApartment> GetSortedData(IQueryable<VwApartment> myData, string orderBy)
        {
            if (!string.IsNullOrEmpty(orderBy))
            {
                myData = myData.OrderBy(orderBy);
            }
            return myData;
        }

        public async Task<List<VwApartment>> GetSortedDataAsync(IQueryable<VwApartment> myData, string orderBy)
        {
            if (!string.IsNullOrEmpty(orderBy))
            {
                myData = myData.OrderBy(orderBy);
            }
            return await myData.ToListAsync();
        }
    }
}
