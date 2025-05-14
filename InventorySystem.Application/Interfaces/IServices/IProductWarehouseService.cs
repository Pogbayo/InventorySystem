using InventorySystem.Application.DTOs.ProductWarehouseDto;
using InventorySystem.Application.Filter.PagedResult;
using InventorySystem.Application.Filter.ProductWarehouseFilter;
using InventorySystem.Domain.Entities;

namespace InventorySystem.Application.Interfaces.IServices
{
    public interface IProductWarehouseService
    {
        Task<IEnumerable<ProductWarehouseGetDto?>> GetByProductIdAsync(Guid productId);
        Task<IEnumerable<ProductWarehouseGetDto?>> GetByWarehouseIdAsync(Guid warehouseId);
        Task<ProductWarehouseGetDto?> GetByProductIdAndWarehouseIdAsync(Guid productId, Guid warehouseId);
        Task<int?> GetProductQuantityAsync(Guid productId, Guid warehouseId);
        Task<ProductWarehouseGetDto?> AddAsync(ProductWarehouseCreateDto productWarehouse);
        Task<bool> UpdateAsync(Guid productId, Guid warehouseId, ProductWarehouseUpdateDto productWarehouseupdatedata);
        Task<PagedResult<ProductWarehouseGetDto>>? FindProductWarehousesAsync(ProductWarehouseFilter filter);
        Task<bool> DeleteAsync(Guid productId, Guid warehouseId);
    }
}
