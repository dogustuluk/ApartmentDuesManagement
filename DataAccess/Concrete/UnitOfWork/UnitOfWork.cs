using Core.DataAccess;
using DataAccess.Concrete.EntityFramework.Context;
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

        public UnitOfWork(ApartmentDuesManagementContext context)
        {
            _context = context;
        }

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
