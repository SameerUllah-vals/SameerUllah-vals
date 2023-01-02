using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class SubscriptionContactPerson
{
    public int Id { get; set; }

    public int SubscriptionId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? MobileNumber { get; set; }

    public string? TelephoneNumber { get; set; }

    public string? Extension { get; set; }

    public string EmailAddress { get; set; } = null!;

    public string? Designation { get; set; }

    public string? Department { get; set; }

    public string? Cnicnumber { get; set; }

    public DateTime? CnicissueDate { get; set; }

    public DateTime? CnicexpireDate { get; set; }

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

    public virtual Subscription Subscription { get; set; } = null!;
}
