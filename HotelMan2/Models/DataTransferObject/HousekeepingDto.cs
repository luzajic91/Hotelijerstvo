namespace HotelMan2.Models.DataTransferObject
{
    public class HousekeepingDto
    {
        public int HousekeepingId { get; set; }

        public Guid PersonId { get; set; }

        public Guid RoomId { get; set; }

        public string Description { get; set; } = null!;

        public DateOnly HousekeepingDate { get; set; }
    }
}
