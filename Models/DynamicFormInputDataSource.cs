using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class DynamicFormInputDataSource
{
    public int Id { get; set; }

    public int DynamicFormInputId { get; set; }

    public string Key { get; set; } = null!;

    public string Value { get; set; } = null!;

    public virtual DynamicFormInput DynamicFormInput { get; set; } = null!;
}
