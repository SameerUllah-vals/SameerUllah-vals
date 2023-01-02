using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class Role
{
    public int Id { get; set; }

    public int? SubscriptionId { get; set; }

    public string Title { get; set; } = null!;

    public string? Type { get; set; }

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

    public virtual ICollection<BackendMenuDetail> BackendMenuDetails { get; } = new List<BackendMenuDetail>();

    public virtual ICollection<Package> Packages { get; } = new List<Package>();

    public virtual ICollection<RoleBackendMenu> RoleBackendMenus { get; } = new List<RoleBackendMenu>();

    public virtual Subscription? Subscription { get; set; }

    public virtual ICollection<User> Users { get; } = new List<User>();
}
