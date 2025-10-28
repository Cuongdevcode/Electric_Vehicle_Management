using Product.Domain.Entities;

namespace Product.Application.IRepositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Domain.Entities.Product>> GetAllAsync();
        Task<Domain.Entities.Product?> GetByIdAsync(Guid id);
        Task<IEnumerable<Domain.Entities.Product>> GetByCategoryIdAsync(Guid categoryId);
        Task<Domain.Entities.Product> CreateAsync(Domain.Entities.Product product);
        Task<Domain.Entities.Product> UpdateAsync(Domain.Entities.Product product);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
}
