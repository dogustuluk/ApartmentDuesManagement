using Entities.Concrete.EntityFramework.Context;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Apartments;
using Web.Services;

namespace Web.Controllers
{
    public class ApartmentsController : Controller
    {
        private readonly ApartmentsApiService _apartmentsApiService;
        public ApartmentsController(ApartmentsApiService apartmentsApiService)
        {
            _apartmentsApiService = apartmentsApiService;
        }

        public async Task<IActionResult> Index()
        {
            var MyData = await _apartmentsApiService.GetApartmentsAsync();

            return View(MyData);


            // var result = View(await _apartmentsApiService.GetApartmentsAsync());
            //return View(result);
            // ViewBag.apartments = await _apartmentsApiService.GetApartmentsAsync();
            // return View();
            //--
            //var apartments = await _apartmentsApiService.GetApartmentsAsync();
            //if (apartments.Success)
            //{
            //    return View(apartments.Data);
            //}
            //else
            //{
            //    ViewBag.ErrorMessage = apartments.Message;
            //    return View("Error");
            //}
        }
    }
}
