using InventorySystem.Application.Filter.BaseFilter;

namespace InventorySystem.Application.Filter.AuditLogFilter
{
    public class AuditLogFilter : BaseFilterClass
    {
        public string? SearchItem { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Action { get; set; }
    }
}
