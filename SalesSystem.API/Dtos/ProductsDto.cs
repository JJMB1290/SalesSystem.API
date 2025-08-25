using System.Text.Json.Serialization;

namespace SalesSystem.API.Dtos
{
    public class ProductsDto
    {
        public int productId { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public string? image { get; set; }
        public int stock { get; set; }
    }
}
