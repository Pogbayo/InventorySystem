using InventorySystem.Application.Filter.PagedResult;
using InventorySystem.Application.Filter.ProductWarehouseFilter;
using InventorySystem.Application.Interfaces.IRepositories;
using InventorySystem.Domain.Entities;
using InventorySystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Infrastructure.Respositories
{
    public class ProductWarehouseRepository : IProductWarehouseRepository
    {
        private readonly InventorySystemDb _context;
        
        public ProductWarehouseRepository(InventorySystemDb context)
        {
            _context = context;
        }

        public async Task<ProductWarehouse> AddAsync(ProductWarehouse productWarehouse)
        {
            await _context.ProductWarehouses.AddAsync(productWarehouse);
            await _context.SaveChangesAsync();
            return productWarehouse;
        }

        public async Task<bool> DeleteAsync(Guid productId, Guid warehouseId)
        {
            var productWarehouse = await _context.ProductWarehouses
                .Where(pw => pw.ProductId == productId && pw.WarehouseId == warehouseId)
                .FirstOrDefaultAsync();

            if (productWarehouse != null)
            {
                _context.ProductWarehouses.Remove(productWarehouse);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task<PagedResult<ProductWarehouse>> FindProductWarehousesAsync(ProductWarehouseFilter filter)
        {
            var productWarehouses = _context.ProductWarehouses
                .Include(pw => pw.Product)
                .Include(pw => pw.Warehouse)
                .Where(pw =>
                    (!filter.ProductId.HasValue || pw.ProductId == filter.ProductId) &&
                    (!filter.WarehouseId.HasValue || pw.WarehouseId == filter.WarehouseId) &&
                    (!filter.MinQuantity.HasValue || pw.Quantity >= filter.MinQuantity) &&
                    (!filter.MaxQuantity.HasValue || pw.Quantity <= filter.MaxQuantity) &&
                    (string.IsNullOrEmpty(filter.ProductName) || pw.Product.Name.Contains(filter.ProductName)) &&
                    (string.IsNullOrEmpty(filter.WarehouseName) || pw.Warehouse.Name.Contains(filter.WarehouseName)));

            var totalCount = await productWarehouses.CountAsync();
            var paginatedProductWarehouses = await productWarehouses
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            var pagedResult = new PagedResult<ProductWarehouse>
            {
                Data = paginatedProductWarehouses,
                TotalCount = totalCount,
                PageNumber = filter.Page < 1 ? 1 : filter.Page,
                PageSize = filter.PageSize < 1 ? 10 : filter.PageSize
            };

            return pagedResult;
        }



        public async Task<ProductWarehouse?> GetByProductIdAndWarehouseIdAsync(Guid productId, Guid warehouseId)
        {
            var productWarehouse = await _context.ProductWarehouses
                            .Where(pw => pw.ProductId == productId && pw.WarehouseId == warehouseId)
                            .FirstOrDefaultAsync();
            if (productWarehouse == null)
            {
                return null;
            }
            return productWarehouse;
        }

        public async Task<IEnumerable<ProductWarehouse>> GetByProductIdAsync(Guid productId)
        {
            return await _context.ProductWarehouses
                             .Where(pw => pw.ProductId == productId )
                                 .ToListAsync();
        }

        public async Task<IEnumerable<ProductWarehouse>> GetByWarehouseIdAsync(Guid warehouseId)
        {
            return await _context.ProductWarehouses
                                         .Where(pw => pw.ProductId == warehouseId)
                                             .ToListAsync();
        }

        public async Task<int?> GetProductQuantityAsync(Guid productId, Guid warehouseId)
        {
            var productWarehouse = await _context.ProductWarehouses
                           .Where(pw => pw.ProductId == productId && pw.WarehouseId == warehouseId)
                           .FirstOrDefaultAsync();

            if (productWarehouse == null)
            {
                return null;
            }

            return productWarehouse.Quantity;
        }

        public async Task<bool> UpdateAsync(Guid productId, Guid warehouseId,ProductWarehouse productWarehouse)
        {
            var productwarehouse = await _context.ProductWarehouses
                          .Where(pw => pw.ProductId == productId && pw.WarehouseId == warehouseId)
                          .FirstOrDefaultAsync();

            if (productWarehouse == null || productwarehouse == null)
            {
                return false;
            }

            _context.Entry(productWarehouse).CurrentValues.SetValues(productwarehouse);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
