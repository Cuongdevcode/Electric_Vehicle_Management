using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Application.IRepositories;
using User.Domain.Entities;
using User.Infrastructure.Persistence;

namespace User.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly UserDbContext _context;

        public RoleRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task<Role?> GetRoleByNameAsync(string roleName)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
        }
        public async Task AddAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
        }
        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _context.Roles.AnyAsync(r => r.Name == roleName);
        }
        
    }
}
