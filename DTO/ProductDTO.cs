namespace Back_End.DTO
{
    public class ProductDTO
    {
        public string name { get; set; }
        public string description { get; set; }
        public IFormFile image { get; set; }
        public string stock { get; set; }
        public string price { get; set; }
    }
}
