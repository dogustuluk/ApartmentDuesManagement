using Core.DataAccess;
using Core.Entities;
using Core.Utilities.Results.Abstract;
using Entities.Concrete.EntityFramework.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IApartmentService
    {
        IDataResult<List<Apartment>> GetList();
        Task<IDataResult<Apartment>> GetApartmentById(int id);
        Task<IDataResult<List<Apartment>>> GetApartmentsWithFlat();
        Task<IDataResult<List<Apartment>>> GetApartmentsWithFlatAndMember();
        
        

    }
}
