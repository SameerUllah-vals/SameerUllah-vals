using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class DynamicForm
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

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

    public virtual ICollection<DynamicFormInput> DynamicFormInputs { get; } = new List<DynamicFormInput>();
}
