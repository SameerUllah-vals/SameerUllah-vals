using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class BackendMenuDetail
{
    public int Id { get; set; }

    public int BackendMenuId { get; set; }

    public int RoleId { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public int SequenceOrder { get; set; }

    public virtual BackendMenu BackendMenu { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
