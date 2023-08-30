using System.ComponentModel.DataAnnotations;

namespace Back_End.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string name { get; set; }
    }
}
