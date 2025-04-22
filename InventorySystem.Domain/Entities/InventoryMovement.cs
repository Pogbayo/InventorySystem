using InventorySystem.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Domain.Entities;

public class InventoryMovement
{
    public Guid InventoryMovementId { get; set; }

    [Required]
    public Guid ProductId { get; set; }
    [Required]
    public Guid WarehouseId { get; set; }

    [Required]
    public MovementType MovementType { get; set; } = default!;
    [Required]
    public int QuantityChanged { get; set; }
    public DateTime MovementDate { get; set; } = DateTime.UtcNow;


    public Product? Product { get; set; }
    public Warehouse? Warehouse { get; set; }

    public InventoryMovement() { }
    public InventoryMovement(Guid productId,Guid warehouseId, MovementType movementType, int quantityChanged)
    {
        ProductId = productId;
        WarehouseId = warehouseId;
        MovementType = movementType;
        QuantityChanged = quantityChanged;
    }
}
