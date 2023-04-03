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
            return View(await _apartmentsApiService.GetApartmentsAsync());
        }
    }
}
