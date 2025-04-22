using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Domain.Entities;

public class AuditLog
{
    public Guid AuditLogId { get; set; }
    public Guid PerformedBy { get; set; } = default!;
    public string Action { get; set; } = default!;
    public string Details { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    public AuditLog() { }

    public AuditLog( Guid performedBy, string action, string details)
    {
        PerformedBy = performedBy;
        Action = action;
        Details = details;
    }
}
