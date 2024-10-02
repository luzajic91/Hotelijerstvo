using System;
using System.Collections.Generic;

namespace HotelMan2.Models;

public partial class Booking
{
    public Guid BookingId { get; set; }

    public DateOnly CheckIn { get; set; }

    public DateOnly CheckOut { get; set; }

    public decimal TotalAmmount { get; set; }

    public Guid RoomId { get; set; }

    public Guid PersonId { get; set; }

    public virtual Person Person { get; set; } = null!;

    public virtual Room Room { get; set; } = null!;
}
