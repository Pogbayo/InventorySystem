using InventorySystem.Domain.Enums;

namespace InventorySystem.Application.DTOs.InventoryMovementDto
{
    public class InventoryMovementCreateDto
    {
        public Guid ProductId { get; set; }
        public Guid WarehouseId { get; set; }
        public int Quantity { get; set; }
        public MovementType MovementType { get; set; }
    }
}
