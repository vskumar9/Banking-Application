using Microsoft.EntityFrameworkCore;
using BankApplicationAPI.Models;
using BankApplicationAPI.Interfaces;

namespace BankApplicationAPI.Repository
{
    public class UserRoleRepository : IUserRole
    {
        private readonly SunBankContext _context;
        private readonly ILogger<UserRoleRepository> _logger;

        public UserRoleRepository(SunBankContext context, ILogger<UserRoleRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Create a new UserRole
        public async Task<bool> CreateUserRoleAsync(UserRole userRole)
        {
            try
            {
                _context.UserRoles.Add(userRole);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating UserRole");
                return false;
            }
        }

        // Delete an existing UserRole
        public async Task<bool> DeleteUserRoleAsync(UserRole userRole)
        {
            try
            {
                _context.UserRoles.Remove(userRole);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting UserRole");
                return false;
            }
        }

        // Get a specific UserRole based on optional parameters
        public async Task<UserRole> GetUserRoleAsync(int? UserRoleId = null, string? EmployeeId = null, int? RoleId = null)
        {
            try
            {
                var query = _context.UserRoles.AsQueryable();

                if (UserRoleId.HasValue)
                {
                    query = query.Where(ur => ur.UserRoleId == UserRoleId.Value);
                }

                if (!string.IsNullOrEmpty(EmployeeId))
                {
                    query = query.Where(ur => ur.EmployeeId == EmployeeId);
                }

                if (RoleId.HasValue)
                {
                    query = query.Where(ur => ur.RoleId == RoleId.Value);
                }

                return await query.Include(ur => ur.Employee).Include(ur => ur.Role)
                                  .FirstOrDefaultAsync() ?? throw new NullReferenceException("UserRoles not found with the provided criteria.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving UserRole");
                throw;
            }
        }

        // Get UserRoles by UserRoleId
        public async Task<IEnumerable<UserRole>> GetUserRoleByUserRoleIdAsync(int UserRoleId)
        {
            try
            {
                return await _context.UserRoles
                    .Where(ur => ur.UserRoleId == UserRoleId)
                    .Include(ur => ur.Employee)
                    .Include(ur => ur.Role)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving UserRoles by UserRoleId");
                throw;
            }
        }

        // Get all UserRoles
        public async Task<IEnumerable<UserRole>> GetUserRolesAsync()
        {
            try
            {
                return await _context.UserRoles
                    .Include(ur => ur.Employee)
                    .Include(ur => ur.Role)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving UserRoles");
                throw;
            }
        }

        // Update an existing UserRole
        public async Task<UserRole> UpdateUserRoleAsync(UserRole userRole)
        {
            try
            {
                var existingUserRole = await _context.UserRoles
                    .FirstOrDefaultAsync(ur => ur.UserRoleId == userRole.UserRoleId);

                if (existingUserRole == null)
                {
                    throw new KeyNotFoundException("UserRole not found");
                }

                // Update fields
                existingUserRole.EmployeeId = userRole.EmployeeId;
                existingUserRole.RoleId = userRole.RoleId;

                _context.UserRoles.Update(existingUserRole);
                await _context.SaveChangesAsync();

                return existingUserRole;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating UserRole");
                throw;
            }
        }
    }
}
