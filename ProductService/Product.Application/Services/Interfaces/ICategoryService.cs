using Product.Application.DTOs;

namespace Product.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
        Task<CategoryDTO?> GetCategoryByIdAsync(Guid id);
        Task<CategoryDTO> CreateCategoryAsync(CreateCategoryDTO createCategoryDto);
        Task<CategoryDTO?> UpdateCategoryAsync(Guid id, UpdateCategoryDTO updateCategoryDto);
        Task<bool> DeleteCategoryAsync(Guid id);
    }
}
