using InventorySystem.API.Common;
using InventorySystem.Application.DTOs.SupplierDto;
using InventorySystem.Application.Interfaces.IServices;
using InventorySystem.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : BaseController
    {
        private readonly ISupplierService _supplierService;
        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpPost("create-supplier")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<ApiResponse<SupplierGetDto>>> CreateCategory([FromBody] SupplierCreateDto supplierdata)
        {
            var supplier = await _supplierService.CreateSupplierAsync(supplierdata);
            if (supplier == null)
            {
               return ErrorResponse("Supplier not registered successfuly");
            }
            return OkResponse(supplier, "Supplier resgistered successfully.");
        }

        [HttpGet("get-supplier-by-id/{supplierId}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<ApiResponse<SupplierGetDto>>> GetSupplierById(Guid supplierId)
        {
            var supplier = await _supplierService.GetByIdAsync(supplierId);
            if (supplier == null)
            {
                return NotFoundResponse();
            }
            return OkResponse(supplier, "Supplier retrieved successfully");
        }


        [HttpGet("get-supplier-by-name")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<ApiResponse<SupplierGetDto>>> GetSupplierByName(string name)
        {
            var supplier = await _supplierService.GetByName(name);
            if (supplier == null)
            {
                return NotFoundResponse();
            }
            return OkResponse(supplier,"Supplier retreived successfully.");
        }

        [HttpPut("update-supplier-by-id/{supplierId}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateSupplier(Guid supplierId,SupplierUpdateDto supplierUpdate)
        {
            var result = await _supplierService.UpdateAsync(supplierId, supplierUpdate);
            if (!result)
            {
                return ErrorResponse("Error updating supplier");   
            }
            return OkResponse(result,"Supplier details updated successfully");
        }

    }
}
