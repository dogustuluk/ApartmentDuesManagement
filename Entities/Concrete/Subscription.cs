using Core.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Concrete.EntityFramework.Context;

public partial class Subscription : IEntity
{
    public int SubscriptionId { get; set; }

    public Guid Guid { get; set; }

    public string Code { get; set; } = null!;

    public int ApartmentFlatId { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedDate { get; set; }

    public double Price { get; set; }

    public virtual ApartmentFlat ApartmentFlat { get; set; } = null!;

    public virtual ICollection<SubscriptionItem> SubscriptionItems { get; } = new List<SubscriptionItem>();
}
