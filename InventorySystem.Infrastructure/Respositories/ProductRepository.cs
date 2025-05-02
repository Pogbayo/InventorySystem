using InventorySystem.Application.Filter.PagedResult;
using InventorySystem.Application.Filter.ProductFilter;
using InventorySystem.Application.Interfaces.IRepositories;
using InventorySystem.Domain.Entities;
using InventorySystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Infrastructure.Respositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly InventorySystemDb _context;
        public ProductRepository(InventorySystemDb context)
        {
            _context = context;
        }

        public async Task<Product?> CreateProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteAsync(Guid productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
                return false;
            _context.Products.Remove(product);
            return true;
        }

        public async Task<Product?> GetByIdAsync(Guid productId)
        {
            return await _context.Products.FindAsync(productId);
        }

        public async Task<Product?> GetByName(string name)
        {
            return await _context.Products.FirstOrDefaultAsync(n=>n.Name==name);
        }

        public async Task<PagedResult<Product>> GetPagedAsync(ProductFilter filter)
        {
            var query =  _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(c => c.Name.Contains(filter.Name));

            var pageNumber = filter.Page < 1 ? 1 : filter.Page;
            var pageSize = filter.PageSize < 1 ? 10 : filter.PageSize;

            var totalCount = await query.CountAsync();
            var products = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Product>
            {
                Data = products,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };
        }

        public async Task<bool> UpdateAsync(Guid productId,Product updateData)
        {
            var product = await GetByIdAsync(productId);
            if (product is null)
            return false;
            try
            {
                _context.Entry(product).CurrentValues.SetValues(updateData);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }
    }
}
