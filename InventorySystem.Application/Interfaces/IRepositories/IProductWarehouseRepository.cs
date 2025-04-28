using InventorySystem.Application.Filter.PagedResult;
using InventorySystem.Application.Filter.ProductWarehouseFilter;
using InventorySystem.Domain.Entities;

namespace InventorySystem.Application.Interfaces.IRepositories
{
    public interface IProductWarehouseRepository
    {
        Task<IEnumerable<ProductWarehouse>> GetByProductIdAsync(Guid productId);
        Task<IEnumerable<ProductWarehouse>> GetByWarehouseIdAsync(Guid warehouseId);
        Task<ProductWarehouse?> GetByProductIdAndWarehouseIdAsync(Guid productId, Guid warehouseId);
        Task<int?> GetProductQuantityAsync(Guid productId, Guid warehouseId);
        Task<ProductWarehouse> AddAsync(ProductWarehouse productWarehouse);
        Task<bool> UpdateAsync(Guid productId, Guid warehouseId, ProductWarehouse productWarehouse);
        Task<PagedResult<ProductWarehouse>> FindProductWarehousesAsync(ProductWarehouseFilter filter);
        Task<bool> DeleteAsync(Guid productId, Guid warehouseId);
    }
}
