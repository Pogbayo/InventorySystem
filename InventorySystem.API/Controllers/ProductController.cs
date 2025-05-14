using InventorySystem.API.Common;
using InventorySystem.Application.DTOs.ProductDto;
using InventorySystem.Application.Filter.PagedResult;
using InventorySystem.Application.Filter.ProductFilter;
using InventorySystem.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace InventorySystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : BaseController
    {
        private readonly IProductService _productservice;
        public ProductController(IProductService productService)
        {
            _productservice = productService;
        }

        [HttpPost("create-product")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<ApiResponse<ProductGetDto>>> CreateProduct(ProductCreateDto productdata)
        {
            try
            {
                var product = await _productservice.CreateProductAsync(productdata);
                return OkResponse(product, "Product created successfully.");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex.InnerException?.Message ?? ex.Message);
            }
        }


        [HttpGet("get-by-id/{productId}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<ApiResponse<ProductGetDto>>> GetProductById(Guid productId)
        {
            var product = await _productservice.GetByIdAsync(productId);
            if (product == null)
            {
                return NotFoundResponse();
            }
            return OkResponse(product, "Product fetched successfully.");
        }
        
        [HttpGet("get-by-name")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<ApiResponse<ProductGetDto>>> GetProductByName(string name)
        {
            var product = await _productservice.GetByName(name);
            if (product == null)
            {
                return NotFoundResponse();
            }
            return OkResponse(product, "Product fetched successfully.");
        }

        [HttpGet("get-all-product")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<ApiResponse<PagedResult<ProductGetDto>>>> GetAllProducts([FromQuery] ProductFilter filter)
        {
            var productList = await _productservice.GetPagedAsync(filter)!;
            if (productList == null)
            {
                return NotFoundResponse();
            }
            return OkResponse(productList, "Product list retrieved successfully.");
        }

        [HttpDelete("delete-product-by-id/{productId}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteProduct(Guid productId)
        {
            var result = await _productservice.DeleteAsync(productId);
            if (!result)
            {
                return ErrorResponse("Error deleting product");
            }
            return OkResponse(result, "Product deleted successfully.");
        }

        [HttpPut("update-product-by-id/{productId}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateProduct(Guid productId, ProductCreateDto productdata)
        {
            var result = await _productservice.UpdateAsync(productId, productdata);
            if (!result)
            {
                return ErrorResponse("Error updating product");
            }
            return OkResponse(result, "Product updated successfully.");
        }
       
    }
}
