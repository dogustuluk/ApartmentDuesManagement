using System;
using System.Collections.Generic;
using Entities.Concrete.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework.Context;

public partial class ApartmentDuesManagementContext : DbContext
{
    public ApartmentDuesManagementContext()
    {
    }

    public ApartmentDuesManagementContext(DbContextOptions<ApartmentDuesManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Apartment> Apartments { get; set; }

    public virtual DbSet<ApartmentFlat> ApartmentFlats { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<County> Counties { get; set; }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Parameter> Parameters { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<SubscriptionItem> SubscriptionItems { get; set; }

    public virtual DbSet<Trasnsaction> Trasnsactions { get; set; }

    public virtual DbSet<VwApartment> VwApartments { get; set; }

    public virtual DbSet<VwApartmentFlat> VwApartmentFlats { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        =>
        optionsBuilder.UseSqlServer("Data Source=DESKTOP-1C9EMR6;Initial Catalog=ApartmentDuesManagement;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Apartment>(entity =>
        {
            entity.HasKey(e => e.ApartmentId).HasName("PK_Apartments");

            entity.ToTable("Apartment");

            entity.Property(e => e.ApartmentName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.BlockNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CountyId).HasColumnName("CountyID");
            entity.Property(e => e.CreatedBy).HasDefaultValueSql("((1))");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");
            entity.Property(e => e.DoorNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Guid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("GUID");
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            entity.Property(e => e.NumberOfFlats).HasDefaultValueSql("((1))");
            entity.Property(e => e.OpenAdress).HasMaxLength(500);
            entity.Property(e => e.ResponsibleMemberId).HasColumnName("ResponsibleMemberID");
            entity.Property(e => e.UpdatedBy).HasDefaultValueSql("((1))");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");
        });

        modelBuilder.Entity<ApartmentFlat>(entity =>
        {
            entity.HasKey(e => e.ApartmentFlatId).HasName("PK_ApartmentFlats");

            entity.ToTable("ApartmentFlat");

            entity.Property(e => e.CarPlate)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy).HasDefaultValueSql("((1))");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");
            entity.Property(e => e.FlatNumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValueSql("((0))");
            entity.Property(e => e.Floor)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.Guid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.UpdatedBy).HasDefaultValueSql("((1))");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");

            entity.HasOne(d => d.Apartment).WithMany(p => p.ApartmentFlats)
                .HasForeignKey(d => d.ApartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApartmentFlat_Apartment");

            entity.HasOne(d => d.FlatOwner).WithMany(p => p.ApartmentFlats)
                .HasForeignKey(d => d.FlatOwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApartmentFlat_Member1");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.ToTable("City");

            entity.Property(e => e.CityId)
                .ValueGeneratedNever()
                .HasColumnName("CityID");
            entity.Property(e => e.CityName).HasMaxLength(50);
            entity.Property(e => e.Koorx).HasColumnName("KOORX");
            entity.Property(e => e.Koory).HasColumnName("KOORY");
            entity.Property(e => e.SortOrderNo).HasDefaultValueSql("((100))");
        });

        modelBuilder.Entity<County>(entity =>
        {
            entity.ToTable("County");

            entity.Property(e => e.CountyId).HasColumnName("CountyID");
            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.CountyName).HasMaxLength(50);
            entity.Property(e => e.Koorx).HasColumnName("KOORX");
            entity.Property(e => e.Koory).HasColumnName("KOORY");

            entity.HasOne(d => d.City).WithMany(p => p.Counties)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_County_City");
        });

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => e.ExpenseId).HasName("PK_Expenses");

            entity.ToTable("Expense");

            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy).HasDefaultValueSql("((1))");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");
            entity.Property(e => e.Description)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Guid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");

            entity.HasOne(d => d.ExpenseParameter).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.ExpenseParameterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Expense_Parameter");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PK_Users");

            entity.ToTable("Member");

            entity.Property(e => e.CreatedBy).HasDefaultValueSql("((1))");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Guid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.NameSurname)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.RoleId).HasDefaultValueSql("((1))");
            entity.Property(e => e.UpdatedDate).HasColumnType("smalldatetime");

            entity.HasOne(d => d.Role).WithMany(p => p.Members)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Roles");
        });

        modelBuilder.Entity<Parameter>(entity =>
        {
            entity.HasKey(e => e.ParameterId).HasName("PK_ExpenseTypes");

            entity.ToTable("Parameter");

            entity.Property(e => e.Aid).HasColumnName("AID");
            entity.Property(e => e.CreatedBy).HasDefaultValueSql("((1))");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");
            entity.Property(e => e.Guid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("GUID");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("isActive");
            entity.Property(e => e.ParameterName).HasMaxLength(50);
            entity.Property(e => e.ParamsJson)
                .HasMaxLength(1000)
                .HasColumnName("ParamsJSON");
            entity.Property(e => e.TypeName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK_Roles");

            entity.ToTable("Role");

            entity.Property(e => e.Guid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Name).HasMaxLength(70);
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.SubscriptionId).HasName("PK_Subscriptions");

            entity.ToTable("Subscription");

            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy).HasDefaultValueSql("((1))");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");
            entity.Property(e => e.Guid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.UpdatedBy).HasDefaultValueSql("((1))");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");

            entity.HasOne(d => d.ApartmentFlat).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.ApartmentFlatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Subscriptions_ApartmentFlats");
        });

        modelBuilder.Entity<SubscriptionItem>(entity =>
        {
            entity.HasKey(e => e.SubscriptionItemId).HasName("PK_SubscriptionTypes");

            entity.ToTable("SubscriptionItem");

            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Guid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.SubscriptionName)
                .HasMaxLength(90)
                .IsUnicode(false);

            entity.HasOne(d => d.Subscription).WithMany(p => p.SubscriptionItems)
                .HasForeignKey(d => d.SubscriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubscriptionItem_Subscription");
        });

        modelBuilder.Entity<Trasnsaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK_Case");

            entity.ToTable("Trasnsaction");

            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy).HasDefaultValueSql("((1))");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");
            entity.Property(e => e.Description)
                .HasMaxLength(400)
                .IsUnicode(false);
            entity.Property(e => e.Guid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.TypeName)
                .HasMaxLength(90)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedBy).HasDefaultValueSql("((1))");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");
        });

        modelBuilder.Entity<VwApartment>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_Apartment");

            entity.Property(e => e.ApartmentName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.CityName).HasMaxLength(50);
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CountyId).HasColumnName("CountyID");
            entity.Property(e => e.CountyName).HasMaxLength(50);
            entity.Property(e => e.DoorNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.NameSurname)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.OpenAdress).HasMaxLength(500);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.ResponsibleMemberId).HasColumnName("ResponsibleMemberID");
            entity.Property(e => e.UpdatedDate).HasColumnType("smalldatetime");
        });

        modelBuilder.Entity<VwApartmentFlat>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_ApartmentFlat");

            entity.Property(e => e.ApartmentName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CarPlate)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FlatOwnerNameSurname)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.FlatOwnerPhoneNumber)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.FlatNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.NameSurname)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedDate).HasColumnType("smalldatetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
