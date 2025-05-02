

using InventorySystem.Application.DTOs.ProductDto;
using InventorySystem.Application.Filter.PagedResult;
using InventorySystem.Application.Filter.ProductFilter;
using InventorySystem.Domain.Entities;

namespace InventorySystem.Application.Interfaces.IServices
{
    public interface IProductService
    {
        Task<ProductGetDto?> GetByIdAsync(Guid productId);
        Task<ProductGetDto> GetByName(string name);
        Task<ProductGetDto?> CreateProductAsync(ProductCreateDto productcreatedto);
        Task<bool> UpdateAsync(Guid productId, Product updateData);
        Task<bool> DeleteAsync(Guid productId);
        Task<PagedResult<ProductGetDto>>? GetPagedAsync(ProductFilter filter);
    }
}
