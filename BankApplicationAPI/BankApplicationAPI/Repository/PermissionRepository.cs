using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Repository
{
    public class PermissionRepository : IPermission
    {
        public Task<bool> CreatePermissionAsync(Permission permission)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePermissionAsync(Permission permission)
        {
            throw new NotImplementedException();
        }

        public Task<Permission> GetPermissionAsync(int? PermissionId = null, string? PermissionName = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Permission>> GetPermissionByPermissionIdAsync(int PermissionId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Permission> UpdatePermissionAsync(Permission permission)
        {
            throw new NotImplementedException();
        }
    }
}
