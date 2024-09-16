using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Services
{
    public class PermissionService
    {
        private readonly IPermission _permission;

        public PermissionService(IPermission permission)
        {
            _permission = permission;
        }

        public async Task<bool> CreatePermissionAsync(Permission permission)
        {
            try
            {
                return await _permission.CreatePermissionAsync(permission);
            }
            catch { throw; }
        }

        public async Task<bool> DeletePermissionAsync(Permission permission)
        {
            try
            {
                return await _permission.DeletePermissionAsync(permission);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<Permission>> GetPermissionAsync(int? PermissionId = null, string? PermissionName = null)
        {
            try
            {
                return await _permission.GetPermissionAsync(PermissionId, PermissionName);
            }
            catch { throw; }
        }

        public async Task<Permission> GetPermissionByPermissionIdAsync(int PermissionId)
        {
            try
            {
                return await _permission.GetPermissionByPermissionIdAsync(PermissionId);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            try
            {
                return await _permission.GetPermissionsAsync();
            }
            catch { throw; }
        }

        public async Task<Permission> UpdatePermissionAsync(Permission permission)
        {
            try
            {
                return await _permission.UpdatePermissionAsync(permission);
            }
            catch { throw; }
        }
    }
}
