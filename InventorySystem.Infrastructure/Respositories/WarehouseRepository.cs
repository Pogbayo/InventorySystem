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

        public async Task<Warehouse?> CreateWarehouseAsync(Warehouse warehouse)
        {
            await _context.Warehouses.AddAsync(warehouse);
            await _context.SaveChangesAsync();
            return warehouse;
        }

        public async Task<bool> DeleteWarehouseAsync(Guid warehouseId)
        {
            var warehouse = await _context.Warehouses.FindAsync(warehouseId);
            if (warehouse == null)
                return false;
            _context.Warehouses.Remove(warehouse);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<PagedResult<Warehouse>> GetAllAsync(BaseFilterClass filter)
        {
            var query = _context.Warehouses.AsQueryable();
            var pageNumber = filter.Page;
            var pageSize = filter.PageSize;
            var totalCount = await query.CountAsync();
            var warehouses = await query
                .OrderBy(w => w.WarehouseId) 
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new PagedResult<Warehouse>
            {
                Data = warehouses,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }


        public async Task<Warehouse?> GetWarehouseByIdAsync(Guid warehouseId)
        {
            return await _context.Warehouses.FindAsync(warehouseId);
        }

        public async Task<Warehouse?> GetWarehouseByName(string name)
        {
            return await _context.Warehouses.FirstOrDefaultAsync(n=>n.Name==name);
        }

        public async Task<bool> UpdateWarehouseAsync(Guid warehouseId, Warehouse warehouseUpdate)
        {
            var warehouse = await GetWarehouseByIdAsync(warehouseId);
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
