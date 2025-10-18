using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using User.Domain.Entities;

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
            var fullName = User.FindFirstValue(ClaimTypes.Name);
            var email = User.FindFirstValue(ClaimTypes.Email);
            var rlole = User.FindFirstValue(ClaimTypes.Role);

            return Ok(
                new
                {
                    Message = "Bạn đã qua vòng đăng nhâpj hẹ hẹ hẹ",
                    Name = fullName,
                    Email = email,
                    Role = rlole
                }
                );


        }

        [HttpGet("admin-only")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult GetAdminData()
        {
            return Ok("Admin Page");
        }


    }
}
