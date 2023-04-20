using Business.Abstract;
using Core.DataAccess;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete.EntityFramework.Context;
using LinqKit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Apartment_Web.Controllers
{
    public class ApartmentFlatController : Controller
    {
        private readonly IApartmentFlatViewService _apartmentFlatViewService;

        public ApartmentFlatController(IApartmentFlatViewService apartmentViewService)
        {
            _apartmentFlatViewService = apartmentViewService;
        }

        public async Task<IActionResult> Index(string? orderBy, int? PageIndex = 1)
        {
            string defaultSortOrder = "ApartmentFlatId ASC";

            var predicate = PredicateBuilder.New<VwApartmentFlat>(true);

            var sortOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "ApartmentId ASC", Text = "Apartman id artan" },
                new SelectListItem { Value = "ApartmentId DESC", Text = "Apartman id azalan" },
                new SelectListItem { Value = "ApartmentFlatId ASC", Text = "Daire id artan" },
                new SelectListItem { Value = "ApartmentFlatId DESC", Text = "Daire id azalan" },
                new SelectListItem { Value = "FlatNumber ASC", Text = "Kat numarasi artan" },
                new SelectListItem { Value = "FlatNumber DESC", Text = "Kat numarasi azalan" },
                new SelectListItem { Value = "ApartmentName ASC", Text = "Apartman adina gore artan" },
                new SelectListItem { Value = "ApartmentName DESC", Text = "Apartman adina gore azalan" },
                new SelectListItem { Value = "UpdatedDate ASC", Text = "Guncelleme tarihine gore artan" },
                new SelectListItem { Value = "UpdatedDate DESC", Text = "Guncelleme tarihine gore azalan" },
            };
            var selectedSortItem = new SelectList(sortOptions, "Value", "Text", orderBy);

            var PagedList = await _apartmentFlatViewService.GetDataPagedAsync(null,(int)PageIndex,25, orderBy ?? defaultSortOrder);
            

            Dictionary<string, object> Parameters = new()
            {
                { "PageIndex", PageIndex },
            };

            Pagination MyPG = new()
            {
                PageIndex = (int)PageIndex,
                pageSize = 25,
                TotalPages = PagedList.TotalPages,
                TotalRecords = PagedList.TotalRecords,
                HasPreviousPage = PagedList.HasPreviousPage,
                HasNextPage = PagedList.HasNextPage
            };

            Index_VM MYRESULT = new()
            {
                PageTitle = "Daireler",
                PagedList = PagedList,
                Parameters = Parameters,
                MyPagination = MyPG,
                SortOptions = sortOptions,
                OrderBy = orderBy
            };

            return View(MYRESULT);
        }


        public class Index_VM
        {
            public string PageTitle { get; set; }
            public List<VwApartmentFlat>? PagedList { get; set; }
            public List<Apartment>? ListApartment { get; set; }
            public Pagination? MyPagination { get; set; }
            public Dictionary<string, object>? Parameters { get; set; }
            public List<SelectListItem>? SortOptions { get; set; }
            public string? OrderBy { get; set; }
            public int? ApartmentId { get; set; }
        }
    }
}
