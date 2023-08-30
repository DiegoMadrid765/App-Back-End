namespace Back_End.Models
{
    public class Product
    {

        public int Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string imageurl { get; set; }
        public string stock { get; set; }
        public decimal price { get; set; }
        public int userId { get; set; }
        public User user { get; set; }
        public int hidden { get; set; }
    }
}
