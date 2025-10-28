using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace User.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthDebugController : ControllerBase
{
  
    [HttpGet("header")]
    [AllowAnonymous]
    public IActionResult Header()
        => Ok(new { Authorization = Request.Headers["Authorization"].ToString() });

  
    [HttpGet("check")]
    [AllowAnonymous]
    public IActionResult Check()
        => Ok(new { IsAuthenticated = User?.Identity?.IsAuthenticated ?? false });

    
    [HttpGet("whoami")]
    [Authorize]
    public IActionResult WhoAmI()
        => Ok(new
        {
            Name = User.FindFirstValue(ClaimTypes.Name),
            Email = User.FindFirstValue(ClaimTypes.Email),
            Roles = User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToArray()
        });
}