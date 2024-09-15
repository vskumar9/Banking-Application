using BankApplicationAPI.Models;

namespace BankApplicationAPI.Interfaces
{
    public interface IUserRole
    {
        Task<IEnumerable<UserRole>> GetUserRolesAsync();
        Task<IEnumerable<UserRole>> GetUserRoleByUserRoleIdAsync(int UserRoleId);
        Task<UserRole> UpdateUserRoleAsync(UserRole userRole);
        Task<Boolean> DeleteUserRoleAsync(UserRole userRole);
        Task<Boolean> CreateUserRoleAsync(UserRole userRole);
        Task<UserRole> GetUserRoleAsync(int? UserRoleId = null,
                                        string? EmployeeId = null,
                                        int? RoleId = null);
    }
}
