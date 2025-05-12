namespace InventorySystem.Application.Filter.BaseFilter
{
    public class BaseFilterClass
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        //public DateTime? lastUpdateTime { get; set; } = DateTime.UtcNow;
    }
}
