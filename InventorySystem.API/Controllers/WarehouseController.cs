using InventorySystem.API.Common;
using InventorySystem.Application.DTOs.WarehouseDto;
using InventorySystem.Application.Filter.BaseFilter;
using InventorySystem.Application.Filter.PagedResult;
using InventorySystem.Application.Interfaces.IServices;
using InventorySystem.Domain.Entities;

//using InventorySystem.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.API.Controllers
{
    public class WarehouseController : BaseController
    {
        private readonly IWarehouseService _warehouseservice;
        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseservice = warehouseService;
        }

        [HttpGet("get-all-warehouses")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<ApiResponse<PagedResult<WarehouseGetDto>>>> GetAllWarehouses([FromQuery] BaseFilterClass Filter)
        {
            var warehouses = await _warehouseservice.GetAllAsync(Filter)!;
            if (warehouses != null)
            {
                return OkResponse(warehouses, "Warehouses fetched successfully.");
            }
            return NotFoundResponse("Error retrieving warehouses");
        }
          
        [HttpGet("get-by-name")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<ApiResponse<WarehouseGetDto>>> GetWarehouseByName(string name)
        {
            var warehouse = await _warehouseservice.GetByName(name)!;
            if (warehouse != null)
            {
                return OkResponse(warehouse, "Warehouse fetched successfully.");
            }
            return NotFoundResponse("Error retrieving warehouse");
        }
          
        [HttpGet("get-by-id/{warehouseId}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<ApiResponse<WarehouseGetDto>>> GetWarehouseById(Guid warehouseId)
        {
            var warehouse = await _warehouseservice.GetByIdAsync(warehouseId)!;
            if (warehouse != null)
            {
                return OkResponse(warehouse , "Warehouse fetched successfully.");
            }
            return NotFoundResponse("Warehouse not found");
        }

        [HttpDelete("delete-warehouse-by-id/{warehouseId}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteWarehouse(Guid warehouseId)
        {
            var warehouse = await _warehouseservice.GetByIdAsync(warehouseId);
            var result = await _warehouseservice.Deleteasync(warehouseId);
            if (result)
            {
                return OkResponse(result, $"{warehouse!.Name} deleted successfully."); 
            }
            return ErrorResponse("Error deleting warehouse",$"{warehouse!.Name} not deleted");
        }

        [HttpPut("update-warehouse-by-id/{warehouseId}")]
        [Authorize(Roles ="User")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateWarehouse(Guid warehouseId, [FromBody]Warehouse warehouseUpdate)
        {
            var warehouse = await _warehouseservice.GetByIdAsync(warehouseId);
            var result = await _warehouseservice.UpdateAsync(warehouseId, warehouseUpdate);
            if (result)
            {
                return OkResponse(result, $"{warehouse!.Name} was updated successfully.");
            }
            return ErrorResponse("Error updating warehouse");
        }

        [HttpPost("create-warehouse")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<ApiResponse<WarehouseGetDto>>> CreateAsync(WarehouseCreateDto warehouse)
        {
            var warehouseEntity = await _warehouseservice.CreateAsync(warehouse);
            if (warehouseEntity != null)
            {
                return OkResponse(warehouseEntity, "Warehouse created successfully.");
            }
            return NotFoundResponse("Error creating Warehouse");
        }
    }
}
