namespace Product.Application.IRepositories
{
    public interface IUnitOfWorkRepository
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
