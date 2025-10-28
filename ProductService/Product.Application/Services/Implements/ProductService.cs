using Product.Application.DTOs;
using Product.Application.IRepositories;
using Product.Application.Services.Interfaces;

namespace Product.Application.Services.Implements
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWorkRepository _unitOfWork;

        public ProductService(
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IUnitOfWorkRepository unitOfWork)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                ImageUrl = p.ImageUrl,
                IsActive = p.IsActive,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name ?? string.Empty,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            });
        }

        public async Task<ProductDTO?> GetProductByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return null;

            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                ImageUrl = product.ImageUrl,
                IsActive = product.IsActive,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name ?? string.Empty,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsByCategoryIdAsync(Guid categoryId)
        {
            var products = await _productRepository.GetByCategoryIdAsync(categoryId);
            return products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                ImageUrl = p.ImageUrl,
                IsActive = p.IsActive,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name ?? string.Empty,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            });
        }

        public async Task<ProductDTO> CreateProductAsync(CreateProductDTO createProductDto)
        {
            // Validate category exists
            if (!await _categoryRepository.ExistsAsync(createProductDto.CategoryId))
            {
                throw new ArgumentException("Category not found");
            }

            var product = new Domain.Entities.Product
            {
                Id = Guid.NewGuid(),
                Name = createProductDto.Name,
                Description = createProductDto.Description,
                Price = createProductDto.Price,
                StockQuantity = createProductDto.StockQuantity,
                ImageUrl = createProductDto.ImageUrl,
                CategoryId = createProductDto.CategoryId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            var createdProduct = await _productRepository.CreateAsync(product);
            await _unitOfWork.SaveChangesAsync();

            return new ProductDTO
            {
                Id = createdProduct.Id,
                Name = createdProduct.Name,
                Description = createdProduct.Description,
                Price = createdProduct.Price,
                StockQuantity = createdProduct.StockQuantity,
                ImageUrl = createdProduct.ImageUrl,
                IsActive = createdProduct.IsActive,
                CategoryId = createdProduct.CategoryId,
                CategoryName = createdProduct.Category?.Name ?? string.Empty,
                CreatedAt = createdProduct.CreatedAt,
                UpdatedAt = createdProduct.UpdatedAt
            };
        }

        public async Task<ProductDTO?> UpdateProductAsync(Guid id, UpdateProductDTO updateProductDto)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return null;

            // Validate category if changing
            if (updateProductDto.CategoryId.HasValue && 
                updateProductDto.CategoryId.Value != product.CategoryId)
            {
                if (!await _categoryRepository.ExistsAsync(updateProductDto.CategoryId.Value))
                {
                    throw new ArgumentException("Category not found");
                }
                product.CategoryId = updateProductDto.CategoryId.Value;
            }

            if (!string.IsNullOrEmpty(updateProductDto.Name))
                product.Name = updateProductDto.Name;

            if (updateProductDto.Description != null)
                product.Description = updateProductDto.Description;

            if (updateProductDto.Price.HasValue)
                product.Price = updateProductDto.Price.Value;

            if (updateProductDto.StockQuantity.HasValue)
                product.StockQuantity = updateProductDto.StockQuantity.Value;

            if (updateProductDto.ImageUrl != null)
                product.ImageUrl = updateProductDto.ImageUrl;

            if (updateProductDto.IsActive.HasValue)
                product.IsActive = updateProductDto.IsActive.Value;

            product.UpdatedAt = DateTime.UtcNow;

            var updatedProduct = await _productRepository.UpdateAsync(product);
            await _unitOfWork.SaveChangesAsync();

            return new ProductDTO
            {
                Id = updatedProduct.Id,
                Name = updatedProduct.Name,
                Description = updatedProduct.Description,
                Price = updatedProduct.Price,
                StockQuantity = updatedProduct.StockQuantity,
                ImageUrl = updatedProduct.ImageUrl,
                IsActive = updatedProduct.IsActive,
                CategoryId = updatedProduct.CategoryId,
                CategoryName = updatedProduct.Category?.Name ?? string.Empty,
                CreatedAt = updatedProduct.CreatedAt,
                UpdatedAt = updatedProduct.UpdatedAt
            };
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            var result = await _productRepository.DeleteAsync(id);
            if (result)
            {
                await _unitOfWork.SaveChangesAsync();
            }
            return result;
        }
    }
}
