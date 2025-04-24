namespace InventorySystem.Application.Filter.AuditLogFilter
{
    public class AuditLogFilter : BaseFilter
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Action { get; set; }
    }
}
