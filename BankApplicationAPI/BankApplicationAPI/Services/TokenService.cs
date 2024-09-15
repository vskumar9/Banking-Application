using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Services
{
    public class TokenService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly SunBankContext _context;

        public TokenService(IConfiguration configuration, SunBankContext context)
        {
            _key = new SymmetricSecurityKey(UTF8Encoding.UTF8.GetBytes(configuration["Key"]!));
            _context = context;
        }

        public async Task<string> GenerateTokenAsync(Customer user)
        {
            try
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.EmailAddress!),
                    new Claim(JwtRegisteredClaimNames.Name, user.CustomerFirstName!),
                    new Claim(ClaimTypes.Role, "customer"),
                    new Claim(ClaimTypes.PrimarySid, user.CustomerId!),
                    new Claim("CustomerId", user.CustomerId!)
                };

                return await GenerateTokenAsync(claims);

            }
            catch 
            {
                throw;
            }
        }

        public async Task<string> GenerateAdminTokenAsync(Employee emp)
        {
            try
            {
                var employee = await _context.Employees
                .Include(e => e.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(e => e.EmployeeId == emp.EmployeeId);

                if (employee == null)
                {
                    throw new InvalidOperationException("Employee not found.");
                }

                // Retrieve roles
                var roles = employee.UserRoles.Select(ur => ur.Role.RoleName).ToList();

                // Create the list of claims
                var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, employee.EmailAddress ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Name, employee.EmployeeFirstName ?? string.Empty),
                new Claim(ClaimTypes.PrimarySid, employee.EmployeeId),
                new Claim("EmployeeId", employee.EmployeeId)
            };

                // Add roles as claims
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                return await GenerateTokenAsync(claims);
            }
            catch
            {
                throw;
            }
        }

        private async Task<string> GenerateTokenAsync(IEnumerable<Claim> claims)
        {
            try
            {
                var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch
            {
                throw;
            }
        }

        public async Task<Customer?> ValidateCustomerAsync(string customerId, string password)
        {
            try
            {
                var user = await _context.Customers
                    .SingleOrDefaultAsync(c => c.CustomerId == customerId);

                if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                {
                    return user;
                }

                return null;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Employee?> ValidateAdminAsync(string employeeId, string password)
        {
            try
            {
                var employee = await _context.Employees
                    .SingleOrDefaultAsync(a => a.EmployeeId == employeeId);

                if (employee != null && BCrypt.Net.BCrypt.Verify(password, employee.PasswordHash))
                {
                    return employee;
                }

                return null;
            }
            catch
            {
                throw;
            }
        }
    }
}
