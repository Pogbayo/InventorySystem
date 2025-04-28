using InventorySystem.Application.Filter.BaseFilter;

namespace InventorySystem.Application.Filter.ProductWarehouseFilter
{
    public class ProductWarehouseFilter : BaseFilterClass
    {
        public Guid? ProductId { get; set; }
        public string? ProductName { get; set; }
        public Guid? WarehouseId { get; set; }
        public string? WarehouseName { get; set; }
        public int? MinQuantity { get; set; }
        public int? MaxQuantity { get; set; }
    }
}
