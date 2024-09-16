using BankApplicationAPI.Models;

namespace BankApplicationAPI.Interfaces
{
    public interface IRole
    {
        Task<IEnumerable<Role>> GetRolesAsync();
        Task<Role> GetRoleByRoleIdAsync(int RoleId);
        Task<Role> UpdateRoleAsync(Role role);
        Task<Boolean> DeleteRoleAsync(Role role);
        Task<Boolean> CreateRoleAsync(Role role);
        Task<IEnumerable<Role>> GetRoleAsync(int? RoleId = null,
                                string? RoleName = null);
    }
}
