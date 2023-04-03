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
              var result = View(await _apartmentsApiService.GetApartmentsAsync());
            return View(result);
           // ViewBag.apartments = await _apartmentsApiService.GetApartmentsAsync();
           // return View();
        }
    }
}
