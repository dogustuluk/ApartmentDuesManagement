using Core.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Concrete.EntityFramework.Context;

public partial class VwApartmentFlat : IEntity
{
    public int ApartmentFlatId { get; set; }

    public Guid Guid { get; set; }

    public string Code { get; set; } = null!;

    public int ApartmentId { get; set; }

    public string FlatNumber { get; set; } = null!;

    public string ApartmentName { get; set; } = null!;

    public Guid Expr1 { get; set; }

    public int TenantId { get; set; }

    public string? NameSurname { get; set; }

    public string? PhoneNumber { get; set; }

    public int FlatOwnerId { get; set; }

    public string Expr2 { get; set; } = null!;

    public string? Expr3 { get; set; }

    public string? CarPlate { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedDate { get; set; }
}
