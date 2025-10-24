using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using User.Application.DTOs;
using User.Application.Services.Interfaces;
using LoginRequest = User.Application.DTOs.LoginRequest;

namespace User.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
       
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto register)
        {
            try
            {
                await _authService.RegisterAsync(register);
                return Ok(new { message = "User registered successfully" });


            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });

            }
        }

    

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest, IAuthService _authService)
        {
            try
            {
                var response = await _authService.LoginAsync(loginRequest);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
