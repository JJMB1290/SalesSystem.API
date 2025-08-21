namespace SalesSystem.API.Dtos
{
    public class AssignProductToStoreDto
    {
        public int ProductId { get; set; }
        public int StoreId { get; set; }
        public DateTime AssignedDate { get; set; }
    }
}
