using InventorySystem.Application.Filter.AuditLogFilter;
using InventorySystem.Application.Filter.PagedResult;
using InventorySystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Application.Interfaces.IServices
{
    public interface IAuditLogService
    {
        Task<bool> AddLogAsync(AuditLog log);
        Task<PagedResult<AuditLog>>? GetPagedAuditLogsAsync(AuditLogFilter filter);
    }
}
