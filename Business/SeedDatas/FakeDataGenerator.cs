using Bogus;
using Bogus.DataSets;
using DataAccess.Concrete.EntityFramework.Context;
using DataAccess.Concrete.UnitOfWork;
using Entities.Concrete.EntityFramework.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.SeedDatas
{
    //bogus, rulefor ile fluent validation ile ilgili kurallari da belirtilerek sahte veri olusturabilir.
    public class FakeDataGenerator
    {
        private readonly IUnitOfWork _unitOfWork;

        public FakeDataGenerator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public static List<Apartment> GenerateApartments(int count)
        {
            var faker = new Faker();

            var apartments = new List<Apartment>();

            for (int i = 0; i < count; i++)
            {
                var apartment = new Apartment
                {
                    Guid = Guid.NewGuid(),
                    Code = faker.Random.AlphaNumeric(8),
                    ApartmentName = faker.Company.CompanyName(),
                    BlockNo = faker.Random.Number(1, 50).ToString(),
                    DoorNumber = faker.Random.Number(1, 80).ToString(),
                    ResponsibleMemberId = faker.Random.Number(1, 10),
                    NumberOfFlats = faker.Random.Number(1, 10),
                    CityId = faker.Random.Number(1, 81),
                    CountyId = faker.Random.Number(1, 200),
                    OpenAdress = faker.Address.FullAddress(),
                    CreatedBy = faker.Random.Number(1, 100),
                    CreatedDate = faker.Date.Past(),
                    UpdatedBy = faker.Random.Number(1, 100),
                    UpdatedDate = faker.Date.Recent(),
                    IsActive = faker.Random.Number(0, 2),
                };

                apartments.Add(apartment);
            }

            return apartments;
        }

        public static List<ApartmentFlat> GenerateApartmentsFlat(int count)
        {
            var faker = new Faker();

            var apartmentsFlat = new List<ApartmentFlat>();
            for (int i = 0; i < count; i++)
            {
                var apartmentFlat = new ApartmentFlat
                {
                    Guid = Guid.NewGuid(),
                    Code = faker.Random.AlphaNumeric(10),
                    FlatNumber = faker.Address.BuildingNumber(),
                    ApartmentId = faker.Random.Number(1, 50),
                    TenantId = faker.Random.Number(1, 2),
                    FlatOwnerId = faker.Random.Number(1, 2),
                    Floor = faker.Random.Number(1, 10).ToString(),
                    CarPlate = faker.Random.AlphaNumeric(8),
                    CreatedDate = faker.Date.Past(),
                    CreatedBy = faker.Random.Number(1, 10),
                    UpdatedBy = faker.Random.Number(1, 10),
                    UpdatedDate = faker.Date.Past(),

                };
                apartmentsFlat.Add(apartmentFlat);
            }

            return apartmentsFlat;
        }
        public static List<Member> GenerateMember(int count)
        {
            var faker = new Faker();

            var members = new List<Member>();

            for (int i = 0; i < count; i++)
            {
                var member = new Member
                {
                    Guid = Guid.NewGuid(),
                    RoleId = faker.Random.Number(1, 2),
                    NameSurname = faker.Name.FullName(),
                    Email = $"{faker.Name.FullName().Replace(" ", ".")}@gmail.com",
                    //Email = faker.Person.Email,
                    EmailConfirmed = true,
                    PasswordSalt = Encoding.ASCII.GetBytes(faker.Random.String(10)),
                    PasswordHash = Encoding.ASCII.GetBytes(faker.Random.String(20)),
                    PhoneNumber = faker.Phone.PhoneNumber("05#########"),
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = false,
                    CreatedBy = faker.Random.Number(1, 10),
                    CreatedDate = faker.Date.Past(),
                    UpdatedBy = Guid.NewGuid(),
                    UpdatedDate = faker.Date.Past(),
                    IsActive = true
                };

                members.Add(member);
            }

            return members;
        }
        public static List<Subscription> GenerateSubscription(int count)
        {
            var faker = new Faker();

            var subscriptions = new List<Subscription>();
            
            for (int i = 0; i < count; i++)
            {
                var subscription = new Subscription
                {
                    Guid = Guid.NewGuid(),
                    Code = faker.Random.AlphaNumeric(8),
                    ApartmentFlatId = faker.Random.Number(8, 57),
                    CreatedBy = faker.Random.Number(1, 10),
                    CreatedDate = faker.Date.Past(),
                    UpdatedBy = faker.Random.Number(1, 10),
                    UpdatedDate = faker.Date.Past(),
                    Price = faker.Random.Double(100, 3000)
                };
                subscriptions.Add(subscription);
            }

            return subscriptions;
        }
        public static List<SubscriptionItem> GenerateSubscriptionItems(int count)
        {
            var faker = new Faker();

            var subscriptionItems = new List<SubscriptionItem>();

            for (int i = 0; i < count; i++)
            {
                var subscription = new SubscriptionItem
                {
                    Guid = Guid.NewGuid(),
                    Code = faker.Random.AlphaNumeric(8),
                    SubscriptionName = faker.Commerce.ProductName(),
                    UnitPrice = faker.Random.Float(100, 3000),
                    SubscriptionId = faker.Random.Number(34,97)
                };
                subscriptionItems.Add(subscription);
            }

            return subscriptionItems;
        }
    }
}
