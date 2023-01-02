using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Web.Models;

public partial class ValsTechnologiesContext : DbContext
{
    public ValsTechnologiesContext()
    {
    }

    public ValsTechnologiesContext(DbContextOptions<ValsTechnologiesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<BackendMenu> BackendMenus { get; set; }

    public virtual DbSet<BackendMenuDetail> BackendMenuDetails { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Package> Packages { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleBackendMenu> RoleBackendMenus { get; set; }

    public virtual DbSet<Setting> Settings { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<SubcriptionModule> SubcriptionModules { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<SubscriptionContactPerson> SubscriptionContactPersons { get; set; }

    public virtual DbSet<SubscriptionSchedule> SubscriptionSchedules { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
     /*optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS01;Database=ValsTechnologies;integrated security=true;TrustServerCertificate=True");*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Area>(entity =>
        {
            entity.Property(e => e.AccessUrl)
                .HasMaxLength(500)
                .HasColumnName("AccessURL");
            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.DeletedDateTime).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(10);
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.UtccreatedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("UTCCreatedDateTime");
            entity.Property(e => e.UtcdeletedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("UTCDeletedDateTime");
            entity.Property(e => e.UtcupdatedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("UTCUpdatedDateTime");

            entity.HasOne(d => d.City).WithMany(p => p.Areas)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Areas_Cities");

            entity.HasOne(d => d.Country).WithMany(p => p.Areas)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Areas_Countries");

            entity.HasOne(d => d.State).WithMany(p => p.Areas)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Areas_States");
        });

        modelBuilder.Entity<BackendMenu>(entity =>
        {
            entity.Property(e => e.AccessUrl)
                .HasMaxLength(100)
                .HasColumnName("AccessURL");
            entity.Property(e => e.CreatedDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IconClass).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(10);
            entity.Property(e => e.Title).HasMaxLength(100);
            entity.Property(e => e.Type).HasMaxLength(10);

            entity.HasOne(d => d.Package).WithMany(p => p.BackendMenus)
                .HasForeignKey(d => d.PackageId)
                .HasConstraintName("FK_BackendMenus_Packages");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK_BackendMenus_BackendMenus");
        });

        modelBuilder.Entity<BackendMenuDetail>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(5);

            entity.HasOne(d => d.BackendMenu).WithMany(p => p.BackendMenuDetails)
                .HasForeignKey(d => d.BackendMenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BackendMenuDetails_BackendMenus");

            entity.HasOne(d => d.Role).WithMany(p => p.BackendMenuDetails)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BackendMenuDetails_Roles");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.Property(e => e.AccessUrl)
                .HasMaxLength(500)
                .HasColumnName("AccessURL");
            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.DeletedDateTime).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(10);
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.UtccreatedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("UTCCreatedDateTime");
            entity.Property(e => e.UtcdeletedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("UTCDeletedDateTime");
            entity.Property(e => e.UtcupdatedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("UTCUpdatedDateTime");

            entity.HasOne(d => d.Country).WithMany(p => p.Cities)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cities_Countries");

            entity.HasOne(d => d.State).WithMany(p => p.Cities)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cities_States");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.Property(e => e.AccessUrl)
                .HasMaxLength(255)
                .HasColumnName("AccessURL");
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.CodePhoneNumber).HasMaxLength(20);
            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.DeletedDateTime).HasColumnType("datetime");
            entity.Property(e => e.IconImage).HasMaxLength(500);
            entity.Property(e => e.Image).HasMaxLength(500);
            entity.Property(e => e.Status).HasMaxLength(10);
            entity.Property(e => e.Title).HasMaxLength(100);
            entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.UtccreatedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("UTCCreatedDateTime");
            entity.Property(e => e.UtcdeletedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("UTCDeletedDateTime");
            entity.Property(e => e.UtcupdatedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("UTCUpdatedDateTime");
        });

        modelBuilder.Entity<Package>(entity =>
        {
            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.DeletedDateTime).HasColumnType("datetime");
            entity.Property(e => e.LogoPath).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.Type).HasMaxLength(20);
            entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.UtccreatedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("UTCCreatedDateTime");
            entity.Property(e => e.UtcdeletedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("UTCDeletedDateTime");
            entity.Property(e => e.UtcupdatedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("UTCUpdatedDateTime");

            entity.HasOne(d => d.Role).WithMany(p => p.Packages)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Packages_Roles");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.DeletedDateTime).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.Type).HasMaxLength(20);
            entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.UtccreatedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("UTCCreatedDateTime");
            entity.Property(e => e.UtcdeletedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("UTCDeletedDateTime");
            entity.Property(e => e.UtcupdatedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("UTCUpdatedDateTime");

            entity.HasOne(d => d.Subscription).WithMany(p => p.Roles)
                .HasForeignKey(d => d.SubscriptionId)
                .HasConstraintName("FK_Roles_Subscriptions");
        });

        modelBuilder.Entity<RoleBackendMenu>(entity =>
        {
            entity.Property(e => e.Permission).HasMaxLength(50);

            entity.HasOne(d => d.Menu).WithMany(p => p.RoleBackendMenus)
                .HasForeignKey(d => d.MenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoleBackendMenus_BackendMenus");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleBackendMenus)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoleBackendMenus_Roles");
        });

        modelBuilder.Entity<Setting>(entity =>
        {
            entity.Property(e => e.Content).HasColumnType("text");
            entity.Property(e => e.Title).HasMaxLength(500);
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.Property(e => e.AccessUrl)
                .HasMaxLength(500)
                .HasColumnName("AccessURL");
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.DeletedDateTime).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(10);
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.UtccreatedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("UTCCreatedDateTime");
            entity.Property(e => e.UtcdeletedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("UTCDeletedDateTime");
            entity.Property(e => e.UtcupdatedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("UTCUpdatedDateTime");

            entity.HasOne(d => d.Country).WithMany(p => p.States)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_States_Countries");
        });

        modelBuilder.Entity<SubcriptionModule>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.BackendMenu).WithMany(p => p.SubcriptionModules)
                .HasForeignKey(d => d.BackendMenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubcriptionModules_Menus");

            entity.HasOne(d => d.Subcription).WithMany(p => p.SubcriptionModules)
                .HasForeignKey(d => d.SubcriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubcriptionModules_Subscriptions");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.Property(e => e.Code).HasMaxLength(500);
            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.DeletedDateTime).HasColumnType("datetime");
            entity.Property(e => e.RegisterFrom).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.TimeZoneId).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(500);
            entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.UtccreatedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("UTCCreatedDateTime");
            entity.Property(e => e.UtcdeletedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("UTCDeletedDateTime");
            entity.Property(e => e.UtcupdatedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("UTCUpdatedDateTime");

            entity.HasOne(d => d.Area).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.AreaId)
                .HasConstraintName("FK_Subscriptions_Areas");

            entity.HasOne(d => d.City).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK_Subscriptions_Cities");

            entity.HasOne(d => d.Country).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK_Subscriptions_Countries");

            entity.HasOne(d => d.State).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK_Subscriptions_States");
        });

        modelBuilder.Entity<SubscriptionContactPerson>(entity =>
        {
            entity.Property(e => e.CnicexpireDate)
                .HasColumnType("date")
                .HasColumnName("CNICExpireDate");
            entity.Property(e => e.CnicissueDate)
                .HasColumnType("date")
                .HasColumnName("CNICIssueDate");
            entity.Property(e => e.Cnicnumber)
                .HasMaxLength(100)
                .HasColumnName("CNICNumber");
            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.DeletedDateTime).HasColumnType("datetime");
            entity.Property(e => e.Department).HasMaxLength(100);
            entity.Property(e => e.Designation).HasMaxLength(100);
            entity.Property(e => e.EmailAddress).HasMaxLength(100);
            entity.Property(e => e.Extension).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.MobileNumber).HasMaxLength(100);
            entity.Property(e => e.Status).HasMaxLength(10);
            entity.Property(e => e.TelephoneNumber).HasMaxLength(100);
            entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.UtccreatedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("UTCCreatedDateTime");
            entity.Property(e => e.UtcdeletedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("UTCDeletedDateTime");
            entity.Property(e => e.UtcupdatedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("UTCUpdatedDateTime");

            entity.HasOne(d => d.Subscription).WithMany(p => p.SubscriptionContactPeople)
                .HasForeignKey(d => d.SubscriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubscriptionContactPersons_Subscriptions");
        });

        modelBuilder.Entity<SubscriptionSchedule>(entity =>
        {
            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.DeletedDateTime).HasColumnType("datetime");
            entity.Property(e => e.EndDateTime).HasColumnType("datetime");
            entity.Property(e => e.StartDateTime).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(10);
            entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.UtccreatedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("UTCCreatedDateTime");
            entity.Property(e => e.UtcdeletedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("UTCDeletedDateTime");
            entity.Property(e => e.UtcendDateTime)
                .HasColumnType("datetime")
                .HasColumnName("UTCEndDateTime");
            entity.Property(e => e.UtcstartDateTime)
                .HasColumnType("datetime")
                .HasColumnName("UTCStartDateTime");
            entity.Property(e => e.UtcupdatedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("UTCUpdatedDateTime");

            entity.HasOne(d => d.Package).WithMany(p => p.SubscriptionSchedules)
                .HasForeignKey(d => d.PackageId)
                .HasConstraintName("FK_SubscriptionSchedules_Packages");

            entity.HasOne(d => d.Subscription).WithMany(p => p.SubscriptionSchedules)
                .HasForeignKey(d => d.SubscriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubscriptionSchedules_Subscriptions");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.AccountType).HasMaxLength(20);
            entity.Property(e => e.ConnectionId).HasColumnType("text");
            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.DeletedDateTime).HasColumnType("datetime");
            entity.Property(e => e.EmailAddress).HasMaxLength(200);
            entity.Property(e => e.FirebaseId).HasColumnType("text");
            entity.Property(e => e.Fullname).HasMaxLength(500);
            entity.Property(e => e.Password).HasMaxLength(200);
            entity.Property(e => e.PasswordRecoveryCode).HasMaxLength(50);
            entity.Property(e => e.PasswordRecoveryExpireDateTime).HasColumnType("datetime");
            entity.Property(e => e.PhoneCountryIso)
                .HasMaxLength(10)
                .HasColumnName("PhoneCountryISO");
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.ProfileImagePath).HasMaxLength(100);
            entity.Property(e => e.RegisteredWith).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.UtccreatedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("UTCCreatedDateTime");
            entity.Property(e => e.UtcdeletedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("UTCDeletedDateTime");
            entity.Property(e => e.UtcupdatedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("UTCUpdatedDateTime");
            entity.Property(e => e.VerificationDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Roles");

            entity.HasOne(d => d.Subcription).WithMany(p => p.Users)
                .HasForeignKey(d => d.SubcriptionId)
                .HasConstraintName("FK_Users_Subscriptions");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
