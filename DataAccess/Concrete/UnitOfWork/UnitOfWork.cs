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
    public class UnitOfWork : IUnitOfWork2
    {
        private readonly ApartmentDuesManagementContext _context;
        //private IDbContextTransaction _transaction;
        private readonly IApartmentDal _apartmentDal;
        private readonly IVwApartmentDal _apartmentVwDal;
        private readonly IApartmentFlat _apartmentFlat;
        private readonly IVwApartmentFlat _apartmentVwFlat;
        public UnitOfWork(ApartmentDuesManagementContext context, IApartmentDal apartmentDal, IApartmentFlat apartmentFlat, IVwApartmentDal apartmentVwDal, IVwApartmentFlat apartmentVwFlat)
        {
            _context = context;
            _apartmentDal = apartmentDal;
            _apartmentFlat = apartmentFlat;
            _apartmentVwDal = apartmentVwDal;
            _apartmentVwFlat = apartmentVwFlat;
        }

        public IApartmentDal apartmentDal => _apartmentDal;
        public IVwApartmentDal vwApartmentDal => _apartmentVwDal;
        public IApartmentFlat apartmentFlat => _apartmentFlat;
        public IVwApartmentFlat vwApartmentFlat => _apartmentVwFlat;

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
