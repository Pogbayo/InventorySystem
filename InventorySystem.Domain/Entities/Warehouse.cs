using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Domain.Entities;

public class Warehouse
{
    public Guid WarehouseId { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = default!;
    public string Location { get; set; } = default!;
    public ICollection<ProductWarehouse> ProductWarehouses { get; set; } = new List<ProductWarehouse>();
    public ICollection<InventoryMovement> InventoryMovements { get; set; } = new List<InventoryMovement>();


    public Warehouse() { }

    public Warehouse(string name,string location)
    {
        Name = name;
        Location = location;
    }
}
