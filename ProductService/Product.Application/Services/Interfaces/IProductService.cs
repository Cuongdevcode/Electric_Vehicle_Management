using Product.Application.DTOs;

namespace Product.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO?> GetProductByIdAsync(Guid id);
        Task<IEnumerable<ProductDTO>> GetProductsByCategoryIdAsync(Guid categoryId);
        Task<ProductDTO> CreateProductAsync(CreateProductDTO createProductDto);
        Task<ProductDTO?> UpdateProductAsync(Guid id, UpdateProductDTO updateProductDto);
        Task<bool> DeleteProductAsync(Guid id);
    }
}
