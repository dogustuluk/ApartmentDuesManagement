using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class ApartmentDto : IDto
    {
        public int ApartmentId { get; set; }

        public Guid Guid { get; set; }

        public string? Code { get; set; }

        public string? ApartmentName { get; set; }

        public string? BlockNo { get; set; }

        public string? DoorNumber { get; set; }
        public int NumberOfFlats { get; set; }
        public string? OpenAdress { get; set; }
        public bool IsActive { get; set; }
    }
    public class ApartmentAddDto : IDto
    {
        public string ApartmentName { get; set; }

        public string BlockNo { get; set; }

        public string DoorNumber { get; set; }

        public MemberShortDto? ResponsibleMemberInfo { get; set; }

        public int NumberOfFlats { get; set; }

        public int CityId { get; set; }
        public List<DDL>? CityIdDDL { get; set; }
        public int CountyId { get; set; }
        public List<DDL>? CountyIdDDL { get; set; }
        public string? OpenAdress { get; set; }
        public int IsActive { get; set; }
        public string Code { get { return GenerateApartmentCode(); } }


        public string GenerateApartmentCode()
        {

            var codeBuild = new StringBuilder();
            codeBuild.Append(this.CityId);
            codeBuild.Append("-");
            codeBuild.Append(this.CountyId);
            codeBuild.Append("-");
            if (this.ApartmentName != null)
            {
                codeBuild.Append(this.ApartmentName.Substring(0, Math.Min(5, this.ApartmentName.Length)));
            }
            codeBuild.Append("-");
            codeBuild.Append(this.BlockNo);
            codeBuild.Append("-");
            codeBuild.Append(this.DoorNumber);
            return codeBuild.ToString();
        }
    }
    public class ApartmentUpdateDto : IDto
    {
        public int ApartmentId { get; set; }
        public Guid Guid { get; set; }

        public string? Code { get { return GenerateApartmentCode(); } }

        public string? ApartmentName { get; set; }
        public string? UpdatedApartmentName { get; set; }

        public string? BlockNo { get; set; }

        public string? DoorNumber { get; set; }
        public int NumberOfFlats { get; set; }
        public string? OpenAdress { get; set; }
        public int IsActive { get; set; }
        public MemberShortDto? ResponsibleMemberInfo { get; set; }
        public int MemberId { get; set; }
        public int CityId { get; set; }
        public List<DDL>? CityIdDDL { get; set; }
        public int CountyId { get; set; }
        public List<DDL>? CountyIdDDL { get; set; }
        public string GenerateApartmentCode()
        {

            var codeBuild = new StringBuilder();
            codeBuild.Append(this.CityId);
            codeBuild.Append("-");
            codeBuild.Append(this.CountyId);
            codeBuild.Append("-");
            if (this.ApartmentName != null)
            {
                codeBuild.Append(this.ApartmentName.Substring(0, Math.Min(5, this.ApartmentName.Length)));
            }
            codeBuild.Append("-");
            codeBuild.Append(this.BlockNo);
            codeBuild.Append("-");
            codeBuild.Append(this.DoorNumber);
            return codeBuild.ToString();
        }
    }

}