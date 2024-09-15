using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Services
{
    public class UserRoleService
    {
        private readonly IUserRole _userRole;

        public UserRoleService(IUserRole userRole)
        {
            _userRole = userRole;
        }

        public async Task<bool> CreateUserRoleAsync(UserRole userRole)
        {
            try
            {
                return await _userRole.CreateUserRoleAsync(userRole);
            }
            catch { throw; }
        }

        public async Task<bool> DeleteUserRoleAsync(UserRole userRole)
        {
            try
            {
                return await _userRole.DeleteUserRoleAsync(userRole);
            }
            catch { throw; }
        }

        public async Task<UserRole> GetUserRoleAsync(int? UserRoleId = null, string? EmployeeId = null, int? RoleId = null)
        {
            try
            {
                return await _userRole.GetUserRoleAsync(UserRoleId, EmployeeId, RoleId);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<UserRole>> GetUserRoleByUserRoleIdAsync(int UserRoleId)
        {
            try
            {
                return await _userRole.GetUserRoleByUserRoleIdAsync(UserRoleId);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<UserRole>> GetUserRolesAsync()
        {
            try
            {
                return await _userRole.GetUserRolesAsync();
            }
            catch { throw; }
        }

        public async Task<UserRole> UpdateUserRoleAsync(UserRole userRole)
        {
            try
            {
                return await _userRole.UpdateUserRoleAsync(userRole);
            }
            catch { throw; }
        }


    }
}
