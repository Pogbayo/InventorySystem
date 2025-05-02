using InventorySystem.Application.Filter.BaseFilter;
using InventorySystem.Application.Filter.PagedResult;
using InventorySystem.Application.Interfaces.IRepositories;
using InventorySystem.Domain.Entities;
using InventorySystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Infrastructure.Respositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly InventorySystemDb _context;
        public WarehouseRepository(InventorySystemDb context)
        {
            _context = context;
        }

        public async Task<Warehouse?> CreateProductAsync(Warehouse warehouse)
        {
            await _context.Warehouses.AddAsync(warehouse);
            await _context.SaveChangesAsync();
            return warehouse;
        }

        public async Task<bool> Deleteasync(Guid warehouseId)
        {
            var warehouse = await _context.Warehouses.FindAsync(warehouseId);
            if (warehouse == null)
                return false;
            _context.Warehouses.Remove(warehouse);
            return true;
        }

        public async Task<PagedResult<Warehouse>> GetAllAsync(BaseFilterClass filter)
        {
            var query = _context.Warehouses.AsQueryable();
            var pageNumber = filter.Page;
            var pagesize = filter.PageSize;
            var totalCount = await query.CountAsync();
            var warehouses = await query
                .Skip((pageNumber - 1) * pagesize)
                .Take(pagesize)
                .ToListAsync();

            return new PagedResult<Warehouse>
            {
                Data = warehouses,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pagesize
            };
        }

        public async Task<Warehouse?> GetByIdAsync(Guid warehouseId)
        {
            return await _context.Warehouses.FindAsync(warehouseId);
        }

        public async Task<Warehouse?> GetByName(string name)
        {
            return await _context.Warehouses.FirstOrDefaultAsync(n=>n.Name==name);
        }

        public async Task<bool> UpdateAsync(Guid warehouseId, Warehouse warehouseUpdate)
        {
            var warehouse = await GetByIdAsync(warehouseId);
            if (warehouse == null)
            {
                throw new Exception("Warehouse not found");
            }
            try
            {
                _context.Entry(warehouse).CurrentValues.SetValues(warehouseUpdate);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
