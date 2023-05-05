using Core.Entities;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete.EntityFramework.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class ApartmentFlatDto
    {
        public int ApartmentFlatId { get; set; }

        public Guid Guid { get; set; }

        public string? Code { get; set; }

        public string? FlatNumber { get; set; }

        public int ApartmentId { get; set; }

        public int TenantId { get; set; }

        public int FlatOwnerId { get; set; }

        public string? Floor { get; set; }

        public string? CarPlate { get; set; }
    }

    public class ApartmentFlatAddDto
    {
        public string? FlatNumber { get; set; }
        public string? Code { get { return GenerateApartmentCode(); } }
        public int ApartmentId { get; set; }
        public List<DDL>? ApartmentIdDDL { get; set; }

        public int TenantId { get; set; }

        public int FlatOwnerId { get; set; }

        public string? Floor { get; set; }

        public string? CarPlate { get; set; }
        public virtual Apartment? Apartment { get; set; }

        public virtual Member? FlatOwner { get; set; }
        public bool IsFlatOwnerAndResident { get; set; }
        public MemberShortDto? ResponsibleMemberInfo { get; set; }

        public string GenerateApartmentCode()
        {

            var codeBuild = new StringBuilder();
            codeBuild.Append(this.Floor);
            codeBuild.Append("-");
            codeBuild.Append(this.FlatNumber);
            codeBuild.Append("-");
            codeBuild.Append(this.CarPlate);
            return codeBuild.ToString();
        }
    }
    public class ApartmentFlatUpdateDto
    {
        public int ApartmentFlatId { get; set; }
        public Guid Guid { get; set; }
        public string? FlatNumber { get; set; }
        public string? Code { get { return GenerateApartmentCode(); } }
        public int ApartmentId { get; set; }
        public List<DDL>? ApartmentIdDDL { get; set; }

        public int TenantId { get; set; }

        public int FlatOwnerId { get; set; }

        public string? Floor { get; set; }

        public string? CarPlate { get; set; }
        public virtual Apartment? Apartment { get; set; }

        public virtual Member? FlatOwner { get; set; }
        public MemberShortDto? ResponsibleMemberInfo { get; set; }

        public string GenerateApartmentCode()
        {

            var codeBuild = new StringBuilder();
            codeBuild.Append(this.Floor);
            codeBuild.Append("-");
            codeBuild.Append(this.FlatNumber);
            codeBuild.Append("-");
            codeBuild.Append(this.CarPlate);
            return codeBuild.ToString();
        }
    }
}
