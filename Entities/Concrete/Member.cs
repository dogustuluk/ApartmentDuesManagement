using Core.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Concrete.EntityFramework.Context;

public partial class Member : IEntity
{
    public int MemberId { get; set; }

    public int RoleId { get; set; }

    public Guid Guid { get; set; }

    public string? NameSurname { get; set; }

    public string? Email { get; set; }

    public bool EmailConfirmed { get; set; }

    public byte[]? PasswordSalt { get; set; }

    public byte[]? PasswordHash { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<ApartmentFlat> ApartmentFlats { get; } = new List<ApartmentFlat>();

    public virtual Role? Role { get; set; }
}
