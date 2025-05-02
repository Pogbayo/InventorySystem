using AutoMapper;
using InventorySystem.Application.DTOs.ProductDto;
using InventorySystem.Application.Filter.PagedResult;
using InventorySystem.Application.Filter.ProductFilter;
using InventorySystem.Application.Interfaces.IRepositories;
using InventorySystem.Application.Interfaces.IServices;
using InventorySystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InventorySystem.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly IProductRepository _productRepository;
        private readonly CurrentUserService _currentuser;

        public ProductService
            (
            IMapper mapper, 
            IAuditLogRepository auditLogRepository,
            IProductRepository productRepository,
            CurrentUserService currentuser
            )
        {
            _mapper = mapper;
            _auditLogRepository = auditLogRepository;
            _currentuser = currentuser;
            _productRepository = productRepository;
        }

        public async Task<ProductGetDto?> CreateProductAsync(ProductCreateDto productcreatedto)
        {
            if (productcreatedto == null)
            {
                return null;
            }
            var productEntity = _mapper.Map<Product>(productcreatedto);
            var savedEntity = await _productRepository.CreateProductAsync(productEntity);
            var mappedProduct = _mapper.Map<ProductGetDto>(savedEntity);
            var currentUserId = _currentuser.GetUserId();

            var auditLog = new AuditLog
            (
                action: $"Created {productEntity}",
                performedBy: currentUserId,
                details: $"{productEntity.Name} was created"
            );
            await _auditLogRepository.AddLogAsync(auditLog);

            return mappedProduct;
        }

        public async Task<bool> DeleteAsync(Guid productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                return false;
            }
            var result = await _productRepository.DeleteAsync(productId);
            var currentUserId = _currentuser.GetUserId();

            var auditLog = new AuditLog
            (
                action: $"Deleted {product.Name}",
                performedBy: currentUserId,
                details: $"{product.Name} was deleted"
            );
            await _auditLogRepository.AddLogAsync(auditLog);

            return result;
        }

        public async Task<ProductGetDto?> GetByIdAsync(Guid productId)
        {
            if (productId == Guid.Empty)
                throw new Exception("Invalid productId");
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                return null;
            }
            var mappedProduct = _mapper.Map<ProductGetDto>(product);

            var currentUserId = _currentuser.GetUserId();

            var auditLog = new AuditLog
            (
              action: $"Fetched {product.Name} by id: {productId}",
              performedBy: currentUserId,
              details: $"{product.Name} was fetched"
            );

            await _auditLogRepository.AddLogAsync(auditLog);
            return mappedProduct;
        }

        public async Task<ProductGetDto> GetByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Please, provide a product name");
            }
            var product = await _productRepository.GetByName(name);
            if (product == null)
            {
                throw new Exception("No product with the provided name exists");
            }
            var mappedProduct = _mapper.Map<ProductGetDto>(product);
            var currentUserId = _currentuser.GetUserId();

            var auditLog = new AuditLog
           (
             action: $"Fetched {product.Name}",
             performedBy: currentUserId,
             details: $"{product.Name} was fetched"
           );

            await _auditLogRepository.AddLogAsync(auditLog);
            return mappedProduct;
        }

        public async Task<PagedResult<ProductGetDto>>? GetPagedAsync(ProductFilter filter)
        {
            var pagedResult = await _productRepository.GetPagedAsync(filter);

            var currentUserId = _currentuser.GetUserId();
            var auditLog = new AuditLog(
                action: $"Fetched products",
                performedBy: currentUserId,
                details: $"Page: {filter.Page}, Page size: {filter.PageSize}, Total count: {pagedResult.TotalCount}"
            );
            await _auditLogRepository.AddLogAsync(auditLog);

            var mappedProducts = pagedResult.Data?.Select(p => _mapper.Map<ProductGetDto>(p)).ToList();
            return new PagedResult<ProductGetDto>
            {
                Data = mappedProducts,
                TotalCount = pagedResult.TotalCount,
                PageNumber = pagedResult.PageNumber,
                PageSize = pagedResult.PageSize
            };
        }

        public async Task<bool> UpdateAsync(Guid productId, Product updateData)
        {
            if (productId == Guid.Empty || updateData == null)
                throw new Exception("Data error;");

            var result = await _productRepository.UpdateAsync(productId, updateData);
            if (!result)
            {
                return false;
            }
            var currentUserId = _currentuser.GetUserId();
            var auditLog = new AuditLog(
                action: $"Updated product",
                performedBy: currentUserId,
                details: $"{updateData.Name} was updated"
            );
            await _auditLogRepository.AddLogAsync(auditLog);
            return true;
        }
    }
}
