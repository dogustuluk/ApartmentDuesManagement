using Core.Entities;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.UnitOfWork
{
    public interface IUnitOfWork
    {
        IApartmentDal apartmentDal { get; }
        IVwApartmentDal vwApartmentDal { get; }
        IApartmentFlat apartmentFlat { get; }
        IVwApartmentFlat vwApartmentFlat { get; }
        IMemberDal memberDal { get; }
        ISubscriptionDal subscriptionDal { get; }
        ISubscriptionItemDal subscriptionItemDal { get; }
        ICityDal cityDal { get; }
        Task CommitAsync();
        void Commit();
    }
}
