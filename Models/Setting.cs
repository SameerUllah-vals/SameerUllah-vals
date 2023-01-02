using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class Setting
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;
}
