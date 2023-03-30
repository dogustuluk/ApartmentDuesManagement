using Core.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Concrete.EntityFramework.Context;

public partial class Parameter : IEntity
{
    public int ParameterId { get; set; }

    public int Aid { get; set; }

    public Guid Guid { get; set; }

    public string? TypeName { get; set; }

    public string? ParameterName { get; set; }

    public bool ParameterStatus { get; set; }

    public string? ParamsJson { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedDate { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Expense> Expenses { get; } = new List<Expense>();
}
