using Core.Entities;
using DataAccess.Concrete.EntityFramework.Context;
using System;
using System.Collections.Generic;

namespace Entities.Concrete.EntityFramework.Context;

public partial class Apartment : IEntity
{
    public int ApartmentId { get; set; }

    public Guid Guid { get; set; }

    public string? Code { get; set; }

    public string? ApartmentName { get; set; }

    public string? BlockNo { get; set; }

    public string? DoorNumber { get; set; }

    public int ResponsibleMemberId { get; set; }

    public int NumberOfFlats { get; set; }

    public int CityId { get; set; }

    public int CountyId { get; set; }

    public string? OpenAdress { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedDate { get; set; }

    public bool IsActive { get; set; } //int'i bool olarak degistirdim
    public City Cities { get; set; }

    public virtual ICollection<ApartmentFlat> ApartmentFlats { get;} = new List<ApartmentFlat>();
}
