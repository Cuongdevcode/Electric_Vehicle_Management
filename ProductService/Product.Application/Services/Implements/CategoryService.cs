using Product.Application.DTOs;
using Product.Application.IRepositories;
using Product.Application.Services.Interfaces;
using Product.Domain.Entities;

namespace Product.Application.Services.Implements
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWorkRepository _unitOfWork;

        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWorkRepository unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                IsActive = c.IsActive,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            });
        }

        public async Task<CategoryDTO?> GetCategoryByIdAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) return null;

            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                IsActive = category.IsActive,
                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt
            };
        }

        public async Task<CategoryDTO> CreateCategoryAsync(CreateCategoryDTO createCategoryDto)
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = createCategoryDto.Name,
                Description = createCategoryDto.Description,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            var createdCategory = await _categoryRepository.CreateAsync(category);
            await _unitOfWork.SaveChangesAsync();

            return new CategoryDTO
            {
                Id = createdCategory.Id,
                Name = createdCategory.Name,
                Description = createdCategory.Description,
                IsActive = createdCategory.IsActive,
                CreatedAt = createdCategory.CreatedAt,
                UpdatedAt = createdCategory.UpdatedAt
            };
        }

        public async Task<CategoryDTO?> UpdateCategoryAsync(Guid id, UpdateCategoryDTO updateCategoryDto)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) return null;

            if (!string.IsNullOrEmpty(updateCategoryDto.Name))
                category.Name = updateCategoryDto.Name;

            if (updateCategoryDto.Description != null)
                category.Description = updateCategoryDto.Description;

            if (updateCategoryDto.IsActive.HasValue)
                category.IsActive = updateCategoryDto.IsActive.Value;

            category.UpdatedAt = DateTime.UtcNow;

            var updatedCategory = await _categoryRepository.UpdateAsync(category);
            await _unitOfWork.SaveChangesAsync();

            return new CategoryDTO
            {
                Id = updatedCategory.Id,
                Name = updatedCategory.Name,
                Description = updatedCategory.Description,
                IsActive = updatedCategory.IsActive,
                CreatedAt = updatedCategory.CreatedAt,
                UpdatedAt = updatedCategory.UpdatedAt
            };
        }

        public async Task<bool> DeleteCategoryAsync(Guid id)
        {
            var result = await _categoryRepository.DeleteAsync(id);
            if (result)
            {
                await _unitOfWork.SaveChangesAsync();
            }
            return result;
        }
    }
}
