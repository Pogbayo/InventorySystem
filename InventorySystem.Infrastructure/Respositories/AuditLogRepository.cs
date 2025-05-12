using InventorySystem.Application.Filter.AuditLogFilter;
using InventorySystem.Application.Filter.PagedResult;
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

        public async Task<bool> AddLogAsync(AuditLog log)
        {
            await _context.AuditLogs.AddAsync(log);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PagedResult<AuditLog>> GetPagedAuditLogsAsync(AuditLogFilter filter)
        {
            var query = _context.AuditLogs.AsQueryable();

            if (!string.IsNullOrEmpty(filter.SearchItem))
            {
                query = query.Where(al => al.Action.Contains(filter.SearchItem) || al.PerformedBy.ToString().Contains(filter.SearchItem));
            }

            //if (filter.lastUpdateTime.HasValue)
            //{
            //    query = query.Where(al => al.CreatedAt > filter.lastUpdateTime.Value);
            //}

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

            var totalCount = await query.CountAsync();
            var auditLogs = await query
                .OrderByDescending(al => al.CreatedAt)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return new PagedResult<AuditLog>
            {
                Data = auditLogs,
                TotalCount = totalCount,
                PageNumber = filter.Page,
                PageSize = filter.PageSize
            };
        }
    }
}
