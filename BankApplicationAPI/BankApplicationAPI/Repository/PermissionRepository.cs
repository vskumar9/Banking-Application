using Microsoft.EntityFrameworkCore;
using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Repository
{
    public class PermissionRepository : IPermission
    {
        private readonly SunBankContext _context;
        private readonly ILogger<PermissionRepository> _logger;

        public PermissionRepository(SunBankContext context, ILogger<PermissionRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Create a new Permission
        public async Task<bool> CreatePermissionAsync(Permission permission)
        {
            try
            {
                if (permission == null)
                {
                    throw new ArgumentNullException(nameof(permission), "Permission cannot be null");
                }

                await _context.Permissions.AddAsync(permission);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error creating permission");
                throw new Exception("An error occurred while creating the permission.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        // Delete a Permission
        public async Task<bool> DeletePermissionAsync(Permission permission)
        {
            try
            {
                if (permission == null)
                {
                    throw new ArgumentNullException(nameof(permission), "Permission cannot be null");
                }

                var existingPermission = await _context.Permissions.FindAsync(permission.PermissionId);
                if (existingPermission == null)
                {
                    throw new KeyNotFoundException("Permission not found");
                }

                _context.Permissions.Remove(existingPermission);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error deleting permission");
                throw new Exception("An error occurred while deleting the permission.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        // Get Permission by optional parameters
        public async Task<Permission> GetPermissionAsync(int? PermissionId = null, string? PermissionName = null)
        {
            try
            {
                var query = _context.Permissions.AsQueryable();

                if (PermissionId.HasValue)
                    query = query.Where(p => p.PermissionId == PermissionId.Value);

                if (!string.IsNullOrEmpty(PermissionName))
                    query = query.Where(p => p.PermissionName == PermissionName);

                return await query.FirstOrDefaultAsync()
                       ?? throw new KeyNotFoundException("Permission not found with the provided criteria.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching permission");
                throw new Exception("An error occurred while fetching the permission.", ex);
            }
        }

        // Get Permission by PermissionId
        public async Task<IEnumerable<Permission>> GetPermissionByPermissionIdAsync(int PermissionId)
        {
            try
            {
                if (PermissionId <= 0)
                {
                    throw new ArgumentException("Invalid PermissionId", nameof(PermissionId));
                }

                return await _context.Permissions
                                     .Where(p => p.PermissionId == PermissionId)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching permission by ID");
                throw new Exception("An error occurred while fetching the permission by ID.", ex);
            }
        }

        // Get all Permissions
        public async Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            try
            {
                return await _context.Permissions.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all permissions");
                throw new Exception("An error occurred while fetching all permissions.", ex);
            }
        }

        // Update an existing Permission
        public async Task<Permission> UpdatePermissionAsync(Permission permission)
        {
            try
            {
                if (permission == null)
                {
                    throw new ArgumentNullException(nameof(permission), "Permission cannot be null");
                }

                var existingPermission = await _context.Permissions.FindAsync(permission.PermissionId);
                if (existingPermission == null)
                {
                    throw new KeyNotFoundException("Permission not found");
                }

                _context.Entry(existingPermission).CurrentValues.SetValues(permission);
                await _context.SaveChangesAsync();
                return existingPermission;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error updating permission");
                throw new Exception("An error occurred while updating the permission.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw new Exception("An unexpected error occurred.", ex);
            }
        }
    }
}
