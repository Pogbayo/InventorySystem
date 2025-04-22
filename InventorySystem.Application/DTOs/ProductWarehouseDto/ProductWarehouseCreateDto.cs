namespace InventorySystem.Application.DTOs.ProductWarehouseDto
{
    public class ProductWarehouseCreateDto
    {
        public Guid ProductId { get; set; }
        public Guid WarehouseId { get; set; }
        public int Quantity { get; set; }
    }
}
