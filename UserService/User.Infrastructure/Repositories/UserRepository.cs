using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Application.IRepositories;
using User.Infrastructure.Persistence;

namespace User.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _context;

        public UserRepository(UserDbContext context)
        {
            _context = context;
        }


        public async Task AddUserAsync(Domain.Entities.User user)
        {
            //throw new NotImplementedException();
            await _context.Users.AddAsync(user);
        }

        // luu ca Role de User co the lay duoc name of Role
        public async Task<Domain.Entities.User> GetUserByEmailAsync(string email)
        {
            //throw new NotImplementedException();
            return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(e => e.Email == email);
        }
    }
}
