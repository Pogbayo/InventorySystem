using AutoMapper;
using InventorySystem.Application.DTOs.SupplierDto;
using InventorySystem.Application.Interfaces.IRepositories;
using InventorySystem.Application.Interfaces.IServices;
using InventorySystem.Domain.Entities;

namespace InventorySystem.Application.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IMapper _mapper;
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly CurrentUserService _currentuser;

        public SupplierService(
            IMapper mapper,
            IAuditLogRepository auditLogRepository,
            ISupplierRepository supplierRepository,
            CurrentUserService currentuser)
        {
            _mapper = mapper;
            _auditLogRepository = auditLogRepository;
            _currentuser = currentuser;
            _supplierRepository = supplierRepository;
        }

        public async Task<SupplierGetDto?> CreateSupplierAsync(Supplier supplier)
        {
            if (supplier == null)
            {
                throw new Exception("Invalid supplier details");
            }

            var supplierEntity = await _supplierRepository.CreateSupplierAsync(supplier);
            if (supplierEntity == null)
            {
                return null;
            }

            var mappedSupplierEntity = _mapper.Map<SupplierGetDto>(supplierEntity);
            var currentUserId = _currentuser.GetUserId();
            var auditLog = new AuditLog
            {
                Action = $"Registered a Supplier",
                PerformedBy = currentUserId,
                Details = $"{supplierEntity.Name} was created"
            };
            await _auditLogRepository.AddLogAsync(auditLog);
            return mappedSupplierEntity;
        }

        public async Task<SupplierGetDto?> GetByIdAsync(Guid supplierId)
        {
            var supplierEntity = await _supplierRepository.GetByIdAsync(supplierId);
            if (supplierEntity == null)
            {
                return null;
            }

            var mappedSupplierEntity = _mapper.Map<SupplierGetDto>(supplierEntity);
            var currentUserId = _currentuser.GetUserId();
            var auditLog = new AuditLog
            {
                Action = $"Fetched a supplier",
                PerformedBy = currentUserId,
                Details = $"{supplierEntity.Name} was fetched"
            };
            await _auditLogRepository.AddLogAsync(auditLog);
            return mappedSupplierEntity;
        }

        public async Task<SupplierGetDto?> GetByName(string name)
        {
            var supplierEntity = await _supplierRepository.GetByName(name);
            if (supplierEntity == null)
            {
                return null;
            }

            var mappedSupplierEntity = _mapper.Map<SupplierGetDto>(supplierEntity);
            var currentUserId = _currentuser.GetUserId();
            var auditLog = new AuditLog
            {
                Action = $"Fetched a supplier",
                PerformedBy = currentUserId,
                Details = $"{supplierEntity.Name} was fetched"
            };
            await _auditLogRepository.AddLogAsync(auditLog);
            return mappedSupplierEntity;
        }

        public async Task<bool> UpdateAsync(Guid supplierId, Supplier supplierUpdate)
        {
            var supplierEntity = await _supplierRepository.GetByIdAsync(supplierId);
            if (supplierEntity == null)
            {
                return false;
            }

            var updatedSupplierEntity = await _supplierRepository.UpdateAsync(supplierId, supplierUpdate);
            var mappedSupplierEntity = _mapper.Map<SupplierGetDto>(updatedSupplierEntity);
            var currentUserId = _currentuser.GetUserId();
            var auditLog = new AuditLog
            {
                Action = $"Updated a supplier",
                PerformedBy = currentUserId,
                Details = $"{supplierUpdate.Name} was updated"
            };
            await _auditLogRepository.AddLogAsync(auditLog);
            return true;
        }
    }
}


