using Core.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Concrete.EntityFramework.Context;

public partial class Trasnsaction : IEntity
{
    public int TransactionId { get; set; }

    public Guid Guid { get; set; }

    public string Code { get; set; } = null!;

    public int MainId { get; set; }

    public int TargetId { get; set; }

    public string? TypeName { get; set; }

    public string? Description { get; set; }

    public double? Price { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedDate { get; set; }

    public bool IsPaid { get; set; }
}
