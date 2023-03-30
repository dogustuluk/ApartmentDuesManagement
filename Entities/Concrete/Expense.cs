using Core.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Concrete.EntityFramework.Context;

public partial class Expense : IEntity
{
    public int ExpenseId { get; set; }

    public Guid Guid { get; set; }

    public string Code { get; set; } = null!;

    public int ExpenseParameterId { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? Description { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedDate { get; set; }

    public double? Price { get; set; }

    public bool? IsActive { get; set; }

    public virtual Parameter ExpenseParameter { get; set; } = null!;
}
