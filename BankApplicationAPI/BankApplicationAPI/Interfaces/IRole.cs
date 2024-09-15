using BankApplicationAPI.Models;

namespace BankApplicationAPI.Interfaces
{
    public interface IRole
    {
        Task<IEnumerable<Role>> GetRolesAsync();
        Task<IEnumerable<Role>> GetRoleByRoleIdAsync(int RoleId);
        Task<Role> UpdateRoleAsync(Role role);
        Task<Boolean> DeleteRoleAsync(Role role);
        Task<Boolean> CreateRoleAsync(Role role);
        Task<Role> GetRoleAsync(int? RoleId = null,
                                string? RoleName = null);
    }
}
