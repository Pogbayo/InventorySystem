using AutoMapper;
using InventorySystem.Application.DTOs.ProductDto;
using InventorySystem.Application.DTOs.ProductWarehouseDto;
using InventorySystem.Application.Filter.PagedResult;
using InventorySystem.Application.Filter.ProductWarehouseFilter;
using InventorySystem.Application.Interfaces.IRepositories;
using InventorySystem.Application.Interfaces.IServices;
using InventorySystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Application.Services
{
    public class ProductWarehouseService : IProductWarehouseService
    {
        private readonly IMapper _mapper;
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly IProductWarehouseRepository _productWarehouseRepository;
        private readonly CurrentUserService _currentuser;
        public ProductWarehouseService
           (
           IMapper mapper,
           IAuditLogRepository auditLogRepository,
           IProductWarehouseRepository productWarehouseRepository,
           CurrentUserService currentuser
           )
        {
            _mapper = mapper;
            _auditLogRepository = auditLogRepository;
            _currentuser = currentuser;
            _productWarehouseRepository = productWarehouseRepository;
        }
        public async Task<ProductWarehouseGetDto?> AddAsync(ProductWarehouse productWarehouse)
        {
            if (productWarehouse is null)
            {
                return null;
            }

            var productWarehouseEntity = await _productWarehouseRepository.AddAsync(productWarehouse);
            var mappedProductWarehouse = _mapper.Map<ProductWarehouseGetDto>(productWarehouseEntity);
            var currentUserId = _currentuser.GetUserId();

            var auditLog = new AuditLog
            (
                action: $"Created {mappedProductWarehouse}",
                performedBy: currentUserId,
                details: $"{mappedProductWarehouse} was created"
            );
            await _auditLogRepository.AddLogAsync(auditLog);

            return mappedProductWarehouse;
        }

        public async Task<bool> DeleteAsync(Guid productId, Guid warehouseId)
        {
            if (warehouseId == Guid.Empty || productId == Guid.Empty)
            {
                throw new Exception("Invalid credentials");
            }
            var result = await _productWarehouseRepository.DeleteAsync(productId, warehouseId);

            if (result)
            {
             var currentuserId = _currentuser.GetUserId();

              var auditLog = new AuditLog(
             action: $"Productwarehouse with productId: {productId} and warehouseId: {warehouseId} deleted",
             performedBy: currentuserId,
             details: $"{currentuserId} deleted the specified productWarehouse"
            );
            
            await _auditLogRepository.AddLogAsync(auditLog);

                return true;
            }
            return false;
        }

        public async Task<PagedResult<ProductWarehouseGetDto>>? FindProductWarehousesAsync(ProductWarehouseFilter filter)
        {
            var pagedResult = await _productWarehouseRepository.FindProductWarehousesAsync(filter);
            if (pagedResult == null)
            {
                throw new Exception("Error retrieving product warehouses");
            }
            var currentUserId = _currentuser.GetUserId();
            var auditLog = new AuditLog(
                action: $"Fetched productWarehouses",
                performedBy: currentUserId,
                details: $"Page: {filter.Page}, Page size: {filter.PageSize}, Total count: {pagedResult.TotalCount}"
            );
            await _auditLogRepository.AddLogAsync(auditLog);

            var mappedProductWarehouses = pagedResult.Data?.Select(p => _mapper.Map<ProductWarehouseGetDto>(p)).ToList();

            return new PagedResult<ProductWarehouseGetDto>
            {
                Data = mappedProductWarehouses,
                TotalCount = pagedResult.TotalCount,
                PageNumber = pagedResult.PageNumber,
                PageSize = pagedResult.PageSize
            };
        }

        public Task<ProductWarehouseGetDto?> GetByProductIdAndWarehouseIdAsync(Guid productId, Guid warehouseId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductWarehouseGetDto?>> GetByProductIdAsync(Guid productId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductWarehouseGetDto?>> GetByWarehouseIdAsync(Guid warehouseId)
        {
            throw new NotImplementedException();
        }

        public Task<int?> GetProductQuantityAsync(Guid productId, Guid warehouseId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Guid productId, Guid warehouseId, ProductWarehouse productWarehouse)
        {
            throw new NotImplementedException();
        }
    }
}
