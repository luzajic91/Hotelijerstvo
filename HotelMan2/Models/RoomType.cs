using System;
using System.Collections.Generic;

namespace HotelMan2.Models;

public partial class RoomType
{
    public int RoomTypeId { get; set; }

    public string RoomTypeName { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
