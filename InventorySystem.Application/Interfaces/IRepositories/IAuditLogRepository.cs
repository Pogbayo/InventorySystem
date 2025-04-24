using InventorySystem.Application.Filter;
using InventorySystem.Domain.Entities;

namespace InventorySystem.Application.Interfaces.IRepositories
{
    public interface IAuditLogRepository
    {
        Task AddLogAsync(AuditLog log);
        Task<List<AuditLog>> GetPagedAuditLogsAsync(AuditLogFilter filter);

    }
}
