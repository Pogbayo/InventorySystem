using InventorySystem.Application.DTOs.SupplierDto;
using InventorySystem.Domain.Entities;


namespace InventorySystem.Application.Interfaces.IServices
{
    public interface ISupplierService
    {
        Task<SupplierGetDto?> CreateSupplierAsync(SupplierCreateDto supplier);
        Task<SupplierGetDto?> GetByIdAsync(Guid supplierId);
        Task<SupplierGetDto?> GetByName(string name);
        Task<bool> UpdateAsync(Guid supplierId, SupplierUpdateDto supplierUpdate);
    }
}
