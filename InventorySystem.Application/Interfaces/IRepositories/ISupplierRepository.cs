using InventorySystem.Domain.Entities;

namespace InventorySystem.Application.Interfaces.IRepositories
{
    public interface ISupplierRepository
    {
        Task<Supplier> CreateSupplierAsync(Supplier supplier);
        Task<Supplier?> GetByIdAsync(Guid supplierId);
        Task<Supplier?> GetByName(string name);
        Task<bool> UpdateAsync(Guid supplierId,Supplier supplierUpdate);
    }
}
