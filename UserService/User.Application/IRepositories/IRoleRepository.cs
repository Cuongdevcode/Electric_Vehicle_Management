using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Entities;

namespace User.Application.IRepositories
{
    public interface IRoleRepository
    {
        Task<Role?> GetRoleByNameAsync(string roleName);
        Task AddAsync(Role role);
        Task<bool> RoleExistsAsync(string roleName);
    }
}
