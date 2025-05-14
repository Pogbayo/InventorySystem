using InventorySystem.Application.Filter.AuditLogFilter;
using InventorySystem.Application.Filter.PagedResult;
using InventorySystem.Application.Interfaces.IRepositories;
using InventorySystem.Application.Interfaces.IServices;
using InventorySystem.Domain.Entities;


namespace InventorySystem.Application.Services
{
    public class AuditLogService : IAuditLogService
    {
        private readonly IAuditLogRepository _auditLogRepository;
        public AuditLogService(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }
        public async Task<bool> AddLogAsync(AuditLog log)
        {
            if (log == null)
            {
                throw new Exception("Log is invalid");
            }
            var result = await _auditLogRepository.AddLogAsync(log);
            if (result != true)
            {
                return false;
            }
            return true;
        }

        public async Task<PagedResult<AuditLog>> GetPagedAuditLogsAsync(AuditLogFilter filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var pagedResult = await _auditLogRepository.GetPagedAuditLogsAsync(filter);
            return pagedResult;
        }
    }
}
