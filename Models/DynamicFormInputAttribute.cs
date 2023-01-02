using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class DynamicFormInputAttribute
{
    public int Id { get; set; }

    public int DynamicFormInputId { get; set; }

    public string AttrKey { get; set; } = null!;

    public string AttrValue { get; set; } = null!;
}
