using Core.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Concrete.EntityFramework.Context;

public partial class City : IEntity
{
    public int CityId { get; set; }

    public int RefNo { get; set; }

    public string CityName { get; set; } = null!;

    public double Koorx { get; set; }

    public double Koory { get; set; }

    public int SortOrderNo { get; set; }

    public virtual ICollection<County> Counties { get; } = new List<County>();
}
