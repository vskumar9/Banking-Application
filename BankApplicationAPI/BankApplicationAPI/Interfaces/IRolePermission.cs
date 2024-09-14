using BankApplicationAPI.Models;

namespace BankApplicationAPI.Interfaces
{
    public interface IRolePermission
    {
        Task<IEnumerable<RolePermission>> GetRolePermissionsAsync();
        Task<IEnumerable<RolePermission>> GetRolePermissionByRolePermissionIdAsync(int RolePermissionId);
        Task<RolePermission> UpdateRolePermissionAsync(RolePermission rolePermission);
        Task<Boolean> DeleteRolePermissionAsync(RolePermission rolePermission);
        Task<Boolean> CreateRolePermissionAsync(RolePermission rolePermission);
        Task<RolePermission> GetRolePermissionAsync(int? RolePermissionId = null,
                                                    int? RoleId = null,
                                                    int? PermissionId = null);
    }
}
