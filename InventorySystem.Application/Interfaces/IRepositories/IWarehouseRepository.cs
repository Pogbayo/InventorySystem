
using InventorySystem.Application.Filter.BaseFilter;
using InventorySystem.Application.Filter.PagedResult;
using InventorySystem.Domain.Entities;

namespace InventorySystem.Application.Interfaces.IRepositories
{
    public interface IWarehouseRepository
    {
        Task<Warehouse?> GetByIdAsync(Guid warehouseId);
        Task<Warehouse?> CreateProductAsync(Warehouse warehouse);
        Task<Warehouse?> GetByName(string name);
        Task<bool> UpdateAsync(Guid warehouseId, Warehouse warehouseUpdate);
        Task<bool> Deleteasync(Guid warehouseId);
        Task<PagedResult<Warehouse>> GetAllAsync(BaseFilterClass filter);
    }
}
