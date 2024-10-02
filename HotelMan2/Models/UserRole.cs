using System;
using System.Collections.Generic;

namespace HotelMan2.Models;

public partial class UserRole
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
