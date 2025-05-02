using InventorySystem.Application.Filter.InventoryFilter;
using InventorySystem.Application.Filter.PagedResult;
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
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IAuditLogService _auditLogService;
        private readonly CurrentUserService _currentuser;

        public InventoryService(
            IInventoryRepository inventoryRepository,
            IAuditLogService auditLogService,
            CurrentUserService currentUser
            )
        {
            _inventoryRepository = inventoryRepository;
            _auditLogService = auditLogService;
            _currentuser = currentUser;
        }

        public async Task<InventoryMovement?> AddInventoryMovementAsync(InventoryMovement inventoryMovement)
        {
            if (inventoryMovement == null)
            {
                throw new Exception("Movement is null");
            }

            var addedMovement = await _inventoryRepository.AddInventoryMovementAsync(inventoryMovement);
            
            return addedMovement;
        }

        public async Task<PagedResult<InventoryMovement>> GetPagedAsync(InventoryFilter filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var pagedResult = await _inventoryRepository.GetPagedAsync(filter);
            
            return pagedResult;
        }
    }

}
