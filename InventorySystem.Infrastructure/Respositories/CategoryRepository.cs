using InventorySystem.Application.Filter.CategoryFilter;
using InventorySystem.Application.Filter.PagedResult;
using InventorySystem.Application.Interfaces.IRepositories;
using InventorySystem.Domain.Entities;
using InventorySystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Infrastructure.Respositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly InventorySystemDb _context;

        public CategoryRepository(InventorySystemDb context)
        {
            _context = context;
        }
        public async Task<Category?> CreateCategoryAsync(Category createData)
        {
            if (string.IsNullOrEmpty(createData.Name))
            {
                throw new Exception("Please provide a Name for the Category");
            }

            await _context.Categories.AddAsync(createData);
            await _context.SaveChangesAsync();

            return createData;
        }

        public async Task<Category?> GetByIdAsync(Guid categoryId)
        {
            return await _context.Categories.FindAsync(categoryId);
        }

        public async Task<bool> DeleteCategoryAsync(Guid categoryId)
        {
            var category = await GetByIdAsync(categoryId);
            if (category == null) return false;
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetNumberOfProductsInCategoryAsync(Guid categoryId)
        {
            return await _context.Categories
                .Where(c => c.CategoryId == categoryId)
                .Select(c => c.Products.Count)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateCategoryAsync(Guid categoryId, Category categoryUpdate)
        {
            var existingCategory = await _context.Categories.FindAsync(categoryId);
            if (existingCategory == null)
                return false;

            existingCategory.Name = categoryUpdate.Name;
            

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<PagedResult<Category>> GetPagedAsync(CategoryFilter filter)
        {
            var query = _context.Categories.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(c => c.Name.Contains(filter.Name));
            }

            var pageNumber = filter.Page < 1 ? 1 : filter.Page;
            var pageSize = filter.PageSize < 1 ? 10 : filter.PageSize;

            var totalCount = await query.CountAsync();
            var categories = await query
                .Skip(( pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Category>
            {
                Data = categories,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}
