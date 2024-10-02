namespace HotelMan.Model
{
    public class Room
    {
        public Guid room_id {  get; set; }
        public string room_number { get; set; }
        public bool status {  get; set; }
        public int room_type_id { get; set; }
    }
}
