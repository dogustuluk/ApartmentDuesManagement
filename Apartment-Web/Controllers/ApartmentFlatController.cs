using Business.Abstract;
using Core.DataAccess;
using DataAccess.Concrete.EntityFramework.Context;
using DataAccess.Concrete.UnitOfWork;
using Entities.Concrete.EntityFramework.Context;
using Entities.Dtos;
using LinqKit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Apartment_Web.Controllers
{
    public class ApartmentFlatController : Controller
    {
        private readonly IApartmentFlatViewService _apartmentFlatViewService;
        private readonly IUnitOfWork _unitOfWork;
        public ApartmentFlatController(IApartmentFlatViewService apartmentViewService, IUnitOfWork unitOfWork)
        {
            _apartmentFlatViewService = apartmentViewService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(string? orderBy, int? PageIndex = 1)
        {
            string defaultSortOrder = "ApartmentFlatId ASC";

            var predicate = PredicateBuilder.New<VwApartmentFlat>(true);

            var sortOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "ApartmentId ASC", Text = "Apartman id artan" },
                new SelectListItem { Value = "ApartmentId DESC", Text = "Apartman id azalan" },
                new SelectListItem { Value = "ApartmentFlatId ASC", Text = "Daire id artan" },
                new SelectListItem { Value = "ApartmentFlatId DESC", Text = "Daire id azalan" },
                new SelectListItem { Value = "FlatNumber ASC", Text = "Kat numarasi artan" },
                new SelectListItem { Value = "FlatNumber DESC", Text = "Kat numarasi azalan" },
                new SelectListItem { Value = "ApartmentName ASC", Text = "Apartman adina gore artan" },
                new SelectListItem { Value = "ApartmentName DESC", Text = "Apartman adina gore azalan" },
                new SelectListItem { Value = "UpdatedDate ASC", Text = "Guncelleme tarihine gore artan" },
                new SelectListItem { Value = "UpdatedDate DESC", Text = "Guncelleme tarihine gore azalan" },
            };

            var PagedList = await _unitOfWork.vwApartmentFlatDal.GetDataPagedAsync(null, (int)PageIndex, 25, orderBy ?? defaultSortOrder);


            Dictionary<string, object> Parameters = new()
            {
                { "PageIndex", PageIndex },
            };

            Pagination MyPG = new()
            {
                PageIndex = (int)PageIndex,
                pageSize = 25,
                TotalPages = PagedList.TotalPages,
                TotalRecords = PagedList.TotalRecords,
                HasPreviousPage = PagedList.HasPreviousPage,
                HasNextPage = PagedList.HasNextPage
            };

            Index_VM MYRESULT = new()
            {
                PageTitle = "Daireler",
                PagedList = PagedList,
                Parameters = Parameters,
                MyPagination = MyPG,
                SortOptions = sortOptions,
                OrderBy = orderBy
            };

            return View(MYRESULT);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(int apartmentId, ApartmentFlatAddDto apartmentFlatAddDto)
        {
            List<ApartmentFlat> flats = (List<ApartmentFlat>)_unitOfWork.apartmentFlatDal.GetAll(x=>x.ApartmentId == apartmentId);
            foreach (var flat in flats)
            {
                if (flat.FlatNumber == apartmentFlatAddDto.FlatNumber)
                {
                    ModelState.AddModelError("FlatNumber", "Bu daire numarası zaten kayıtlı.");
                    return View(apartmentFlatAddDto);
                }
            }

            Member newTenant = null;
            if (apartmentFlatAddDto.ResponsibleMemberInfo.NameSurname != null 
                && apartmentFlatAddDto.ResponsibleMemberInfo.PhoneNumber != null
                && apartmentFlatAddDto.ResponsibleMemberInfo.Email       != null)
            {
                 newTenant = new Member
                {
                    NameSurname = apartmentFlatAddDto.ResponsibleMemberInfo.NameSurname,
                    Email = apartmentFlatAddDto.ResponsibleMemberInfo.Email,
                    PhoneNumber = apartmentFlatAddDto.ResponsibleMemberInfo.PhoneNumber,
                    ApartmentId = apartmentId
                };
                    _unitOfWork.memberDal.Add(newTenant);
                    _unitOfWork.Commit();
            }
            
            var newFlatOwner = new Member
            {
                NameSurname = apartmentFlatAddDto.FlatOwner.NameSurname,
                Email = apartmentFlatAddDto.FlatOwner.Email,
                PhoneNumber = apartmentFlatAddDto.FlatOwner.PhoneNumber,  
            };
            _unitOfWork.memberDal.Add(newFlatOwner);
            _unitOfWork.Commit();


            var newApartmentFlat = new ApartmentFlat
            {
                ApartmentId = apartmentFlatAddDto.ApartmentId,
                CarPlate = apartmentFlatAddDto.CarPlate,
                Code = apartmentFlatAddDto.Code,
                FlatNumber = apartmentFlatAddDto.FlatNumber,
                Floor = apartmentFlatAddDto.Floor,
                FlatOwnerId = newFlatOwner.MemberId,
                TenantId = newTenant.MemberId != null ? newTenant.MemberId : 0
            };
            

            if (apartmentFlatAddDto.IsFlatOwnerAndResident)
            {
                newApartmentFlat.TenantId = newFlatOwner.MemberId;
                newFlatOwner.ApartmentId = apartmentId;
            }
            else newFlatOwner.ApartmentId = 0;


            _unitOfWork.apartmentFlatDal.Add(newApartmentFlat);
            _unitOfWork.Commit();

            var apartment = _unitOfWork.apartmentDal.GetById(apartmentId);
            apartment.NumberOfFlats += 1;
            _unitOfWork.apartmentDal.Update(apartment);


            TempData["updateSuccess"] = true;
            return View(apartmentFlatAddDto);
        }
        [HttpGet]
        public IActionResult Update(Guid guid)
        {
            var result = _unitOfWork.apartmentFlatDal.Get(x => x.Guid == guid);
            var tenant = _unitOfWork.memberDal.GetById(result.TenantId);
            var flatOwner = _unitOfWork.memberDal.GetById(result.FlatOwnerId);

            var apartmentFlat = new ApartmentFlatUpdateDto
            {
                ApartmentFlatId = result.ApartmentFlatId,
                Guid = result.Guid,
                CarPlate = result.CarPlate,
                FlatNumber = result.FlatNumber,
                Floor = result.Floor,
                ResponsibleMemberInfo = tenant?.NameSurname != null ? new MemberShortDto
                {
                    NameSurname = tenant.NameSurname,
                    Email = tenant.Email,
                    PhoneNumber = tenant.PhoneNumber
                } : null,
                FlatOwner = new Member
                {
                    NameSurname = flatOwner.NameSurname,
                    Email = flatOwner.Email,
                    PhoneNumber = flatOwner.PhoneNumber
                },
            };

            return View (apartmentFlat);
        }
        public class Index_VM
        {
            public string PageTitle { get; set; }
            public List<VwApartmentFlat>? PagedList { get; set; }
            public List<Apartment>? ListApartment { get; set; }
            public Pagination? MyPagination { get; set; }
            public Dictionary<string, object>? Parameters { get; set; }
            public List<SelectListItem>? SortOptions { get; set; }
            public string? OrderBy { get; set; }
            public int? ApartmentId { get; set; }
        }
    }
}
