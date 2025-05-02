using InventorySystem.Application.Filter.PagedResult;
using InventorySystem.Application.Filter.ProductFilter;
using InventorySystem.Domain.Entities;

namespace InventorySystem.Application.Interfaces.IRepositories
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(Guid productId);
        Task<Product?> CreateProductAsync(Product product);
        Task<Product?> GetByName(string name);
        Task<bool> UpdateAsync(Guid productId,Product updateData);
        Task<bool> DeleteAsync(Guid productId);
        Task<PagedResult<Product>> GetPagedAsync(ProductFilter filter);
    }
}
