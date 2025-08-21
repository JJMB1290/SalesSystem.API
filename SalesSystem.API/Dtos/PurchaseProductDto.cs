namespace SalesSystem.API.Dtos
{
    public class PurchaseProductDto
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
