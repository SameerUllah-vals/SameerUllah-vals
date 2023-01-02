using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class RoleBackendMenu
{
    public int Id { get; set; }

    public int RoleId { get; set; }

    public int MenuId { get; set; }

    public string Permission { get; set; } = null!;

    public int Position { get; set; }

    public virtual BackendMenu Menu { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
