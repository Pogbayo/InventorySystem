

namespace InventorySystem.Application.DTOs.AuditLogDto
{
    public class AuditLogGetDto
    {
        public Guid Id { get; set; }
        public string? Action { get; set; }
        public string? PerformedBy { get; set; }
        public DateTime PerformedAt { get; set; }
        public string? Details { get; set; }
    }
}
