using InventorySystem.Application.Filter.CategoryFilter;
using InventorySystem.Application.Filter;
using InventorySystem.Domain.Entities;
using InventorySystem.Application.DTOs.CategoryDto;

namespace InventorySystem.Application.Interfaces.IServices
{
    public interface ICategoryService
    {
        Task<CategoryGetDto?> CreateCategoryAsync(Category createData);
        Task<CategoryGetDto?> GetByIdAsync(Guid categoryId);
        Task<bool> DeleteCategoryAsync(Guid categoryId);
        Task<int> GetNumberOfProductsInCategoryAsync(Guid categoryId);
        Task<bool> UpdateCategoryAsync(Guid categoryId, Category updateData);
        Task<PagedResult<Category>> GetPagedAsync(CategoryFilter filter);
    }
}
