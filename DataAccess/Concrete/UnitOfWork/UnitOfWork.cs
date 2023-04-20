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
        private readonly IApartmentFlat _apartmentFlat;
        private readonly IVwApartmentFlat _apartmentVwFlat;
        private readonly IMemberDal _memberDal;
        private readonly ISubscriptionDal _subscriptionDal;
        private readonly ISubscriptionItemDal _subscriptionItemDal;
        private readonly ICityDal _cityDal;
        public UnitOfWork(ApartmentDuesManagementContext context, IApartmentDal apartmentDal, IApartmentFlat apartmentFlat, IVwApartmentDal apartmentVwDal, IVwApartmentFlat apartmentVwFlat, IMemberDal memberDal, ISubscriptionDal subscriptionDal, ISubscriptionItemDal subscriptionItemDal, ICityDal cityDal)
        {
            _context = context;
            _apartmentDal = apartmentDal;
            _apartmentFlat = apartmentFlat;
            _apartmentVwDal = apartmentVwDal;
            _apartmentVwFlat = apartmentVwFlat;
            _memberDal = memberDal;
            _subscriptionDal = subscriptionDal;
            _subscriptionItemDal = subscriptionItemDal;
            _cityDal = cityDal;
        }

        public IApartmentDal apartmentDal => _apartmentDal;
        public IVwApartmentDal vwApartmentDal => _apartmentVwDal;
        public IApartmentFlat apartmentFlat => _apartmentFlat;
        public IVwApartmentFlat vwApartmentFlat => _apartmentVwFlat;

        public IMemberDal memberDal => _memberDal;

        public ISubscriptionDal subscriptionDal => _subscriptionDal;

        public ISubscriptionItemDal subscriptionItemDal => _subscriptionItemDal;

        public ICityDal cityDal => _cityDal;

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
