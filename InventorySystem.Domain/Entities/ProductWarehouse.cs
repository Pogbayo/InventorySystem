using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Domain.Entities;

public class ProductWarehouse
{
    public Guid ProductWarehouseId { get; set; }
    [Required]
    public Guid ProductId { get; set; }
    [Required]
    public Guid WarehouseId { get; set; }

    [Required]
    public int Quantity { get; set; }

    public Product Product { get; set; } = default!;
    public Warehouse Warehouse { get; set; } = default!;


    public ProductWarehouse() { }

    public ProductWarehouse(Guid productId,Guid warehouseId)
    {
        ProductId = productId;
        WarehouseId = warehouseId;
    }
}
