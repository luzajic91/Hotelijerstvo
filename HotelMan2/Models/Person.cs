using System;
using System.Collections.Generic;

namespace HotelMan2.Models;

public partial class Person
{
    public string? UserName { get; set; }

    public string? Password { get; set; }

    public int RoleId { get; set; }

    public string Name { get; set; } = null!;

    public string Surename { get; set; } = null!;

    public string? Email { get; set; }

    public Guid PersonId { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Housekeeping> Housekeepings { get; set; } = new List<Housekeeping>();

    public virtual UserRole Role { get; set; } = null!;
}
