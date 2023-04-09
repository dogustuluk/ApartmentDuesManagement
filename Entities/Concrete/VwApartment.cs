using Core.Entities;
using DataAccess.Concrete.EntityFramework.Context;
using System;
using System.Collections.Generic;

namespace DataAccess.Concrete.EntityFramework.Context;

public partial class VwApartment : IEntity
{
    public int ApartmentId { get; set; }

    public Guid Guid { get; set; }

    public string? Code { get; set; }

    public string? ApartmentName { get; set; }

    public string? DoorNumber { get; set; }

    public int ResponsibleMemberId { get; set; }

    public string? NameSurname { get; set; }

    public string? PhoneNumber { get; set; }

    public int NumberOfFlats { get; set; }

    public string? CityName { get; set; }

    public int CityId { get; set; }

    public int CountyId { get; set; }

    public string? CountyName { get; set; }

    public string? OpenAdress { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int IsActive { get; set; }

}
