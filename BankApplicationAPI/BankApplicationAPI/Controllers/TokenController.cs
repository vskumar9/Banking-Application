using BankApplicationAPI.Models;
using BankApplicationAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ILogger<TokenController> _logger;
        private readonly TokenService _tokenService;
        private readonly SunBankContext _context;

        public TokenController(TokenService tokenService, SunBankContext context, ILogger<TokenController> logger)
        {
            _tokenService = tokenService;
            _context = context;
            _logger = logger;
        }


        [HttpPost("customer/login")]
        public async Task<IActionResult> CustomerLogin([FromBody] Login model)
        {
            try
            {
                var customer = await _tokenService.ValidateCustomerAsync(model.Id, model.Password);
                if (customer != null)
                {
                    var token = await _tokenService.GenerateTokenAsync(customer);
                    customer.LastLoginDate = DateTime.UtcNow;
                    _context.Entry(customer).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Ok(new { Token = token });
                }

                return Unauthorized("Invalid credentials.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while registering the rental.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }

        [HttpPost("employee/login")]
        public async Task<IActionResult> EmployeeLogin([FromBody] Login model)
        {
            try
            {
                var employee = await _tokenService.ValidateAdminAsync(model.Id!, model.Password!);
                if (employee != null)
                {
                    var token = await _tokenService.GenerateAdminTokenAsync(employee);
                    employee.LastLoginDate = DateTime.Now;
                    _context.Entry(employee).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Ok(new { Token = token });
                }

                return Unauthorized("Invalid credentials.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while registering the rental.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }
    }
}
