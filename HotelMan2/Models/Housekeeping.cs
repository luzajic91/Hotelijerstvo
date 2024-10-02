using System;
using System.Collections.Generic;

namespace HotelMan2.Models;

public partial class Housekeeping
{
    public int HousekeepingId { get; set; }

    public Guid PersonId { get; set; }

    public Guid RoomId { get; set; }

    public string Description { get; set; } = null!;

    public DateOnly HousekeepingDate { get; set; }

    public virtual Person Person { get; set; } = null!;

    public virtual Room Room { get; set; } = null!;
}
