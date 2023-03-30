using Core.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Concrete.EntityFramework.Context;

public partial class County : IEntity
{
    public int CountyId { get; set; }

    public int RefNo { get; set; }

    public string? CountyName { get; set; }

    public int CityId { get; set; }

    public double Koorx { get; set; }

    public double Koory { get; set; }

    public virtual City City { get; set; } = null!;
}
