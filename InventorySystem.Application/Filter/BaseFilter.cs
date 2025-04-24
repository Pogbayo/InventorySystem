
namespace InventorySystem.Application.Filter
{
    public class BaseFilter
    {
        public int? Page { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
        public DateTime? lastUpdateTime { get; set; }
    }
}
