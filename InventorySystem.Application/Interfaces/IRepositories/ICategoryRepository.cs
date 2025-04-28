using InventorySystem.Application.DTOs.CategoryDto;
using InventorySystem.Application.Filter.CategoryFilter;
using InventorySystem.Application.Filter.PagedResult;
using InventorySystem.Domain.Entities;


namespace InventorySystem.Application.Interfaces.IRepositories
{
    public interface ICategoryRepository
    {
        Task<Category?> CreateCategoryAsync(Category createData);
        Task<Category?> GetByIdAsync(Guid categoryId);
        Task<int> GetNumberOfProductsInCategoryAsync(Guid categoryId);
        Task<bool> UpdateCategoryAsync(Guid categoryId, Category updateData);
        Task<bool> DeleteCategoryAsync(Guid categoryId);
        Task<PagedResult<Category>> GetPagedAsync(CategoryFilter filter);
    }
}
