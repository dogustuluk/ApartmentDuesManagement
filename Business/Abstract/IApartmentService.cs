using Core.DataAccess;
using Core.Entities;
using Core.Utilities.Results.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete.EntityFramework.Context;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IApartmentService
    {
        //Idata result donme
        Task<IEnumerable<Apartment>> GetList();
        Task<IEnumerable<Apartment>> GetList(Expression<Func<Apartment, bool>> filter = null);

        Task<IDataResult<Apartment>> GetApartmentById(int id);
        Task<IDataResult<List<Apartment>>> GetApartmentsWithFlat();
        Task<IDataResult<List<Apartment>>> GetApartmentsWithFlatAndMember();
    }
}
