using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCA_DataAccess.Data
{
	public class Pagination
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int pageSize { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public int TotalRecords { get; set; }
    }

    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalRecords { get; private set; }
        public static int pageSize = 25;

        public PaginatedList(List<T> items, int TotalRecordsAA, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(TotalRecordsAA / (double)pageSize);
            TotalRecords = TotalRecordsAA;

            this.AddRange(items);
        }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public static async Task<PaginatedList<T>> CreateAsync(
            IQueryable<T> source, int pageIndex, int pageSize)
        {
            var TotalRecords = await source.CountAsync();
            var items = await source.Skip(
                (pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, TotalRecords, pageIndex, pageSize);
        }

		public static PaginatedList<T> Create(
			List<T> source, int pageIndex, int pageSize)
		{
			var TotalRecords = source.Count();
			var items = source.Skip(
				(pageIndex - 1) * pageSize)
				.Take(pageSize).ToList();
			return new PaginatedList<T>(items, TotalRecords, pageIndex, pageSize);
		}
	}
}
