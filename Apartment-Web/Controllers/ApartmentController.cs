using Apartment_Web.Models;
using Business.Abstract;
using Core.DataAccess;
using Core.Utilities.Extensions;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete.EntityFramework.Context;
using Entities.Dtos;
using LinqKit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Drawing.Printing;
using System.Linq.Expressions;
using System.Text.Json;

namespace Apartment_Web.Controllers
{
    public class ApartmentController : Controller
    {
        private readonly IApartmentViewService _apartmentViewService;
        private readonly IApartmentService _apartmentService;

        public ApartmentController(IApartmentViewService apartmentViewService, IApartmentService apartmentService)
        {
            _apartmentViewService = apartmentViewService;
            _apartmentService = apartmentService;
        }


        //public async Task<IActionResult> Index(string? apartmentFilter, string? countyFilter, string? orderOpt)
        //{
        //    var skipCount = 0;
        //    var maxResultCount = 25;

        //    var predicate = PredicateBuilder.New<VwApartment>(true);

        //    if (!string.IsNullOrEmpty(apartmentFilter))
        //    {
        //        predicate = predicate.And(p => p.ApartmentName.Contains(apartmentFilter));
        //    }
        //    if (!string.IsNullOrEmpty(countyFilter))
        //    {
        //        predicate = predicate.And(p => p.CountyName.Contains(countyFilter));
        //    }

        //    var orderOptions = new List<SelectListItem>
        //    {
        //        new SelectListItem {Value = "DoorNumber-asc", Text="kapi numarasi artan"},
        //        new SelectListItem {Value = "DoorNumber-desc", Text="kapi numarasi azalan"}
        //    };

        //    ViewBag.OrderOptions = orderOptions;
        //    if (orderOpt?.StartsWith("-") == true)
        //    {
        //        orderOpt = orderOpt.Substring(1);
        //    }

        //    var model = await _apartmentViewService.GetPagedList(
        //        skipCount,
        //        maxResultCount: maxResultCount,
        //        predicate: predicate,
        //        orderBy:orderOpt
        //        );


        //    ViewBag.OrderOptions = new Func<List<SelectListItem>>(() => orderOptions);

        //    return View(model);
        //}
        public async Task<IActionResult> Index( string? countyFilter, string? orderBy ,int? PageIndex=1)
        {
            //var skipCount = 0;
            string defaultSortOrder = "ApartmentId ASC";
            //int PageIndex = 1;
            var predicate = PredicateBuilder.New<VwApartment>(true);

            
            if (!string.IsNullOrEmpty(countyFilter))
            {
                predicate = predicate.And(p => p.CountyName.Contains(countyFilter));
            }


            var sortOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "ApartmentId ASC", Text = "Apartman id artan" },
                new SelectListItem { Value = "ApartmentId DESC", Text = "Apartman id azalan" },
                new SelectListItem { Value = "CountyName ASC", Text = "Ilceye gore artan" },
                new SelectListItem { Value = "CountyName DESC", Text = "Ilceye gore azalan" },
                new SelectListItem { Value = "DoorNumber ASC", Text = "Kapi numarasina gore artan" },
                new SelectListItem { Value = "DoorNumber DESC", Text = "Kapi numarasina gore azalan" },
                new SelectListItem { Value = "UpdatedDate ASC", Text = "Guncelleme tarihine gore artan" },
                new SelectListItem { Value = "UpdatedDate DESC", Text = "Guncelleme tarihine gore azalan" },
            };
            
            var selectedSortItem = new SelectList(sortOptions, "Value", "Text", orderBy);
           var x = selectedSortItem.SelectedValue;
            
            var PagedList = await _apartmentViewService.GetDataPagedAsync(predicate, (int)PageIndex, 25, orderBy ?? defaultSortOrder);

            Pagination MyPG = new()
            {
                PageIndex = (int)PageIndex,
                pageSize = 25,
                TotalPages = PagedList.TotalPages,
                TotalRecords = PagedList.TotalRecords,
                HasPreviousPage = PagedList.HasPreviousPage,
                HasNextPage = PagedList.HasNextPage
            };

            Dictionary<string, object> Parameters = new()
            {
                { "PageIndex", PageIndex },

                { "OrderBy", orderBy},
             };
            ViewBag.order = orderBy;
            Index_VM MYRESULT = new()
            {
                PageTitle = "Apartmanlar",
                PagedList = PagedList,
                SortOptions = sortOptions,
                Parameters = Parameters,
                MyPagination = MyPG,
                OrderBy = orderBy
            };

            return View(MYRESULT);
        }

        public class Index_VM
        {
            public string PageTitle { get; set; }
            public List<VwApartment>? PagedList { get; set; }
            public List<City>? ListCity { get; set; }
            public Pagination? MyPagination { get; set; }
            public Dictionary<string, object>? Parameters { get; set; }
            public List<SelectListItem>? SortOptions { get; set; }
            public string? OrderBy { get; set; }
        }
    }
}
