using InventorySystem.Application.Filter.BaseFilter;

namespace InventorySystem.Application.Filter.InventoryFilter
{
    public class InventoryFilter : BaseFilterClass
    {
        public DateTime? StartDate { get; set; }
        public string? ProductName { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
