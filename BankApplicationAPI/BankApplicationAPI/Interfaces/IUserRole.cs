using BankApplicationAPI.Models;

namespace BankApplicationAPI.Interfaces
{
    public interface IUserRole
    {
        Task<IEnumerable<UserRole>> GetUserRolesAsync();
        Task<UserRole> GetUserRoleByUserRoleIdAsync(int UserRoleId);
        Task<UserRole> UpdateUserRoleAsync(UserRole userRole);
        Task<Boolean> DeleteUserRoleAsync(UserRole userRole);
        Task<Boolean> CreateUserRoleAsync(UserRole userRole);
        Task<IEnumerable<UserRole>> GetUserRoleAsync(int? UserRoleId = null,
                                        string? EmployeeId = null,
                                        int? RoleId = null);
    }
}
