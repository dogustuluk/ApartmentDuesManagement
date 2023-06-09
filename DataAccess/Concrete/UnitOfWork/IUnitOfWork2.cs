﻿using Core.Entities;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.UnitOfWork
{
    public interface IUnitOfWork2
    {
        IApartmentDal apartmentDal { get; }
        IApartmentFlat apartmentFlat { get; }
        Task CommitAsync();
        void Commit();
        //Task<int> SaveChangesAsync();
        //Task BeginTransactionAsync();
        //Task CommitTransactionAsync();
        //Task RollbackTransactionAsync();

        //public interface IEntityRepository<T> where T : class, IEntity, new()
        //{
        //    Task AddRangeAsync(IEnumerable<T> entities);
        //}

        //public interface IApartmentRepository : IEntityRepository<Apartment> { }
    }
}
