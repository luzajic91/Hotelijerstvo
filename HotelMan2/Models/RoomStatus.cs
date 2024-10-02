using System;
using System.Collections.Generic;

namespace HotelMan2.Models;

public partial class RoomStatus
{
    public int RoomStatusId { get; set; }

    public string RoomStatusName { get; set; } = null!;

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
