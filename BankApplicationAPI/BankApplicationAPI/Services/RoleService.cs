using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Services
{
    public class RoleService
    {
        private readonly IRole _role;

        public RoleService(IRole role)
        {
            _role = role;
        }

        public async Task<bool> CreateRoleAsync(Role role)
        {
            try
            {
                return await _role.CreateRoleAsync(role);
            }
            catch { throw; }
        }

        public async Task<bool> DeleteRoleAsync(Role role)
        {
            try
            {
                return await _role.DeleteRoleAsync(role);
            }
            catch { throw; }
        }

        public async Task<Role> GetRoleAsync(int? RoleId = null, string? RoleName = null)
        {
            try
            {
                return await _role.GetRoleAsync(RoleId, RoleName);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<Role>> GetRoleByRoleIdAsync(int RoleId)
        {
            try
            {
                return await _role.GetRoleByRoleIdAsync(RoleId);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            try
            {
                return await _role.GetRolesAsync();
            }
            catch { throw; }
        }

        public async Task<Role> UpdateRoleAsync(Role role)
        {
            try
            {
                return await _role.UpdateRoleAsync(role);
            }
            catch { throw; }
        }
    }
}
