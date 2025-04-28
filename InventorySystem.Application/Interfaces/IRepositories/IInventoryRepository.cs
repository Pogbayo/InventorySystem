using InventorySystem.Application.Filter.InventoryFilter;
using InventorySystem.Application.Filter.PagedResult;
using InventorySystem.Domain.Entities;

namespace InventorySystem.Application.Interfaces.IRepositories
{
    public interface IInventoryRepository
    {
        Task<InventoryMovement?> AddInventoryMovementAsync(InventoryMovement inventorymovement);
        Task<PagedResult<InventoryMovement>>? GetPagedAsync(InventoryFilter filter);
    }
}
