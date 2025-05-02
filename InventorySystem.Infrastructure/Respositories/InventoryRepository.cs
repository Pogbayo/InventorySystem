using InventorySystem.Application.Filter.InventoryFilter;
using InventorySystem.Application.Filter.PagedResult;
using InventorySystem.Application.Interfaces.IRepositories;
using InventorySystem.Domain.Entities;
using InventorySystem.Infrastructure.Migrations;
using InventorySystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Infrastructure.Respositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly InventorySystemDb _context;

        public InventoryRepository(IAuditLogRepository auditLogRepository, InventorySystemDb context)
        {
            _auditLogRepository = auditLogRepository;
            _context = context;
        }

        public async Task<InventoryMovement?> AddInventoryMovementAsync(InventoryMovement inventorymovement)
        {
            if (inventorymovement == null)
                throw new Exception("Movement is null");

            await _context.InventoryMovements.AddAsync(inventorymovement);
            await _context.SaveChangesAsync();
            return inventorymovement;
        }

        public async Task<PagedResult<InventoryMovement>> GetPagedAsync(InventoryFilter filter)
        {
            var query = _context.InventoryMovements.AsQueryable();

            if (filter.StartDate.HasValue)
            {
                query = query.Where(im => im.MovementDate >= filter.StartDate.Value);
            }

            if (!string.IsNullOrEmpty(filter.ProductName))
            {
                query = query.Where(im => im.Product != null && im.Product.Name.Contains(filter.ProductName));   
            }
             

            if (filter.EndDate.HasValue)
            {
                query = query.Where(im => im.MovementDate <= filter.EndDate.Value);
            }

            var pageNumber = filter.Page < 1 ? 1 : filter.Page;
            var pageSize = filter.PageSize < 1 ? 10 : filter.PageSize;

            var totalCount = await query.CountAsync();
            var movements = await query
               .Skip((pageNumber - 1) * pageSize)
               .Take(pageSize)
               .ToListAsync();

            return new PagedResult<InventoryMovement>
            {
                Data = movements,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}
