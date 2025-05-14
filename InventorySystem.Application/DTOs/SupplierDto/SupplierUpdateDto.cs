
namespace InventorySystem.Application.DTOs.SupplierDto
{
    public class SupplierUpdateDto
    {
        public string Name { get; set; } = default!;
        public string ContactInfo { get; set; } = default!;
        public string ContactEmail { get; set; } = default!;
        public string Address { get; set; } = default!;
    }
}
