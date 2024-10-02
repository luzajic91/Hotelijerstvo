

namespace HotelMan2.Models.DataTransferObject
{
    public class RoomDto
    {
        public int RoomTypeId { get; set; }
        public string RoomNumber { get; set; } = null!;
        public int StatusId { get; set; }
        public Guid? RoomId { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public virtual ICollection<Housekeeping> Housekeepings { get; set; } = new List<Housekeeping>();
        public virtual RoomType RoomType { get; set; } = null!;
    }
}
