namespace HotelMan.Model
{
    public class Housekeeping
    {
        public int housekeepin_id {  get; set; }
        public Guid person_id { get; set; }
        public Guid room_id { get; set; }
        public string description {  get; set; }
        public DateOnly housekeeping_date { get; set; }
    }
}
