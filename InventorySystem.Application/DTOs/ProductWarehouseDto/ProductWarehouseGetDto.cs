
namespace InventorySystem.Application.DTOs.ProductWarehouseDto
{
    public class ProductWarehouseGetDto
    {
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        public Guid WarehouseId { get; set; }
        public string? WarehouseName { get; set; }
        public int Quantity { get; set; }
    }
}
