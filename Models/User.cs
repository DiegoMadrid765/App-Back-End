using System.ComponentModel.DataAnnotations;

namespace Back_End.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string names { get; set; }
        [Required]
        public string lastnames { get; set; }
        [Required]
        public DateTime birthdate { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string gender { get; set; }
        [Required]
        public string email { get; set; }
        public Address address { get; set; }
    }
}
