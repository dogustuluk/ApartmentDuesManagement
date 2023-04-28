using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            //codeBuild.Append(this.ApartmentName.Substring(0, 3));
            if (this.ApartmentName != null)
            {
                codeBuild.Append(this.ApartmentName.Substring(0, Math.Min(5, this.ApartmentName.Length)));
            }
            codeBuild.Append("-");
            codeBuild.Append(this.BlockNo);
            codeBuild.Append("-");
            codeBuild.Append(this.DoorNumber);
            //codeBuild.Append("-");
            //codeBuild.Append(Guid.NewGuid().ToString("N").Substring(0, 6));
            return codeBuild.ToString();
        }

        #region generate code v1
        //public string GenerateApartmentCode(int length)
        //{
        //    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        //    var random = new Random();
        //    return new string(Enumerable.Repeat(chars, length)
        //        .Select(x => x[random.Next(x.Length)]).ToArray()
        //        );
        //}
        #endregion
    }
    public class ApartmentUpdateDto : IDto
    {
        public int ApartmentId { get; set; }
        public Guid Guid { get; set; }

        public string? Code { get; set; }

        public string? ApartmentName { get; set; }

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

    }


}
