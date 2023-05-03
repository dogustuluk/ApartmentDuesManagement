using Apartment_Web.Models;
using Business.Abstract;
using Business.Constants;
using Business.Validations;
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
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing.Printing;
using System.Linq.Expressions;
using System.Text.Json;
using System.Transactions;
using static Business.Validations.Validations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Apartment_Web.Controllers
{
    public class ApartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICityService _cityService;
        public ApartmentController(IUnitOfWork unitOfWork, ICityService cityService)
        {
            _unitOfWork = unitOfWork;
            _cityService = cityService;
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

            ViewBag.cities = cityList;

            var sortOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "ApartmentId ASC", Text = "Apartman id artan" },
                new SelectListItem { Value = "ApartmentId DESC", Text = "Apartman id azalan" },
                new SelectListItem { Value = "CityName ASC", Text = "Sehre gore artan" },
                new SelectListItem { Value = "CityName DESC", Text = "Sehre gore azalan" },
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
        public IActionResult Add()
        {
            return View();

        }
        [HttpPost]
        public IActionResult Add(ApartmentAddDto apartmentAddDto)
        {
            var validator = new ApartmentAddValidation();
            var result = validator.Validate(apartmentAddDto);
            if (result.IsValid)
            {
                var newApartment = new Apartment
                {

                    ApartmentName = apartmentAddDto.ApartmentName,
                    BlockNo = apartmentAddDto.BlockNo,
                    DoorNumber = apartmentAddDto.DoorNumber,
                    NumberOfFlats = apartmentAddDto.NumberOfFlats,
                    CityId = apartmentAddDto.CityId,
                    CountyId = apartmentAddDto.CountyId,
                    OpenAdress = apartmentAddDto.OpenAdress,
                    IsActive = 1,
                    Code = apartmentAddDto.GenerateApartmentCode()
                };


                _unitOfWork.apartmentDal.Add(newApartment);
                _unitOfWork.Commit();

                if (apartmentAddDto.ResponsibleMemberInfo.NameSurname != null)
                {
                    var responsibleMember = new Member
                    {
                        NameSurname = apartmentAddDto.ResponsibleMemberInfo.NameSurname,
                        Email = apartmentAddDto.ResponsibleMemberInfo.Email,
                        PhoneNumber = apartmentAddDto.ResponsibleMemberInfo.PhoneNumber,
                    };

                    _unitOfWork.memberDal.Add(responsibleMember);
                    _unitOfWork.Commit();

                    var apartment = _unitOfWork.apartmentDal.GetById(newApartment.ApartmentId);
                    apartment.ResponsibleMemberId = responsibleMember.MemberId;
                    _unitOfWork.apartmentDal.Update(apartment);
                }
                return RedirectToAction("Update", new { id= newApartment.ApartmentId });
            }
            else
            {
                var errors = result.Errors.Select(x => x.ErrorMessage).ToList();
                foreach (var error in errors)
                {
                    ModelState.AddModelError("", error);
                }
                return View(apartmentAddDto);


            }
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var result = _unitOfWork.apartmentDal.GetById(id);
            var responsibleMember = _unitOfWork.memberDal.GetById(result.ResponsibleMemberId);

            var apartment = new ApartmentUpdateDto
            {
                ApartmentId = result.ApartmentId,
                Guid = result.Guid,
                ApartmentName = result.ApartmentName,
                BlockNo = result.BlockNo,
                CityId = result.CityId,
                CountyId = result.CountyId,
                DoorNumber = result.DoorNumber,
                IsActive = result.IsActive,
                NumberOfFlats = result.NumberOfFlats,
                OpenAdress = result.OpenAdress,
                MemberId = result.ResponsibleMemberId,
                ResponsibleMemberInfo = responsibleMember?.NameSurname != null ? new MemberShortDto
                {
                    NameSurname = responsibleMember.NameSurname,
                    Email = responsibleMember.Email,
                    PhoneNumber = responsibleMember.PhoneNumber
                } : null

            };
            return View(apartment);
        }

        [HttpPost]
        public IActionResult Update(int id, ApartmentUpdateDto updatedApartment)
        {
            var apartment = _unitOfWork.apartmentDal.GetById(id);
            if (apartment == null)
            {
                return NotFound();
            }
            var validator = new ApartmentUpdateValidation();
            var result = validator.Validate(updatedApartment);
            if (result.IsValid)
            {
                apartment.ApartmentName = updatedApartment.ApartmentName;
                apartment.BlockNo = updatedApartment.BlockNo;
                apartment.CityId = updatedApartment.CityId;
                apartment.CountyId = updatedApartment.CountyId;
                apartment.DoorNumber = updatedApartment.DoorNumber;
                apartment.IsActive = updatedApartment.IsActive;
                apartment.NumberOfFlats = updatedApartment.NumberOfFlats;
                apartment.OpenAdress = updatedApartment.OpenAdress;
                apartment.ResponsibleMemberId = updatedApartment.MemberId;
                apartment.Code = updatedApartment.GenerateApartmentCode();
                var alreadyMember = _unitOfWork.memberDal.GetById(apartment.ResponsibleMemberId);
                if (updatedApartment.ResponsibleMemberInfo.NameSurname != alreadyMember.NameSurname)
                {
                    alreadyMember.NameSurname = updatedApartment.ResponsibleMemberInfo.NameSurname;
                    _unitOfWork.memberDal.Update(alreadyMember);
                }

                if (updatedApartment.ResponsibleMemberInfo.Email != alreadyMember.Email)
                {
                    alreadyMember.Email = updatedApartment.ResponsibleMemberInfo.Email;
                    _unitOfWork.memberDal.Update(alreadyMember);
                }
                if (updatedApartment.ResponsibleMemberInfo.PhoneNumber != alreadyMember.PhoneNumber)
                {
                    alreadyMember.PhoneNumber = updatedApartment.ResponsibleMemberInfo.PhoneNumber;
                    _unitOfWork.memberDal.Update(alreadyMember);
                }
                _unitOfWork.apartmentDal.Update(apartment);
                TempData["updateSuccess"] = true;
               // TempData["updatedApartment"] = " ";
                return RedirectToAction("Update", "Apartment", new { ApartmentId = id });

            }
            else
            {
                var errors = result.Errors.Select(x => x.ErrorMessage).ToList();
                foreach (var error in errors)
                {
                    ModelState.AddModelError("", error);
                }
                return View(updatedApartment);
            }

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
