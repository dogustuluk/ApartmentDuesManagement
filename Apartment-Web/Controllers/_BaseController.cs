﻿using Business.Abstract;
using Core.Entities;
using DataAccess.Concrete.UnitOfWork;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Apartment_Web.Controllers
{
    public class _BaseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public _BaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public JsonResult GetAjaxApartment_DDL()
        {
            List<DDL> MyReturnList = _unitOfWork.apartmentDal.GetDDL(a=>a.ApartmentId > 0, "ApartmentName","ApartmentId",false,"Seciniz","0","0",100,"ApartmentName ASC","").ToList();

            return Json(MyReturnList);
        }
        public JsonResult GetAjaxCity_DDL()
        {
            List<DDL> MyReturnList = _unitOfWork.cityDal.GetDDL(a => a.CityId > 0, "CityName", "CityId", false, "Seciniz", "0", "0", 100, "CityName ASC", "").ToList();

            return Json(MyReturnList);
        }

        public JsonResult GetAjaxCounty_DDL(int Id)
        {
            List<DDL> MyReturnList = _unitOfWork.countyDal.GetDDL(a => a.CityId == Id, "CountyName", "CountyId", false, "Seciniz", "0", "0", 100, "CountyName ASC", "").ToList();

            return Json(MyReturnList);
        }
        public JsonResult GetAjaxApartmentMember_DDL(int Id)
        {
            List<DDL> MyReturnList = _unitOfWork.memberDal.GetDDL(m => m.ApartmentId == Id, "NameSurname", "MemberId", false, "Seciniz", "0", "0", 100, "NameSurname ASC", "").ToList();

            return Json(MyReturnList);
        }
        public JsonResult GetApartmentMemberInfo(int Id)
        {
            var member = _unitOfWork.memberDal.GetById(Id);
            if (member != null)
            {
                var memberInfo = new
                {
                    NameSurname = member.NameSurname,
                    Email = member.Email,
                    PhoneNumber = member.PhoneNumber
                };
                return Json(memberInfo);
            }
            else
            {
                return Json(null);
            }
        }


    }
}
