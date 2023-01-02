using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class Package
{
    public int Id { get; set; }

    public int RoleId { get; set; }

    public string Title { get; set; } = null!;

    public decimal Price { get; set; }

    public string Type { get; set; } = null!;

    public int DurationInMonths { get; set; }

    public int StorageSpaceMegaByte { get; set; }

    public int AllowedJobsCount { get; set; }

    public int AllowedUsersCount { get; set; }

    public int PremiumJobsCount { get; set; }

    public bool ShowPremiumData { get; set; }

    public bool IsDefault { get; set; }

    public string? LogoPath { get; set; }

    public string Status { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public DateTime? UpdatedDateTime { get; set; }

    public DateTime? DeletedDateTime { get; set; }

    public DateTime UtccreatedDateTime { get; set; }

    public DateTime? UtcupdatedDateTime { get; set; }

    public DateTime? UtcdeletedDateTime { get; set; }

    public int CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public int? DeletedBy { get; set; }

    public virtual ICollection<BackendMenu> BackendMenus { get; } = new List<BackendMenu>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<SubscriptionSchedule> SubscriptionSchedules { get; } = new List<SubscriptionSchedule>();
}
