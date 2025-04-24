using InventorySystem.Application.Filter;
using InventorySystem.Application.Interfaces.IRepositories;
using InventorySystem.Domain.Entities;
using InventorySystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Infrastructure.Respositories
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly InventorySystemDb _context;

        public AuditLogRepository(InventorySystemDb context)
        {
            _context = context;
        }

        public async Task AddLogAsync(AuditLog log)
        {
            await _context.AuditLogs.AddAsync(log);
            await _context.SaveChangesAsync();
        }

        public async Task<List<AuditLog>> GetPagedAuditLogsAsync(AuditLogFilter filter)
        {
            var query = _context.AuditLogs.AsQueryable();

            if (!string.IsNullOrEmpty(filter.SearchItem))
            {
                query = query.Where(al => al.Action.Contains(filter.SearchItem) || al.PerformedBy.ToString().Contains(filter.SearchItem));
            }

            if (filter.lastUpdateTime.HasValue)
            {
                query = query.Where(al => al.CreatedAt > filter.lastUpdateTime.Value);
            }

            if (filter.StartDate.HasValue)
            {
                query = query.Where(al => al.CreatedAt >= filter.StartDate.Value);
            }

            if (filter.EndDate.HasValue)
            {
                query = query.Where(al => al.CreatedAt <= filter.EndDate.Value);
            }

            if (!string.IsNullOrEmpty(filter.Action))
            {
                query = query.Where(al => al.Action == filter.Action);
            }

            var auditLogs = await query
                .OrderBy(al => al.CreatedAt)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();
            return auditLogs;
        }
    }
}
