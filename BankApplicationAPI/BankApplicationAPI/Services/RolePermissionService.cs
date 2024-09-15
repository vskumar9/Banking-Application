using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Services
{
    public class RolePermissionService
    {
        private readonly IRolePermission _permission;

        public RolePermissionService(IRolePermission permission)
        {
            _permission = permission;
        }

        public async Task<bool> CreateRolePermissionAsync(RolePermission rolePermission)
        {
            try
            {
                return await _permission.CreateRolePermissionAsync(rolePermission);
            }
            catch { throw; }
        }

        public async Task<bool> DeleteRolePermissionAsync(RolePermission rolePermission)
        {
            try
            {
                return await _permission.DeleteRolePermissionAsync(rolePermission);
            }
            catch { throw; }
        }

        public async Task<RolePermission> GetRolePermissionAsync(int? RolePermissionId = null, int? RoleId = null, int? PermissionId = null)
        {
            try
            {
                return await _permission.GetRolePermissionAsync(RolePermissionId, RoleId, PermissionId);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<RolePermission>> GetRolePermissionByRolePermissionIdAsync(int RolePermissionId)
        {
            try
            {
                return await _permission.GetRolePermissionByRolePermissionIdAsync(RolePermissionId);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<RolePermission>> GetRolePermissionsAsync()
        {
            try
            {
                return await _permission.GetRolePermissionsAsync();
            }
            catch { throw; }
        }

        public async Task<RolePermission> UpdateRolePermissionAsync(RolePermission rolePermission)
        {
            try
            {
                return await _permission.UpdateRolePermissionAsync(rolePermission);
            }
            catch { throw; }
        }

    }
}
