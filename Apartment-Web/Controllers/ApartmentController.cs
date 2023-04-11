using Apartment_Web.Models;
using Business.Abstract;
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

        public async Task<IActionResult> Index(int? maxResultCount, string? apartmentFilter, string? countyFilter)
        {
            var skipCount = 0;

            maxResultCount ??= 10;

            var predicate = PredicateBuilder.New<VwApartment>(true);
            if (!string.IsNullOrEmpty(apartmentFilter))
            {
                predicate = predicate.And(p => p.ApartmentName.Contains(apartmentFilter));
            }
            if (!string.IsNullOrEmpty(countyFilter))
            {
                predicate = predicate.And(p => p.CountyName.Contains(countyFilter));
            }


            var model = await _apartmentViewService.GetPagedList(
                skipCount,
                maxResultCount.Value,
                predicate:predicate
                );

            


            return View(model);
        }
    }
}
