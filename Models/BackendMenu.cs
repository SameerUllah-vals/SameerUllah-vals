using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class BackendMenu
{
    public int Id { get; set; }

    public int? ParentId { get; set; }

    public int? PackageId { get; set; }

    public string Type { get; set; } = null!;

    public bool? IsDefault { get; set; }

    public string Title { get; set; } = null!;

    public string AccessUrl { get; set; } = null!;

    public string IconClass { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public virtual ICollection<BackendMenuDetail> BackendMenuDetails { get; } = new List<BackendMenuDetail>();

    public virtual ICollection<BackendMenu> InverseParent { get; } = new List<BackendMenu>();

    public virtual Package? Package { get; set; }

    public virtual BackendMenu? Parent { get; set; }

    public virtual ICollection<RoleBackendMenu> RoleBackendMenus { get; } = new List<RoleBackendMenu>();

    public virtual ICollection<SubcriptionModule> SubcriptionModules { get; } = new List<SubcriptionModule>();
}
