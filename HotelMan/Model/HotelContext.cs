using Microsoft.EntityFrameworkCore;
namespace HotelMan.Model
{
    public class HotelContext : DbContext
    {
        public HotelContext(DbContextOptions<HotelContext> options) : base(options) { }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Housekeeping> Housekeepings { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
    }
}
