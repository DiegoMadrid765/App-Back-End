namespace Back_End.Models
{
    public class ResetPassword
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int userId { get; set; }
        public User User { get; set; }
    }
}
