using AutoMapper;
using InventorySystem.API.Common;
using InventorySystem.Application.DTOs.CategoryDto;
using InventorySystem.Application.Filter.BaseFilter;
using InventorySystem.Application.Filter.CategoryFilter;
using InventorySystem.Application.Filter.PagedResult;
using InventorySystem.Application.Interfaces.IServices;
using InventorySystem.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpPost("create-category")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<ApiResponse<CategoryGetDto>>> CreateCategory(CategoryCreateDto categoryData)
        {
            var mappedCategory = _mapper.Map<Category>(categoryData);
            var category = await _categoryService.CreateCategoryAsync(mappedCategory);
            if (category == null)
            {
                return ErrorResponse("Error creating category");
            }
            return OkResponse(category, "Category created successfully.");
        }

        [HttpGet("get-by-id/{categoryId}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<ApiResponse<CategoryGetDto>>> GetById(Guid categoryId)
        {
            var category = await _categoryService.GetByIdAsync(categoryId);
            if (category == null)
            {
                return NotFoundResponse();
            }
            return OkResponse(category, "category fetched successfully.");
        }

        [HttpGet("get-number-of-products-in-category-by id/{categoryId}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<ApiResponse<int>>> GetNumberOfProductsInCategoryAsync(Guid categoryId)
        {
            var productCount = await _categoryService.GetNumberOfProductsInCategoryAsync(categoryId);
            if (productCount <= 0)
            {
                throw new Exception("Products not found in category");
            }
            return OkResponse(productCount, "Products count retrieved successfully.");
        }

        [HttpGet("get-all-categories")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<ApiResponse<PagedResult<CategoryGetDto>>>> GetAllCategroies(CategoryFilter filter)
        {
            var categories = await _categoryService.GetPagedAsync(filter);
            if (categories != null)
            {
                return ErrorResponse("Category list is empty");
            }
            return OkResponse(categories, "category list retreived successfully");
        }

        [HttpPut("update-category-by-id/{categoryId}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateCategory(Guid categoryId, [FromBody]Category categoryUpdate)
        {
            var result = await _categoryService.UpdateCategoryAsync(categoryId, categoryUpdate);
            if (!result)
            {
                return ErrorResponse("Error updating category");
            }
            return OkResponse(result,"Category updated successfully.");
        }

        [HttpDelete("delete-category-by-id/{categoryId}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteCategory(Guid categoryId)
        {
            var result = await _categoryService.DeleteCategoryAsync(categoryId);
            if (!result)
            {
                return ErrorResponse("Error deleting category");
            }
            return OkResponse(result, "Category deleted successfully.");
        }
    }
}
