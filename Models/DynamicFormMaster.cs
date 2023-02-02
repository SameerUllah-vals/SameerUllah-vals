using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class DynamicFormMaster
{
    public int Id { get; set; }

    public int DynamicFormId { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDateTime { get; set; }

    public string Status { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public virtual ICollection<DynamicFormMasterDetail> DynamicFormMasterDetails { get; } = new List<DynamicFormMasterDetail>();
}
