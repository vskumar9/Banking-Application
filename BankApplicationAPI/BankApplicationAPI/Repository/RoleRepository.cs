using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Repository
{
    public class RoleRepository : IRole
    {
        public Task<bool> CreateRoleAsync(Role role)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteRoleAsync(Role role)
        {
            throw new NotImplementedException();
        }

        public Task<Role> GetRoleAsync(int? RoleId = null, string? RoleName = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Role>> GetRoleByRoleIdAsync(int RoleId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Role>> GetRolesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Role> UpdateRoleAsync(Role role)
        {
            throw new NotImplementedException();
        }
    }
}
