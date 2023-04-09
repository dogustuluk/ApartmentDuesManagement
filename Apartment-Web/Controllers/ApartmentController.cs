using Apartment_Web.Models;
using Business.Abstract;
using Core.Utilities.Extensions;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete.EntityFramework.Context;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using System.Text.Json;

namespace Apartment_Web.Controllers
{
    public class ApartmentController : Controller
    {
        private readonly IApartmentViewService _apartmentViewService;

        public ApartmentController(IApartmentViewService apartmentViewService)
        {
            _apartmentViewService = apartmentViewService;
        }

        public async Task<IActionResult> Index(int? maxResultCount)
        {
            var skipCount = 0;
            maxResultCount ??= 10;
            var model = await _apartmentViewService.GetPagedList(skipCount, maxResultCount.Value);

            var counties = await _apartmentViewService.GetCounties(
                 filter: x => true,
                 orderBy: x => x.CountyName,
                 selector: x => new SelectListItem
                 {
                     Text = x.CountyName,
                     Value = x.CountyName
                 });

            ViewBag.CountySelect = new SelectList(counties, "Value", "Text");

            return View(model);
        }
    }
}
