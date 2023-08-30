namespace Back_End.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public User user { get; set; }
        public DateTime DatePurchase { get; set; }
        public int productId { get; set; }
        public Product product { get; set; }
    }
}
