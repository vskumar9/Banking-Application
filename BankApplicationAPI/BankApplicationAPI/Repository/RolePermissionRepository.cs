using Microsoft.EntityFrameworkCore;
using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Repository
{
    public class RolePermissionRepository : IRolePermission
    {
        private readonly SunBankContext _context;
        private readonly ILogger<RolePermissionRepository> _logger;

        public RolePermissionRepository(SunBankContext context, ILogger<RolePermissionRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Create a new RolePermission
        public async Task<bool> CreateRolePermissionAsync(RolePermission rolePermission)
        {
            try
            {
                if (rolePermission == null)
                {
                    throw new ArgumentNullException(nameof(rolePermission), "RolePermission cannot be null");
                }

                await _context.RolePermissions.AddAsync(rolePermission);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error creating RolePermission");
                throw new Exception("An error occurred while creating the RolePermission.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        // Delete a RolePermission
        public async Task<bool> DeleteRolePermissionAsync(RolePermission rolePermission)
        {
            try
            {
                if (rolePermission == null)
                {
                    throw new ArgumentNullException(nameof(rolePermission), "RolePermission cannot be null");
                }

                var existingRolePermission = await _context.RolePermissions.FindAsync(rolePermission.RolePermissionId);
                if (existingRolePermission == null)
                {
                    throw new KeyNotFoundException("RolePermission not found");
                }

                _context.RolePermissions.Remove(existingRolePermission);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error deleting RolePermission");
                throw new Exception("An error occurred while deleting the RolePermission.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        // Get RolePermission by optional parameters
        public async Task<RolePermission> GetRolePermissionAsync(int? RolePermissionId = null, int? RoleId = null, int? PermissionId = null)
        {
            try
            {
                var query = _context.RolePermissions.AsQueryable();

                if (RolePermissionId.HasValue)
                    query = query.Where(rp => rp.RolePermissionId == RolePermissionId.Value);

                if (RoleId.HasValue)
                    query = query.Where(rp => rp.RoleId == RoleId.Value);

                if (PermissionId.HasValue)
                    query = query.Where(rp => rp.PermissionId == PermissionId.Value);

                return await query.Include(rp => rp.Permission)
                                  .Include(rp => rp.Role)
                                  .FirstOrDefaultAsync()
                       ?? throw new KeyNotFoundException("RolePermission not found with the provided criteria.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching RolePermission");
                throw new Exception("An error occurred while fetching the RolePermission.", ex);
            }
        }

        // Get RolePermission by RolePermissionId
        public async Task<IEnumerable<RolePermission>> GetRolePermissionByRolePermissionIdAsync(int RolePermissionId)
        {
            try
            {
                if (RolePermissionId <= 0)
                {
                    throw new ArgumentException("Invalid RolePermissionId", nameof(RolePermissionId));
                }

                return await _context.RolePermissions
                                     .Where(rp => rp.RolePermissionId == RolePermissionId)
                                     .Include(rp => rp.Permission)
                                     .Include(rp => rp.Role)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching RolePermission by ID");
                throw new Exception("An error occurred while fetching the RolePermission by ID.", ex);
            }
        }

        // Get all RolePermissions
        public async Task<IEnumerable<RolePermission>> GetRolePermissionsAsync()
        {
            try
            {
                return await _context.RolePermissions
                                     .Include(rp => rp.Permission)
                                     .Include(rp => rp.Role)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all RolePermissions");
                throw new Exception("An error occurred while fetching all RolePermissions.", ex);
            }
        }

        // Update an existing RolePermission
        public async Task<RolePermission> UpdateRolePermissionAsync(RolePermission rolePermission)
        {
            try
            {
                if (rolePermission == null)
                {
                    throw new ArgumentNullException(nameof(rolePermission), "RolePermission cannot be null");
                }

                var existingRolePermission = await _context.RolePermissions.FindAsync(rolePermission.RolePermissionId);
                if (existingRolePermission == null)
                {
                    throw new KeyNotFoundException("RolePermission not found");
                }

                _context.Entry(existingRolePermission).CurrentValues.SetValues(rolePermission);
                await _context.SaveChangesAsync();
                return existingRolePermission;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error updating RolePermission");
                throw new Exception("An error occurred while updating the RolePermission.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw new Exception("An unexpected error occurred.", ex);
            }
        }
    }
}
