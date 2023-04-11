using AutoMapper;
using Business.Abstract;
using Core.DataAccess;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using DataAccess.Concrete.UnitOfWork;
using Entities.Concrete.EntityFramework.Context;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ApartmentManager : IApartmentService
    {
        //private readonly IApartmentDal _apartmentDal;
        private readonly IUnitOfWork2 _unitOfWork2;
        private readonly IMapper _mapper;
        //IApartmentDal'ı unıt of work'e aktar, burda unıt of work ıle tek seferde erıs.
        //_unitOfWork._apartmentDal.GetList() şeklınde
        public ApartmentManager(IUnitOfWork2 unitOfWork2, IMapper mapper)
        {
            _unitOfWork2 = unitOfWork2;
            _mapper = mapper;
        }

        //public List<VwApartment> GetList()
        //{
        //    //var apartments = _unitOfWork2.apartmentDal.GetList();
        //    //var result = _mapper.Map<List<ApartmentDto>>(apartments);
        //    //return new SuccessDataResult<List<ApartmentDto>>(result, "Apartmanlar basarili bir sekilde listelendi.");
        //    var apartments = _unitOfWork2.apartmentDal.GetList();
        //    return new List<VwApartment>(apartments);
        //}

        async Task<IEnumerable<Apartment>> IApartmentService.GetList()
        {
            var apartments = _unitOfWork2.apartmentDal.GetList();
                return new List<Apartment>(apartments);
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
            var query1 = _unitOfWork2.apartmentDal.GetQueryable(x => x.ApartmentFlats).Include(x => x.ApartmentFlats).ThenInclude(x => x.FlatOwner);
            var result = await query1.ToListAsync();
           
            return new SuccessDataResult<List<Apartment>>(result, "Apartmanlar ve apartmanlara ait daireler ile beraber daire sahipleri başarıyla getirildi.");

        }

        public async Task<IEnumerable<Apartment>> GetList(Expression<Func<Apartment, bool>> filter = null)
        {
            var apartments = _unitOfWork2.apartmentDal.GetList(filter);
            return apartments;
        }
    }
}
