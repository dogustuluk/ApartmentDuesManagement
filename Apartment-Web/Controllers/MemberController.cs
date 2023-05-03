using DataAccess.Concrete.EntityFramework.Context;
using DataAccess.Concrete.UnitOfWork;
using Entities.Concrete.EntityFramework.Context;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Apartment_Web.Controllers
{
    public class MemberController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddNewResponsibleMember(ApartmentUpdateDto apartmentUpdateDto)
        {
            var responsibleMember = new Member
            {
                NameSurname = apartmentUpdateDto.ResponsibleMemberInfo.NameSurname,
                PhoneNumber = apartmentUpdateDto.ResponsibleMemberInfo.PhoneNumber,
                Email = apartmentUpdateDto.ResponsibleMemberInfo.Email,
                ApartmentId = apartmentUpdateDto.ApartmentId,
            };
            if (responsibleMember != null)
            {
                _unitOfWork.memberDal.Add(responsibleMember);
                _unitOfWork.Commit();

                var apartment = _unitOfWork.apartmentDal.GetById(apartmentUpdateDto.ApartmentId);
                apartment.ResponsibleMemberId = responsibleMember.MemberId;
                _unitOfWork.apartmentDal.Update(apartment);
            }

            return RedirectToAction("Update","Apartment", new { id = apartmentUpdateDto.ApartmentId});
        }
    }
}
