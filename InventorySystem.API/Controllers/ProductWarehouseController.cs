using InventorySystem.API.Common;
using InventorySystem.Application.DTOs.ProductWarehouseDto;
using InventorySystem.Application.Filter.PagedResult;
using InventorySystem.Application.Filter.ProductWarehouseFilter;
using InventorySystem.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductWarehouseController : BaseController
    {
        private readonly IProductWarehouseService _pws;
        public ProductWarehouseController(IProductWarehouseService pws)
        {
            _pws = pws;
        }

        [HttpPost("add-productwarehouse")]
        [Authorize(Roles ="User")]
        public async Task<ActionResult<ApiResponse<ProductWarehouseGetDto>>> CreatPw(ProductWarehouseCreateDto productwarehousedata)
        {
            var pw = await _pws.AddAsync(productwarehousedata);
            if (pw == null)
            {
                return ErrorResponse("Error adding productWarehouse");
            }
            return OkResponse(pw, "ProductWarehouse added successfully.");
        }

        [HttpGet("get-all-pws")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<ApiResponse<PagedResult<ProductWarehouseGetDto>>>> GetAllPws([FromQuery]ProductWarehouseFilter filter)
        {
            var pws = await _pws.FindProductWarehousesAsync(filter)!;
            if (pws == null || !pws.Data!.Any())
            {
                return NotFoundResponse("List is empty");
            }
            return OkResponse(pws, "Productwarehouses list retrieved successfully.");
        }

        [HttpGet("get-pw-by-id/{pwId}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<ApiResponse<ProductWarehouseGetDto>>> GetById(Guid productId,Guid warehouseId)
        {
            var pw = await _pws.GetByProductIdAndWarehouseIdAsync(productId, warehouseId);
            if (pw == null)
            {
                return NotFoundResponse();
            }
            return OkResponse(pw, "ProductWarehouse retrieved successfully.");
        }

        [HttpGet("get-pw-by-productId/{productId}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<ApiResponse<ProductWarehouseGetDto>>> GetPwByProductId(Guid productId)
        {
            var pw = await _pws.GetByProductIdAsync(productId);
            if (pw == null)
            {
                return NotFoundResponse();
            }
            return OkResponse(pw, "product warehouse retrieved successfully");
        }
        
        [HttpGet("get-pw-by-warehouseId/{warehouseId}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<ApiResponse<ProductWarehouseGetDto>>> GetPwByWarehouseId(Guid warehouseId)
        {
            var pw = await _pws.GetByWarehouseIdAsync(warehouseId);
            if (pw == null)
            {
                return NotFoundResponse();
            }
            return OkResponse(pw, "product warehouse retrieved successfully");
        }

        [HttpGet("get-pw-product-quantity")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<ApiResponse<int>>> GetProductQuantity(Guid productId,Guid warehouseId)
        {
            var pw = await _pws.GetProductQuantityAsync(productId, warehouseId);
            if (pw == null)
            {
                return NotFoundResponse();
            }
            return OkResponse(pw, "Product quantity retrieved successfully");
        }

        [HttpPut("update-pw-by-productId/{productId:Guid}&warehouseId/{warehouseId:Guid}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateProductWarehouse(Guid productId,Guid warehouseId, [FromBody] ProductWarehouseUpdateDto productWarehouseUpdataData)
        {
            var result = await _pws.UpdateAsync(productId, warehouseId, productWarehouseUpdataData);
            if (!result)
            {
                return ErrorResponse("Error updating productWarehouse");   
            }
            return OkResponse(result, "Product warehouse updated successfully.");
        }

        [HttpDelete("delete-productwarehouse-by-productId/{productId:Guid}&warehouseid/{warehouseId:Guid}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteProductWarehouse(Guid productId, Guid warehouseId)
        {
            var result = await _pws.DeleteAsync(productId, warehouseId);
            if (result)
            {
                return OkResponse(result, "pw deleted successfully");
            }
            return ErrorResponse("Error deleting pw");
        }
    }
}
