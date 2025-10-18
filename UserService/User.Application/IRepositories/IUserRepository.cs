using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Application.IRepositories
{
    public interface IUserRepository
    {
        Task<Domain.Entities.User> GetUserByEmailAsync(string email);
        Task AddUserAsync(Domain.Entities.User user);
    }
}
