using BankApplicationAPI.Models;

namespace BankApplicationAPI.Interfaces
{
    public interface IPermission
    {
        Task<IEnumerable<Permission>> GetPermissionsAsync();
        Task<IEnumerable<Permission>> GetPermissionByPermissionIdAsync(int PermissionId);
        Task<Permission> UpdatePermissionAsync(Permission permission);
        Task<Boolean> DeletePermissionAsync(Permission permission);
        Task<Boolean> CreatePermissionAsync(Permission permission);
        Task<Permission> GetPermissionAsync(int? PermissionId = null,
                                      string? PermissionName = null);
    }
}
