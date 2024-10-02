using Microsoft.AspNetCore.Identity;

namespace HotelMan2.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
