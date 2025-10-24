using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Application.DTOs;

namespace User.Application.Services.Interfaces
{
    public interface IRoleService
    {
        Task CreateRole(CreateRoleDto roleDto);
    }
}
