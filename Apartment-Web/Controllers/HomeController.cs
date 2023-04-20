using Apartment_Web.Models;
using Bogus;
using Business.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using DataAccess.Concrete.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text;

namespace Apartment_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork ;

        public HomeController(IUnitOfWork unitOfWork, ILogger<HomeController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;

        }


        public IActionResult Index()
        {
            //var faker = new Faker();

            //var apartments = _unitOfWork.apartmentDal.GetAll();
            //foreach (var item in apartments)
            //{
            //    int totalAdd = item.ApartmentId % 3 == 0 ? 18 : 10;
            //    for (int i = 0; i < totalAdd; i++)
            //    {
            //        var apartmentFlat = new ApartmentFlat
            //        {
            //            Guid = Guid.NewGuid(),
            //            Code = faker.Random.AlphaNumeric(10),
            //            FlatNumber = (i+1).ToString("00"),
            //            ApartmentId = item.ApartmentId,
            //            TenantId = _unitOfWork.memberDal.GetData(a=> a.RoleId ==2,100, "MemberId ASC").OrderBy(a=>Guid.NewGuid()).FirstOrDefault().MemberId,
            //            FlatOwnerId = _unitOfWork.memberDal.GetData(a => a.RoleId == 2, 100, "MemberId ASC").OrderBy(a => Guid.NewGuid()).FirstOrDefault().MemberId,
            //            Floor = faker.Random.Number(1, 10).ToString(),
            //            CarPlate = faker.Random.AlphaNumeric(8),
            //            CreatedDate = faker.Date.Past(),
            //            CreatedBy = 1,
            //            UpdatedBy = 1,
            //            UpdatedDate = faker.Date.Past(),

            //        };
            //        _unitOfWork.apartmentFlat.Add(apartmentFlat);
            //    }
            //    _unitOfWork.Commit();

            //}

            //var faker = new Faker();

            //var apartments = _unitOfWork.apartmentDal.GetAll();
            //foreach (var item in apartments)
            //{
            //    int totalAdd = item.ApartmentId % 3 == 0 ? 18 : 10;
            //    for (int i = 0; i <= 200; i++)
            //    {
            //        var apartmentFlat = new Member
            //        {
            //            Guid = Guid.NewGuid(),
            //            RoleId = 2,
            //            NameSurname = faker.Name.FullName(),
            //            Email = $"{faker.Name.FullName().Replace(" ", ".")}@gmail.com",
            //            EmailConfirmed = true,
            //            PasswordSalt = Encoding.ASCII.GetBytes(faker.Random.String(10)),
            //            PasswordHash = Encoding.ASCII.GetBytes(faker.Random.String(20)),
            //            PhoneNumber = faker.Phone.PhoneNumber("05#########"),
            //            PhoneNumberConfirmed = true,
            //            TwoFactorEnabled = false,
            //            CreatedBy = 1,
            //            CreatedDate = faker.Date.Past(),
            //            UpdatedBy = Guid.NewGuid(),
            //            UpdatedDate = faker.Date.Past(),
            //            IsActive = true

            //        };
            //        _unitOfWork.memberDal.Add(apartmentFlat);
            //    }
            //    _unitOfWork.Commit();

            //}

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}