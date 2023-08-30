namespace Back_End.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string homeadress { get; set; }
        public int userId { get; set; }
        public User user { get; set; }
        public int cityId { get; set; }
        public City city { get; set; }
    }
}
