using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class DynamicFormMasterDetail
{
    public int Id { get; set; }

    public int DynamicFormMasterId { get; set; }

    public int DynamicFormInputId { get; set; }

    public string? DynamicFormInputValue { get; set; }

    public virtual DynamicFormMaster DynamicFormMaster { get; set; } = null!;
}
