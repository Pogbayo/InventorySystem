using InventorySystem.Application.Filter.AuditLogFilter;
using InventorySystem.Application.Filter.PagedResult;
using InventorySystem.Domain.Entities;

namespace InventorySystem.Application.Interfaces.IRepositories
{
    public interface IAuditLogRepository
    {
        Task<bool> AddLogAsync(AuditLog log);
        Task<PagedResult<AuditLog>> GetPagedAuditLogsAsync(AuditLogFilter filter);

    }
}
