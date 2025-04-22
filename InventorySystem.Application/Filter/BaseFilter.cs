
namespace InventorySystem.Application.Filter
{
    public class BaseFilter
    {
        public string SearchItem { get; set; } = default!;
        public int Page { get; set; } = 1;
        public int PageSize = 10;
    }
}
