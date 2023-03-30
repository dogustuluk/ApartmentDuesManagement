using Core.Entities;
using Entities.Concrete.EntityFramework.Context;
using System;
using System.Collections.Generic;

namespace DataAccess.Concrete.EntityFramework.Context;

public partial class ApartmentFlat : IEntity
{
    public int ApartmentFlatId { get; set; }

    public Guid Guid { get; set; }

    public string Code { get; set; } = null!;

    public string FlatNumber { get; set; } = null!;

    public int ApartmentId { get; set; }

    public int TenantId { get; set; }

    public int FlatOwnerId { get; set; }

    public string Floor { get; set; } = null!;

    public string? CarPlate { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedDate { get; set; }

    public virtual Apartment Apartment { get; set; } = null!;

    public virtual Member FlatOwner { get; set; } = null!;

    public virtual ICollection<Subscription> Subscriptions { get; } = new List<Subscription>();
}
