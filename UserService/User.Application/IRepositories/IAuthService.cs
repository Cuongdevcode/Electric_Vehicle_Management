using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Application.IRepositories
{
    public interface IAuthService
    {
        Task<DTOs.AuthDTOs.LoginResponse> LoginAsync(DTOs.AuthDTOs.LoginRequest request);
        Task RegisterAsync(DTOs.AuthDTOs.RegisterDto request);  
    }
}
