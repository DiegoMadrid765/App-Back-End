using System.ComponentModel.DataAnnotations;

namespace Back_End.Models
{
    public class City
    {
        
        public int Id { get; set; }
      
        public string name { get; set; }
        public string countryCode { get; set; }
        
    }
}
