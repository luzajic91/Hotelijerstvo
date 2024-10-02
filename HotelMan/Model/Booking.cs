namespace HotelMan.Model
{
    public class Booking
    {
        public Guid booking_id {  get; set; }
        public DateOnly check_in { get; set; }
        public DateOnly check_out { get; set; }
        public decimal total_ammount { get; set; }
        public Guid person_id { get; set; }
        public Guid room_id { get; set; }
    }
}
