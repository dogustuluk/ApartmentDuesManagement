using Core.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Concrete.EntityFramework.Context;

public partial class VwApartmentFlat : IEntity
{
    public int ApartmentFlatId { get; set; }

    public Guid Guid { get; set; }

    public string? Code { get; set; }

    public int ApartmentId { get; set; }

    public string? FlatNumber { get; set; }

    public string? ApartmentName { get; set; }

    public Guid ApartmentGuid { get; set; }

    public int TenantId { get; set; }

    public string? NameSurname { get; set; }

    public string? PhoneNumber { get; set; }

    public int FlatOwnerId { get; set; }

    public string? FlatOwnerNameSurname { get; set; }

    public string? FlatOwnerPhoneNumber { get; set; }

    public string? CarPlate { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedDate { get; set; }
}
