using InventorySystem.Application.DTOs.ProductDto;
using InventorySystem.Application.DTOs.WarehouseDto;
using InventorySystem.Application.Filter.BaseFilter;
using InventorySystem.Application.Filter.PagedResult;
using InventorySystem.Domain.Entities;

namespace InventorySystem.Application.Interfaces.IServices
{
    public interface IWarehouseService
    {
        Task<WarehouseGetDto?> CreateAsync(Warehouse warehouse);
        Task<WarehouseGetDto?> GetByIdAsync(Guid warehouseId);
        Task<WarehouseGetDto?> GetByName(string name);
        Task<bool> UpdateAsync(Guid warehouseId, Warehouse warehouseUpdate);
        Task<bool> Deleteasync(Guid warehouseId);
        Task<PagedResult<WarehouseGetDto>>? GetAllAsync(BaseFilterClass filter);
    }
}
