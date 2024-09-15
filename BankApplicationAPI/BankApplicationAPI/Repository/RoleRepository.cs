using Microsoft.EntityFrameworkCore;
using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Repository
{
    public class RoleRepository : IRole
    {
        private readonly SunBankContext _context;
        private readonly ILogger<RoleRepository> _logger;

        public RoleRepository(SunBankContext context, ILogger<RoleRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Create a new Role
        public async Task<bool> CreateRoleAsync(Role role)
        {
            try
            {
                if (role == null)
                {
                    throw new ArgumentNullException(nameof(role), "Role cannot be null");
                }

                await _context.Roles.AddAsync(role);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error creating role");
                throw new Exception("An error occurred while creating the role.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        // Delete a Role
        public async Task<bool> DeleteRoleAsync(Role role)
        {
            try
            {
                if (role == null)
                {
                    throw new ArgumentNullException(nameof(role), "Role cannot be null");
                }

                var existingRole = await _context.Roles.FindAsync(role.RoleId);
                if (existingRole == null)
                {
                    throw new KeyNotFoundException("Role not found");
                }

                _context.Roles.Remove(existingRole);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error deleting role");
                throw new Exception("An error occurred while deleting the role.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        // Get Role by optional parameters
        public async Task<Role> GetRoleAsync(int? RoleId = null, string? RoleName = null)
        {
            try
            {
                var query = _context.Roles.AsQueryable();

                if (RoleId.HasValue)
                    query = query.Where(r => r.RoleId == RoleId.Value);

                if (!string.IsNullOrEmpty(RoleName))
                    query = query.Where(r => r.RoleName == RoleName);

                return await query.Include(r => r.RolePermissions)
                                  .Include(r => r.UserRoles)
                                  .FirstOrDefaultAsync()
                       ?? throw new KeyNotFoundException("Role not found with the provided criteria.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching role");
                throw new Exception("An error occurred while fetching the role.", ex);
            }
        }

        // Get Role by RoleId
        public async Task<IEnumerable<Role>> GetRoleByRoleIdAsync(int RoleId)
        {
            try
            {
                if (RoleId <= 0)
                {
                    throw new ArgumentException("Invalid RoleId", nameof(RoleId));
                }

                return await _context.Roles
                                     .Where(r => r.RoleId == RoleId)
                                     .Include(r => r.RolePermissions)
                                     .Include(r => r.UserRoles)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching role by ID");
                throw new Exception("An error occurred while fetching the role by ID.", ex);
            }
        }

        // Get all Roles
        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            try
            {
                return await _context.Roles
                                     .Include(r => r.RolePermissions)
                                     .Include(r => r.UserRoles)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all roles");
                throw new Exception("An error occurred while fetching all roles.", ex);
            }
        }

        // Update an existing Role
        public async Task<Role> UpdateRoleAsync(Role role)
        {
            try
            {
                if (role == null)
                {
                    throw new ArgumentNullException(nameof(role), "Role cannot be null");
                }

                var existingRole = await _context.Roles.FindAsync(role.RoleId);
                if (existingRole == null)
                {
                    throw new KeyNotFoundException("Role not found");
                }

                _context.Entry(existingRole).CurrentValues.SetValues(role);
                await _context.SaveChangesAsync();
                return existingRole;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error updating role");
                throw new Exception("An error occurred while updating the role.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw new Exception("An unexpected error occurred.", ex);
            }
        }
    }
}
