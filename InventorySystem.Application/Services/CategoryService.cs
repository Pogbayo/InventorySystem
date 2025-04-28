using AutoMapper;
using InventorySystem.Application.DTOs.CategoryDto;
using InventorySystem.Application.Filter.CategoryFilter;
using InventorySystem.Application.Filter.PagedResult;
using InventorySystem.Application.Interfaces.IRepositories;
using InventorySystem.Application.Interfaces.IServices;
using InventorySystem.Domain.Entities;


namespace InventorySystem.Application.Services
{    
    public class CategoryService : ICategoryService
    {
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly CurrentUserService _currentuser;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(IAuditLogRepository auditRepository, CurrentUserService currentuser, ICategoryRepository categoryRepository,IMapper mapper)
        {
            _auditLogRepository = auditRepository;
            _currentuser = currentuser;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryGetDto?> CreateCategoryAsync(Category createData)
        {
            if (createData == null)
            {
                return null;
            }
            var category = await _categoryRepository.CreateCategoryAsync(createData);

            var currentuserId = _currentuser.GetUserId();

            var auditLog = new AuditLog(
                action: $"Category created",
                performedBy: currentuserId,
                details: $"{currentuserId} created a new Category with name {category?.Name}"
            );

            await _auditLogRepository.AddLogAsync(auditLog);

            var mappedCategory = _mapper.Map<CategoryGetDto>(category);
            return mappedCategory;
        }

        public async Task<bool> DeleteCategoryAsync(Guid categoryId)
        {
            if (categoryId == Guid.Empty)
                return false;
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            var result = await _categoryRepository.DeleteCategoryAsync(categoryId);
            var currentuserId = _currentuser.GetUserId();

            if (result)
            {
               var auditLog = new AuditLog(
               action: $"{category?.Name} category deleted",
               performedBy: currentuserId,
               details: $"{currentuserId} deleted {category?.Name} Category "
             );
                
            await _auditLogRepository.AddLogAsync(auditLog);
            }

            return false;
        }

        public async Task<CategoryGetDto?> GetByIdAsync(Guid categoryId)
        {
            if (categoryId == Guid.Empty)
                 throw new Exception("Category not found");
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            var currentuserId = _currentuser.GetUserId();

            var auditLog = new AuditLog(
              action: $"Category fetched",
              performedBy: currentuserId,
              details: $"{currentuserId} fetched  {category?.Name} category"
            );

            await _auditLogRepository.AddLogAsync(auditLog);
            var mappedCategory = _mapper.Map<CategoryGetDto>(category);
            return mappedCategory;
        }

        public async Task<int> GetNumberOfProductsInCategoryAsync(Guid categoryId)
        {
            if (categoryId == Guid.Empty)
                throw new ArgumentException("Category ID is required", nameof(categoryId));

            var count = await _categoryRepository.GetNumberOfProductsInCategoryAsync(categoryId);
            var currentUserId = _currentuser.GetUserId();
            var auditLog = new AuditLog(
                action: $"Fetched product count for category",
                performedBy: currentUserId,
                details: $"Category ID: {categoryId}, Product count: {count}"
            );
            await _auditLogRepository.AddLogAsync(auditLog);
            return count;
        }

        public async Task<PagedResult<Category>> GetPagedAsync(CategoryFilter filter)
        {
            var pagedResult = await _categoryRepository.GetPagedAsync(filter);
            var currentUserId = _currentuser.GetUserId();
            var auditLog = new AuditLog(
                action: $"Fetched categories",
                performedBy: currentUserId,
                details: $"Page: {filter.Page}, Page size: {filter.PageSize}, Total count: {pagedResult.TotalCount}"
            );
            await _auditLogRepository.AddLogAsync(auditLog);
            return pagedResult;
        }


        public async Task<bool> UpdateCategoryAsync(Guid categoryId, Category updateData)
        {
            if (categoryId == Guid.Empty)
                throw new ArgumentException("Category ID is required", nameof(categoryId));

            var result = await _categoryRepository.UpdateCategoryAsync(categoryId, updateData);
            if (result)
            {
                var currentUserId = _currentuser.GetUserId();
                var auditLog = new AuditLog(
                    action: $"Updated category",
                    performedBy: currentUserId,
                    details: $"Category ID: {categoryId}, New name: {updateData.Name}"
                );
                await _auditLogRepository.AddLogAsync(auditLog);
            }
            return result;
        }

    }
}
