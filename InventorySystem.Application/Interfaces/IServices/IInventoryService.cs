using InventorySystem.Application.Filter.InventoryFilter;
using InventorySystem.Application.Filter.PagedResult;
using InventorySystem.Domain.Entities;

namespace InventorySystem.Application.Interfaces.IServices
{
    public interface IInventoryService
    {
        Task<InventoryMovement?> AddInventoryMovementAsync(InventoryMovement inventoryMovement);
        Task<PagedResult<InventoryMovement>> GetPagedAsync(InventoryFilter filter);

    }
}
