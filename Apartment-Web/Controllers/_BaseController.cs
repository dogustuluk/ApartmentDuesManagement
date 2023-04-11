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
        private readonly IUnitOfWork2 _unitOfWork;

        public _BaseController(IApartmentViewService apartmentViewService, IUnitOfWork2 unitOfWork)
        {
            _apartmentViewService = apartmentViewService;
            _unitOfWork = unitOfWork;
        }

        //public JsonResult GetAjax_DDL(string WHAT, string Params, string ID)
        //{
        //    List<DDL> MyReturnList = new();

        //    if (WHAT == "Apartments")
        //    {
        //        MyReturnList = _unitOfWork.apartmentDal.GetDDL(x=>true,false,"Seciniz","0","ApartmentName",50,"ApartmentName");
        //        //MyReturnList = _serviceManager.Cities.GetDDL(a => a.CityID > 0, false, "Seçiniz", "0", "0", 100, "");
        //    }
        //    return Json(MyReturnList);
        //}
    }
}
