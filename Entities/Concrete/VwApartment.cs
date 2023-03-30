using Core.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Concrete.EntityFramework.Context;

public partial class VwApartment : IEntity
{
    public int ApartmentId { get; set; }

    public Guid Guid { get; set; }

    public string Code { get; set; } = null!;

    public string ApartmentName { get; set; } = null!;

    public string? DoorNumber { get; set; }

    public int ResponsibleMemberId { get; set; }

    public string? NameSurname { get; set; }

    public string? PhoneNumber { get; set; }

    public int NumberOfFlats { get; set; }

    public string CityName { get; set; } = null!;

    public int CityId { get; set; }

    public int CountyId { get; set; }

    public string? CountyName { get; set; }

    public string? OpenAdress { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int IsActive { get; set; }
}
