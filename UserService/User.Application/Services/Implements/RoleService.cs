using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Application.DTOs;
using User.Application.IRepositories;
using User.Application.Services.Interfaces;

namespace User.Application.Services.Implements
{
    using global::User.Domain.Entities;

    namespace User.Application.Services
    {
        public class RoleService : IRoleService
        {
            private readonly IRoleRepository _roleRepository;
            private readonly IUnitOfWorkRepository _unitOfWork;

            public RoleService(IRoleRepository roleRepository, IUnitOfWorkRepository unitOfWork)
            {
                _roleRepository = roleRepository;
                _unitOfWork = unitOfWork;
            }

          

            public async Task CreateRole(CreateRoleDto request)
            {
                // 1. Chuẩn hóa tên Role (viết hoa)
                var roleNameUpper = request.Name.ToUpper();

                // 2. Kiểm tra xem Role đã tồn tại chưa
                if (await _roleRepository.RoleExistsAsync(roleNameUpper))
                {
                    throw new Exception($"Role '{request.Name}' đã tồn tại.");
                }

                // 3. Tạo Entity mới
                var newRole = new Role
                {
                    Name = roleNameUpper
                };

                // 4. Thêm vào DB và lưu
                await _roleRepository.AddAsync(newRole);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}