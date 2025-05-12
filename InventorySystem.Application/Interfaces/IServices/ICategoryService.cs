using InventorySystem.Application.Filter.CategoryFilter;
using InventorySystem.Domain.Entities;
using InventorySystem.Application.DTOs.CategoryDto;
using InventorySystem.Application.Filter.PagedResult;

namespace InventorySystem.Application.Interfaces.IServices
{
    public interface ICategoryService
    {
        Task<CategoryGetDto?> CreateCategoryAsync(Category createData);
        Task<CategoryGetDto?> GetByIdAsync(Guid categoryId);
        Task<bool> DeleteCategoryAsync(Guid categoryId);
        Task<int> GetNumberOfProductsInCategoryAsync(Guid categoryId);
        Task<bool> UpdateCategoryAsync(Guid categoryId, Category updateData);
        Task<PagedResult<CategoryGetDto>> GetPagedAsync(CategoryFilter filter);
    }
}
