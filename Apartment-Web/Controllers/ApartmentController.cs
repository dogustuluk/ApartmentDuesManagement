﻿using Apartment_Web.Models;
using Business.Abstract;
using Core.DataAccess;
using Core.Entities;
using Core.Utilities.Extensions;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Concrete.EntityFramework.Context;
using DataAccess.Concrete.UnitOfWork;
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
        private readonly IUnitOfWork _unitOfWork;
        public ApartmentController( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index(int? cityId, string? countyFilter, string? orderBy, int? PageIndex = 1)
        {
            var skipCount = 0;
            string defaultSortOrder = "ApartmentId ASC";
            var predicate = PredicateBuilder.New<VwApartment>(true);


            if (!string.IsNullOrEmpty(countyFilter))
            {
                predicate = predicate.And(p => p.CountyName.Contains(countyFilter));
            }

            if (cityId != null)
            {
                predicate = predicate.And(p => p.CityId == cityId);
            }

            var cityList = _unitOfWork.cityDal.GetAll();

            #region MyRegion
            //var ddl = _apartmentViewService.GetDDL(predicate, false, "seciniz", "1", "", 0, null);
            //var ddlList = _apartmentViewService.GetDDL
            //   (
            //   predicate: c => true,
            //   isGuid: false,
            //   defaultText: "Sehir Sec",
            //   defaultValue: "-1",
            //   selectedValue: "",
            //   take: 0,
            //   Params: string.Join(",", cityList.Select(c => c.CityName))
            //   )
            //   .Select(c => new DDL
            //   {
            //       SelectedText = c.SelectedText,
            //       SelectedValue = c.SelectedValue
            //   })
            //   .ToList();
            #endregion


            ViewBag.cities = cityList;

            var sortOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "ApartmentId ASC", Text = "Apartman id artan" },
                new SelectListItem { Value = "ApartmentId DESC", Text = "Apartman id azalan" },
                new SelectListItem { Value = "CityId ASC", Text = "Sehre gore artan" },
                new SelectListItem { Value = "CityId DESC", Text = "Sehre gore azalan" },
                new SelectListItem { Value = "CountyName ASC", Text = "Ilceye gore artan" },
                new SelectListItem { Value = "CountyName DESC", Text = "Ilceye gore azalan" },
                new SelectListItem { Value = "DoorNumber ASC", Text = "Kapi numarasina gore artan" },
                new SelectListItem { Value = "DoorNumber DESC", Text = "Kapi numarasina gore azalan" },
                new SelectListItem { Value = "UpdatedDate ASC", Text = "Guncelleme tarihine gore artan" },
                new SelectListItem { Value = "UpdatedDate DESC", Text = "Guncelleme tarihine gore azalan" },
            };

            var selectedSortItem = new SelectList(sortOptions, "Value", "Text", orderBy);

            var PagedList = await _unitOfWork.vwApartmentDal.GetDataPagedAsync(predicate, (int)PageIndex, 25, orderBy ?? defaultSortOrder);

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
                OrderBy = orderBy,
                CityId = cityId,
            };

            return View(MYRESULT);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var MYRESULT = new ApartmentAddDto()
            {
                CityId = 35,
                CountyId = 476,
                CityIdDDL = _unitOfWork.cityDal.GetDDL(x => x.CityId > 0, false, "", "0", "35", 100, "").ToList(),
                CountyIdDDL = _unitOfWork.countyDal.GetDDL(x=>x.CountyId ==476, false,"","0","476",100,"").ToList(),
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
            public int? CityId { get; set; }
        }
    }
}
