namespace HotelMan.Model
{
    public class Person
    {
        public Guid person_id { get; set; }
        public string name { get; set; }
        public string surename { get; set; }
        public string? user_name { get; set; }
        public string? password { get; set; }
        public int role_id  { get; set; }
        public string? email { get; set; }

    }
}
