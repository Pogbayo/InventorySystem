using AutoMapper;
using InventorySystem.Application.DTOs.ProductDto;
using InventorySystem.Application.DTOs.WarehouseDto;
using InventorySystem.Application.Filter.BaseFilter;
using InventorySystem.Application.Filter.PagedResult;
using InventorySystem.Application.Interfaces.IRepositories;
using InventorySystem.Application.Interfaces.IServices;
using InventorySystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Application.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IMapper _mapper;
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly CurrentUserService _currentuser;
        public WarehouseService(
           IMapper mapper,
           IAuditLogRepository auditLogRepository,
           IWarehouseRepository warehouseRepository,
           CurrentUserService currentuser
           )
        {
            _mapper = mapper;
            _auditLogRepository = auditLogRepository;
            _warehouseRepository = warehouseRepository;
            _currentuser = currentuser;
        }

        public Task<WarehouseGetDto?> CreateAsync(Warehouse warehouse)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Deleteasync(Guid warehouseId)
        {
            var warehouse = await GetByIdAsync(warehouseId);
            if (warehouse==null)
            {
                throw new Exception($"Warehouse with provided id: {warehouseId} does not exist");
            }
            var result = await _warehouseRepository.Deleteasync(warehouseId);
            if (result)
            {
                var currentUserId = _currentuser.GetUserId();
                var auditLog = new AuditLog(
                    action: $"Deleted {warehouse.Name}",
                    performedBy: currentUserId,
                    details: $"{warehouse.Name} was updated"
                );
                await _auditLogRepository.AddLogAsync(auditLog);
                return true;
            }
            return false;
        }

        public async Task<PagedResult<WarehouseGetDto>>? GetAllAsync(BaseFilterClass filter)
        {
            if (filter == null)
            {
                throw new Exception("Warehouse list is empty;");  
            }
            var pagedResult = await _warehouseRepository.GetAllAsync(filter);

            var currentUserId = _currentuser.GetUserId();
            var auditLog = new AuditLog
            {
                Action = $"Fetched warehouses",
                PerformedBy = currentUserId,
                Details = $"Page: {filter.Page}, Page size: {filter.PageSize}, Total count: {pagedResult.TotalCount}"
            };
            await _auditLogRepository.AddLogAsync(auditLog);

            var mappedWarehouses = pagedResult.Data?.Select(p => _mapper.Map<WarehouseGetDto>(p)).ToList();
           

            return new PagedResult<WarehouseGetDto>
            {
                Data = mappedWarehouses,
                TotalCount = pagedResult.TotalCount,
                PageNumber = pagedResult.PageNumber,
                PageSize = pagedResult.PageSize
            };
        }



        public async Task<WarehouseGetDto?> GetByIdAsync(Guid warehouseId)
        {
            if (warehouseId == Guid.Empty)
            {
                return null;
            }
            var warehouse = await GetByIdAsync(warehouseId);
            if (warehouse == null)
            {
                return null;
            }
            var currentUserId = _currentuser.GetUserId();
            var auditLog = new AuditLog
            (
                action : $"Fetched warehouse by its id: {warehouse.Id}",
                performedBy : currentUserId,
                details: $"{warehouse.Name} was fetched"
            );
            await _auditLogRepository.AddLogAsync(auditLog);
            var mappedWarehouse = _mapper.Map<WarehouseGetDto>(warehouse);
            return mappedWarehouse;
        }

        public async Task<WarehouseGetDto?> GetByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Please, provide a product name");
            }
            var warehouse = await _warehouseRepository.GetByName(name);
            if (warehouse == null)
            {
                throw new Exception("No warehouse with the provided name exists");
            }
            var mappedWarehouse = _mapper.Map<WarehouseGetDto>(warehouse);
            var currentUserId = _currentuser.GetUserId();

            var auditLog = new AuditLog
           (
             action: $"Fetched {warehouse.Name}",
             performedBy: currentUserId,
             details: $"{warehouse.Name} was fetched"
           );

            await _auditLogRepository.AddLogAsync(auditLog);
            return mappedWarehouse;
        }

        public async Task<bool> UpdateAsync(Guid warehouseId, Warehouse warehouseUpdate)
        {
            if (warehouseId == Guid.Empty || warehouseUpdate == null)
                throw new Exception("Data error;");
            var warehouse = await _warehouseRepository.GetByIdAsync(warehouseId);
          
            var result = await _warehouseRepository.UpdateAsync(warehouseId, warehouseUpdate);
            if (!result)
            {
                return false;
            }
            var currentUserId = _currentuser.GetUserId();
            var auditLog = new AuditLog(
                action: $"Updated warehouse",
                performedBy: currentUserId,
                details: $"{warehouse!.Name} was updated"
            );
            await _auditLogRepository.AddLogAsync(auditLog);
            return true;
        }
    }
}
