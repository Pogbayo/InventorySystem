using AutoMapper;
using InventorySystem.Application.DTOs.ProductDto;
using InventorySystem.Application.DTOs.ProductWarehouseDto;
using InventorySystem.Application.Filter.PagedResult;
using InventorySystem.Application.Filter.ProductWarehouseFilter;
using InventorySystem.Application.Interfaces.IRepositories;
using InventorySystem.Application.Interfaces.IServices;
using InventorySystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        public async Task<ProductWarehouseGetDto?> AddAsync(ProductWarehouseCreateDto productWarehouse)
        {
            if (productWarehouse is null)
            {
                return null;
            }

            var createDtoToEntity = _mapper.Map<ProductWarehouse>(productWarehouse);

            var productWarehouseEntity = await _productWarehouseRepository.AddAsync(createDtoToEntity);

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

        public async Task<ProductWarehouseGetDto?> GetByProductIdAndWarehouseIdAsync(Guid productId, Guid warehouseId)
        {
            if (warehouseId == Guid.Empty || productId == Guid.Empty)
            {
                throw new Exception("Invalid credentials");
            }

            var productWarehouse = await _productWarehouseRepository.GetByProductIdAndWarehouseIdAsync(productId, warehouseId);
            if (productWarehouse == null)
            {
                return null;
            }
            var mappedProductWarehouse = _mapper.Map<ProductWarehouseGetDto>(productWarehouse);
            var currentUserId = _currentuser.GetUserId();
            var auditLog = new AuditLog(
                action: $"Fetched a productWarehouse",
                performedBy: currentUserId,
                details: $"ProductWarehouse was fetched"
            );
            await _auditLogRepository.AddLogAsync(auditLog);
            return mappedProductWarehouse;
        }

        public async Task<IEnumerable<ProductWarehouseGetDto?>> GetByProductIdAsync(Guid productId)
        {
            if (productId == Guid.Empty)
            {
                throw new Exception("Invalid product Id");
            }

            var productWarehouses = await _productWarehouseRepository.GetByProductIdAsync(productId);
            if (productWarehouses == null || !productWarehouses.Any())
            {
                throw new Exception("Product doesn't exist in any ProductWarehouse");
            }

            var mappedProductWarehouses = _mapper.Map<IEnumerable<ProductWarehouseGetDto>>(productWarehouses);
            var currentUserId = _currentuser.GetUserId();
            var auditLog = new AuditLog(
                action: $"Fetched productWarehouses by products",
                performedBy: currentUserId,
                details: $"ProductWarehouses for product {productId} were fetched"
            );
            await _auditLogRepository.AddLogAsync(auditLog);
            return mappedProductWarehouses;
        }

        public async Task<IEnumerable<ProductWarehouseGetDto?>> GetByWarehouseIdAsync(Guid warehouseId)
        {
            if (warehouseId == Guid.Empty)
            {
                throw new Exception("Invalid warehouse Id");
            }

            var productWarehouses = await _productWarehouseRepository.GetByWarehouseIdAsync(warehouseId);
            if (productWarehouses == null || !productWarehouses.Any())
            {
                throw new Exception("WarehouseId doesn't exist in any ProductWarehouse");
            }

            var mappedProductWarehouses = _mapper.Map<IEnumerable<ProductWarehouseGetDto>>(productWarehouses);
            var currentUserId = _currentuser.GetUserId();
            var auditLog = new AuditLog(
                action: $"Fetched productWarehouses by warehouse",
                performedBy: currentUserId,
                details: $"ProductWarehouses for warehouse {warehouseId} were fetched"
            );
            await _auditLogRepository.AddLogAsync(auditLog);
            return mappedProductWarehouses;
        }

        public async Task<int?> GetProductQuantityAsync(Guid productId, Guid warehouseId)
        {
            if (productId == Guid.Empty || warehouseId == Guid.Empty)
            {
                throw new Exception("Invalid product or warehouse Id");
            }

            var quantity = await _productWarehouseRepository.GetProductQuantityAsync(productId, warehouseId);
            var currentUserId = _currentuser.GetUserId();
            var auditLog = new AuditLog(
                action: $"Fetched product quantity",
                performedBy: currentUserId,
                details: $"Product quantity for product {productId} in warehouse {warehouseId} was fetched"
            );
            await _auditLogRepository.AddLogAsync(auditLog);
            return quantity;
        }

        public async Task<bool> UpdateAsync(Guid productId, Guid warehouseId, ProductWarehouseUpdateDto productWarehouseupdatedata)
        {
            if (productId == Guid.Empty || warehouseId == Guid.Empty)
            {
                throw new Exception("Invalid product or warehouse Id");
            }
            var updateDtoToEntity = _mapper.Map<ProductWarehouse>(productWarehouseupdatedata);

            var result = await _productWarehouseRepository.UpdateAsync(productId, warehouseId, updateDtoToEntity);
            var currentUserId = _currentuser.GetUserId();
            var auditLog = new AuditLog(
                action: $"Updated productWarehouse",
                performedBy: currentUserId,
                details: $"ProductWarehouse for product {productId} in warehouse {warehouseId} was updated"
            );
            await _auditLogRepository.AddLogAsync(auditLog);

            return result;
        }

    }
}
