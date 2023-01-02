using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class SubscriptionSchedule
{
    public int Id { get; set; }

    public int SubscriptionId { get; set; }

    public int? PackageId { get; set; }

    public DateTime StartDateTime { get; set; }

    public DateTime EndDateTime { get; set; }

    public string Status { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public DateTime? UpdatedDateTime { get; set; }

    public DateTime? DeletedDateTime { get; set; }

    public DateTime UtcstartDateTime { get; set; }

    public DateTime UtcendDateTime { get; set; }

    public DateTime UtccreatedDateTime { get; set; }

    public DateTime? UtcupdatedDateTime { get; set; }

    public DateTime? UtcdeletedDateTime { get; set; }

    public int CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public int? DeletedBy { get; set; }

    public virtual Package? Package { get; set; }

    public virtual Subscription Subscription { get; set; } = null!;
}
