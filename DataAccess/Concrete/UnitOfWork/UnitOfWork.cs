using Core.DataAccess;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApartmentDuesManagementContext _context;
        private readonly IApartmentDal _apartmentDal;
        private readonly IVwApartmentDal _apartmentVwDal;
        private readonly IApartmentFlatDal _apartmentFlatDal;
        private readonly IVwApartmentFlatDal _apartmentVwFlatDal;
        private readonly IMemberDal _memberDal;
        private readonly ISubscriptionDal _subscriptionDal;
        private readonly ISubscriptionItemDal _subscriptionItemDal;
        private readonly ICityDal _cityDal;
        private readonly ICountyDal _countyDal;
        public UnitOfWork(ApartmentDuesManagementContext context, IApartmentDal apartmentDal, IApartmentFlatDal apartmentFlat, IVwApartmentDal apartmentVwDal, IVwApartmentFlatDal apartmentVwFlatDal, IMemberDal memberDal, ISubscriptionDal subscriptionDal, ISubscriptionItemDal subscriptionItemDal, ICityDal cityDal, ICountyDal countyDal)
        {
            _context = context;
            _apartmentDal = apartmentDal;
            _apartmentFlatDal = apartmentFlat;
            _apartmentVwDal = apartmentVwDal;
            _apartmentVwFlatDal = apartmentVwFlatDal;
            _memberDal = memberDal;
            _subscriptionDal = subscriptionDal;
            _subscriptionItemDal = subscriptionItemDal;
            _cityDal = cityDal;
            _countyDal = countyDal;
        }

        public IApartmentDal apartmentDal => _apartmentDal;
        public IVwApartmentDal vwApartmentDal => _apartmentVwDal;
        public IApartmentFlatDal apartmentFlatDal => _apartmentFlatDal;
        public IVwApartmentFlatDal vwApartmentFlatDal => _apartmentVwFlatDal;

        public IMemberDal memberDal => _memberDal;

        public ISubscriptionDal subscriptionDal => _subscriptionDal;

        public ISubscriptionItemDal subscriptionItemDal => _subscriptionItemDal;

        public ICityDal cityDal => _cityDal;

        public ICountyDal countyDal => _countyDal;

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
