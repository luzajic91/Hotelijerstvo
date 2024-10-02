

namespace HotelMan2.Models.DataTransferObject
{
    public class BookingDto
    {
        public Guid? BookingId { get; set; }
        public DateOnly CheckIn { get; set; }
        public DateOnly CheckOut { get; set; }
        public decimal TotalAmmount { get; set; }
        public Guid RoomId { get; set; }
        public Guid PersonId { get; set; }
        public Person? Person { get; set; }
        public Room? Room { get; set; }
    }
}
