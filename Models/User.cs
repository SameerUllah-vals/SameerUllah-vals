using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class User
{
    public int Id { get; set; }

    public int? SubcriptionId { get; set; }

    public int RoleId { get; set; }

    public string? FirebaseId { get; set; }

    public string? ConnectionId { get; set; }

    public string? Fullname { get; set; }

    public string? ProfileImagePath { get; set; }

    public string? PhoneCountryIso { get; set; }

    public string? PhoneNumber { get; set; }

    public string EmailAddress { get; set; } = null!;

    public string? RegisteredWith { get; set; }

    public string Password { get; set; } = null!;

    public string? PasswordRecoveryCode { get; set; }

    public DateTime? PasswordRecoveryExpireDateTime { get; set; }

    public DateTime? VerificationDateTime { get; set; }

    public string Status { get; set; } = null!;

    public string? AccountType { get; set; }

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

    public virtual Role Role { get; set; } = null!;

    public virtual Subscription? Subcription { get; set; }
}
