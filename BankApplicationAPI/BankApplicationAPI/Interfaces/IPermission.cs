using BankApplicationAPI.Models;

namespace BankApplicationAPI.Interfaces
{
    public interface IPermission
    {
        Task<IEnumerable<Permission>> GetPermissionsAsync();
        Task<Permission> GetPermissionByPermissionIdAsync(int PermissionId);
        Task<Permission> UpdatePermissionAsync(Permission permission);
        Task<Boolean> DeletePermissionAsync(Permission permission);
        Task<Boolean> CreatePermissionAsync(Permission permission);
        Task<IEnumerable<Permission>> GetPermissionAsync(int? PermissionId = null,
                                      string? PermissionName = null);
    }
}
