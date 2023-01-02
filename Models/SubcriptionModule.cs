using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class SubcriptionModule
{
    public Guid Id { get; set; }

    public int SubcriptionId { get; set; }

    public int BackendMenuId { get; set; }

    public virtual BackendMenu BackendMenu { get; set; } = null!;

    public virtual Subscription Subcription { get; set; } = null!;
}
