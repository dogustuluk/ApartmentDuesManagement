using Core.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Concrete.EntityFramework.Context;

public partial class Role : IEntity
{
    public int RoleId { get; set; }

    public Guid Guid { get; set; }

    public string Name { get; set; } = null!;

    public bool? IsActive { get; set; }

    public virtual ICollection<Member> Members { get; } = new List<Member>();
}
