namespace InventorySystem.Application.DTOs.WarehouseDto
{
    public class WarehouseGetDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
    }
}
