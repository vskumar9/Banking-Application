using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Repository
{
    public class UserRoleRepository : IUserRole
    {
        public Task<bool> CreateUserRoleAsync(UserRole userRole)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUserRoleAsync(UserRole userRole)
        {
            throw new NotImplementedException();
        }

        public Task<UserRole> GetUserRoleAsync(int? UserRoleId = null, string? EmployeeId = null, int? RoleId = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserRole>> GetUserRoleByUserRoleIdAsync(int UserRoleId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserRole>> GetUserRolesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserRole> UpdateUserRoleAsync(UserRole userRole)
        {
            throw new NotImplementedException();
        }
    }
}
