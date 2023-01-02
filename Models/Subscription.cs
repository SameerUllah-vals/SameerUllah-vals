using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class Subscription
{
    public int Id { get; set; }

    public int? CountryId { get; set; }

    public int? StateId { get; set; }

    public int? CityId { get; set; }

    public int? AreaId { get; set; }

    public string TimeZoneId { get; set; } = null!;

    public string RegisterFrom { get; set; } = null!;

    public string Code { get; set; } = null!;

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

    public virtual Area? Area { get; set; }

    public virtual City? City { get; set; }

    public virtual Country? Country { get; set; }

    public virtual ICollection<Role> Roles { get; } = new List<Role>();

    public virtual State? State { get; set; }

    public virtual ICollection<SubcriptionModule> SubcriptionModules { get; } = new List<SubcriptionModule>();

    public virtual ICollection<SubscriptionContactPerson> SubscriptionContactPeople { get; } = new List<SubscriptionContactPerson>();

    public virtual ICollection<SubscriptionSchedule> SubscriptionSchedules { get; } = new List<SubscriptionSchedule>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
