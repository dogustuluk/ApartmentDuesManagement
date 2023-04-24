using Business.Abstract;
using Core.Entities;
using DataAccess.Concrete.UnitOfWork;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Apartment_Web.Controllers
{
    public class _BaseController : Controller
    {
        private readonly IApartmentViewService _apartmentViewService;
        private readonly IApartmentService _apartmentService;
        private readonly IUnitOfWork _unitOfWork;

        public _BaseController(IApartmentViewService apartmentViewService, IUnitOfWork unitOfWork)
        {
            _apartmentViewService = apartmentViewService;
            _unitOfWork = unitOfWork;
        }

        public JsonResult GetAjaxCity_DDL()
        {
            List<DDL> MyReturnList =_unitOfWork.cityDal.GetDDL(a => a.CityId > 0, "CityName", "CityID", false, "Seciniz", "0", "0", 100, "CityName Asc", "").ToList();

            return Json(MyReturnList);
        }

        public JsonResult GetAjaxCounty_DDL(int Id)
        {
            List<DDL> MyReturnList = _unitOfWork.countyDal.GetDDL(a => a.CityId ==Id, "CountyName", "CountyID", false, "Seciniz", "0", "0", 100, "CountyName Asc", "").ToList();

            return Json(MyReturnList);
        }


    }
}
