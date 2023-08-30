namespace Back_End.Models
{
    public class ActivatedUser
    {
        public int Id { get; set; }
        public string url { get; set; }
        public int activated { get; set; }
        public int userId { get; set; }
        public User user { get; set; }
    }
}
