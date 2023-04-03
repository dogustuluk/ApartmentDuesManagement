using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentController : ControllerBase
    {
        private readonly IApartmentService _apartmentService;

        public ApartmentController(IApartmentService apartmentService)
        {
            _apartmentService = apartmentService;
        }
       
        [HttpGet("getapartmentlist")]
        public IActionResult GetApartmentList()
        {
            var result = _apartmentService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetApartmentById(int id)
        {
            var result = await _apartmentService.GetApartmentById(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpGet("getapartmentswithflats")]
        public async Task<IActionResult> GetApartmentsWithFlats()
        {
            var result = await _apartmentService.GetApartmentsWithFlat();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("hata");
        }
        [HttpGet("getapartmentswithflatsandmembers")]
        public async Task<IActionResult> GetApartmentsWithFlatsAndMembers()
        {
            var result = await _apartmentService.GetApartmentsWithFlatAndMember();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("hata");

        }
    }
}
