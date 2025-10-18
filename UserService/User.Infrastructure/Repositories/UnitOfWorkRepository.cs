using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Application.IRepositories;
using User.Infrastructure.Persistence;

namespace User.Infrastructure.Repositories
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        private readonly UserDbContext _context;
        public UnitOfWorkRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // throw new NotImplementedException();
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
