using Core.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Concrete.EntityFramework.Context;

public partial class SubscriptionItem : IEntity
{
    public int SubscriptionItemId { get; set; }

    public Guid Guid { get; set; }

    public string Code { get; set; } = null!;

    public string SubscriptionName { get; set; } = null!;

    public double UnitPrice { get; set; }

    public int SubscriptionId { get; set; }

    public virtual Subscription Subscription { get; set; } = null!;
}
