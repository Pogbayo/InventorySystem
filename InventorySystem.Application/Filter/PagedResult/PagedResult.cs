namespace InventorySystem.Application.Filter.PagedResult
{
    public class PagedResult<T>
    {
        public List<T>? Data { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

}
