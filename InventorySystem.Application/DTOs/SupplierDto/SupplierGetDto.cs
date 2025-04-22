
namespace InventorySystem.Application.DTOs.SupplierDto
{
    public class SupplierGetDto
    {
        public Guid Id { get; set; }
        public string? ContactInfo { get; set; }
        public string? Address { get; set; }
        public string? ContactEmail { get; set; }

    }
}
