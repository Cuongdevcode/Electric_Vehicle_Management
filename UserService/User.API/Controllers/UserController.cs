using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace User.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("me")]
        [Authorize]
        public IActionResult GetProfile()
        {
            var fullName = User.FindFirstValue(ClaimTypes.Name) ?? string.Empty;
            var email = User.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
            var role = User.FindFirstValue(ClaimTypes.Role) ?? string.Empty;

            return Ok(new
            {
                Message = "Bạn đã qua vòng đăng nhập",
                Name = fullName,
                Email = email,
                Role = role
            });
        }

        [HttpGet("admin-only")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult GetAdminData()
        {
            return Ok("Admin Page");
        }
    }
}
