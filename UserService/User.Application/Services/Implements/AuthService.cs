using User.Application.DTOs;
using User.Application.Interfaces;
using User.Application.IRepositories;
using User.Application.Services.Interfaces;
using User.Domain.Entities;
//using User.Application.DTOs.AuthDTOs;
namespace User.Application.Services.Implements
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        // Tiêm (Inject) IRepository, không tiêm DbContext
        public AuthService(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IUnitOfWorkRepository unitOfWorkRepository,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _unitOfWorkRepository = unitOfWorkRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task RegisterAsync(RegisterDto request)
        {
            // 1. Kiểm tra email 
            if (await _userRepository.GetUserByEmailAsync(request.Email) != null)
            {
                throw new Exception("Email already exists.");
            }

            // 2. Lấy Role 
            var defaultRole = await _roleRepository.GetRoleByNameAsync("CUSTOMER");
            if (defaultRole == null)
            {
                throw new Exception("Default role not found.");
            }

            // 3. Băm mật khẩu
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // 4. Tạo User mới
            var user = new Domain.Entities.User
            {
                Email = request.Email,
                Password = passwordHash,
                RoleId = defaultRole.Id,
                IsActive = true
            };

            // 5. Thêm vào (dùng Repository)
            await _userRepository.AddUserAsync(user);

            // 6. Lưu thay đổi (dùng UnitOfWork)
            await _unitOfWorkRepository.SaveChangesAsync();
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            // 1. Tìm User 
            var user = await _userRepository.GetUserByEmailAsync(request.Email);

            // 2. Kiểm tra User và Mật khẩu
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                throw new Exception("Invalid email or password.");
            }

            // 3. Tạo Token
            var token = _jwtTokenGenerator.GenerateToken(user);

            // 4. Trả về Response
            return new LoginResponse
            {
                UserId = user.Id,
                Email = user.Email,
                Role = user.Role.Name, // Lấy tên Role
                Token = token
            };
        }

        //public Task<AuthDTOs.LoginResponse> LoginAsync(AuthDTOs.LoginRequest request)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task RegisterAsync(AuthDTOs.RegisterDto request)
        //{
        //    throw new NotImplementedException();
        //}
    }
}