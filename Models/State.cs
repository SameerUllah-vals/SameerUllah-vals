﻿using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class State
{
    public int Id { get; set; }

    public int CountryId { get; set; }

    public string Code { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string AccessUrl { get; set; } = null!;

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

    public virtual ICollection<Area> Areas { get; } = new List<Area>();

    public virtual ICollection<City> Cities { get; } = new List<City>();

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<Subscription> Subscriptions { get; } = new List<Subscription>();
}
