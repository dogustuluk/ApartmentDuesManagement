using Business.Abstract;
using Core.DataAccess;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using DataAccess.Concrete.UnitOfWork;
using Entities.Concrete.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ApartmentManager : IApartmentService
    {
        private readonly IApartmentDal _apartmentDal;
        private readonly IUnitOfWork2 _unitOfWork2;
        //IApartmentDal'ı unıt of work'e aktar, burda unıt of work ıle tek seferde erıs.
        //_unitOfWork._apartmentDal.GetList() şeklınde
        public ApartmentManager(IApartmentDal apartmentDal, IUnitOfWork2 unitOfWork2)
        {
            _apartmentDal = apartmentDal;
            _unitOfWork2 = unitOfWork2;
        }

        public IDataResult<List<Apartment>> GetList()
        {
            return new SuccessDataResult<List<Apartment>>(_unitOfWork2.apartmentDal.GetList(), "Apartmanlar basarili bir sekilde listelendi.");
        }

        public async Task<IDataResult<Apartment>> GetApartmentById(int id)
        {
            var apartment = await _unitOfWork2.apartmentDal.GetByIdAsync(id);
            if (apartment == null)
            {
                return new ErrorDataResult<Apartment>("İstenen apartman id'si ile ilişkili bir veri bulunamadı");
            }
            return new SuccessDataResult<Apartment>(apartment, "Veri başarılı bir şekilde bulundu.");
        }

        public async Task<IDataResult<List<Apartment>>> GetApartmentsWithFlat()
        {
            var query = _unitOfWork2.apartmentDal.GetQueryable(x => x.ApartmentFlats);
            if (query == null)
            {
                return new ErrorDataResult<List<Apartment>>("Bir hata ile karsilasildi");
            }
            return new SuccessDataResult<List<Apartment>>(await query.ToListAsync(), "Apartmanlar ve apartmanlara ait daireler basariyla getirildi.");

            #region 2.yol
            //2.yol. AsSplitQuery() -> bu sekilde sorgu bolunmus sorgu olarak ele alinmaktadir. sorguya dahil edilen iliskisel verilerin yuklenmesini ve sonrasinda ise ayri bir sorgu olarak yurutulmesini saglar. dolayisiyla birden fazla sorgu cagirildiginda veri tabani sorgu performansi artmaktadir. bu yontemi secmek  sorgunun karmasikligina bagli olmalidir, bazi durumlarda daha fazla kaynak tuketebilmektedir.
            //var query = _unitOfWork2.apartmentDal.GetQueryable(x => x.ApartmentFlats);
            //return await query.AsSplitQuery().Include(x => x.ApartmentFlats).ToListAsync();
            #endregion
        }

        public async Task<IDataResult<List<Apartment>>> GetApartmentsWithFlatAndMember()
        {
            #region eski
            //var query = _unitOfWork2.apartmentDal.DetailsAsync(x => x.ApartmentFlats, x => x.ApartmentFlats.Select(f => f.FlatOwner).ToList());


            //if (query == null)
            //{
            //    return new ErrorDataResult<List<Apartment>>("Bir hata ile karsilasildi");

            //}

            //return new SuccessDataResult<List<Apartment>>(await query, "Apartmanlar ve apartmanlara ait daireler basariyla getirildi.");
            #endregion


            //var query = _unitOfWork2.apartmentDal.DetailsAsync(x => x.ApartmentFlats,x => x.ApartmentFlats.Select(f => f.FlatOwner));

            //var result = await _unitOfWork2.apartmentDal.GetListAsync2(await query);
            //return new SuccessDataResult<List<Apartment>>(result, "Apartmanlar ve apartmanlara ait daireler başarıyla getirildi.");

            // var query2 = _unitOfWork2.apartmentFlat.GetQueryable().Include(x => x.FlatOwner);

            //var combinedQuery = query1.Concat(query2.Cast<Apartment>());
            // var result = await combinedQuery.ToListAsync();

            //IQueryable<Apartment> combineDetails = query1.Concat(query2);
            var query1 = _unitOfWork2.apartmentDal.GetQueryable(x => x.ApartmentFlats).Include(x => x.ApartmentFlats).ThenInclude(x => x.FlatOwner);
            var result = await query1.ToListAsync();
           
            return new SuccessDataResult<List<Apartment>>(result, "Apartmanlar ve apartmanlara ait daireler ile beraber daire sahipleri başarıyla getirildi.");

        }
    }
}
