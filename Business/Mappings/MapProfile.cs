using AutoMapper;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete.EntityFramework.Context;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mappings
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Apartment, ApartmentDto>().ReverseMap();
           // CreateMap<Apartment, ApartmentAddDto>().ReverseMap();
           
        }
    }
}
