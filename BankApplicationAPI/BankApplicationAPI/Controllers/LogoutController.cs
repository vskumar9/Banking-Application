using BankApplicationAPI.Models;
using BankApplicationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogoutController : ControllerBase
    {
        private readonly ILogger<LogoutController> _logger;
        private readonly TokenService _tokenService;
        private readonly SunBankContext _context;

        public LogoutController(TokenService tokenService, SunBankContext context, ILogger<LogoutController> logger)
        {
            _tokenService = tokenService;
            _context = context;
            _logger = logger;
        }

        [HttpPost("employee")]
        [Authorize] 
        public async Task<IActionResult> EmployeeLogout()
        {
            try
            {
                var userId = User.Identity!.Name; 

                var user = await _context.Employees.FirstOrDefaultAsync(u => u.EmployeeId == userId);
                if (user == null)
                {
                    return BadRequest("User not found.");
                }

                user.IsActive = false;
                await _context.SaveChangesAsync();

                return Ok(new { message = "Logged out successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while registering the rental.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }

        [HttpPost("customer")]
        [Authorize]
        public async Task<IActionResult> CustomerLogout()
        {
            try
            {
                var userId = User.Identity!.Name; 

                var user = await _context.Customers.FirstOrDefaultAsync(u => u.CustomerId == userId);
                if (user == null)
                {
                    return BadRequest("User not found.");
                }

                user.IsActive = false;
                await _context.SaveChangesAsync();

                return Ok(new { message = "Logged out successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while registering the rental.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }

    }
}
