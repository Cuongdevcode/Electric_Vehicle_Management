using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using User.Infrastructure.Persistence;

namespace User.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly UserDbContext _context;
        public HealthController(UserDbContext context)
        {
            _context = context;
        }

        [HttpGet("dbcheck")]
        public async Task<IActionResult> CheckDatabaseConnection()
        {
            try
            {
                var canConnect = await _context.Database.CanConnectAsync();
                return Ok(new { Connected = canConnect });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Connected = false, Error = ex.Message });
            }
        }
    }
}

