using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Repository
{
    public class RolePermissionRepository : IRolePermission
    {
        public Task<bool> CreateRolePermissionAsync(RolePermission rolePermission)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteRolePermissionAsync(RolePermission rolePermission)
        {
            throw new NotImplementedException();
        }

        public Task<RolePermission> GetRolePermissionAsync(int? RolePermissionId = null, int? RoleId = null, int? PermissionId = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RolePermission>> GetRolePermissionByRolePermissionIdAsync(int RolePermissionId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RolePermission>> GetRolePermissionsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RolePermission> UpdateRolePermissionAsync(RolePermission rolePermission)
        {
            throw new NotImplementedException();
        }
    }
}
