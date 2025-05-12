
using InventorySystem.Application.Filter.BaseFilter;
using InventorySystem.Application.Filter.PagedResult;
using InventorySystem.Domain.Entities;

namespace InventorySystem.Application.Interfaces.IRepositories
{
    public interface IWarehouseRepository
    {
        Task<Warehouse?> GetWarehouseByIdAsync(Guid warehouseId);
        Task<Warehouse?> CreateWarehouseAsync(Warehouse warehouse);
        Task<Warehouse?> GetWarehouseByName(string name);
        Task<bool> UpdateWarehouseAsync(Guid warehouseId, Warehouse warehouseUpdate);
        Task<bool> DeleteWarehouseAsync(Guid warehouseId);
        Task<PagedResult<Warehouse>> GetAllAsync(BaseFilterClass filter);
    }
}
