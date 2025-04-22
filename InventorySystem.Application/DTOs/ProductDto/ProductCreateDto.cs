
namespace InventorySystem.Application.DTOs.ProductDto
{
    public class ProductCreateDto
    {
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SupplierId { get; set; }
    }
}
