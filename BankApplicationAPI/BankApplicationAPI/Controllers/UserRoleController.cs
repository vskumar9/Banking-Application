using BankApplicationAPI.Models;
using BankApplicationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly UserRoleService _userRoleService;

        public UserRoleController(UserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        // GET: api/UserRole
        [HttpGet]
        [Authorize(Roles = "admin")] // Only admins can view all user roles
        public async Task<ActionResult<IEnumerable<UserRole>>> GetUserRoles()
        {
            try
            {
                var userRoles = await _userRoleService.GetUserRolesAsync();
                return Ok(userRoles);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving user roles.");
            }
        }

        // GET: api/UserRole/5
        [HttpGet("{id}")]
        [Authorize(Roles = "admin, support")] // Admins and support can view a specific user role
        public async Task<ActionResult<UserRole>> GetUserRole(int id)
        {
            try
            {
                var userRoles = await _userRoleService.GetUserRoleByUserRoleIdAsync(id);
                if (userRoles == null || !userRoles.Any())
                    return NotFound("User role not found.");

                return Ok(userRoles.First());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving user role.");
            }
        }

        // POST: api/UserRole
        [HttpPost]
        [Authorize(Roles = "admin")] // Only admins can create user roles
        public async Task<ActionResult> CreateUserRole([FromBody] UserRole userRole)
        {
            if (userRole == null)
                return BadRequest("Invalid user role data.");

            try
            {
                var result = await _userRoleService.CreateUserRoleAsync(userRole);
                if (result)
                    return CreatedAtAction(nameof(GetUserRole), new { id = userRole.UserRoleId }, userRole);

                return BadRequest("Failed to create user role.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating user role.");
            }
        }

        // PUT: api/UserRole/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")] // Only admins can update user roles
        public async Task<ActionResult> UpdateUserRole(int id, [FromBody] UserRole userRole)
        {
            if (userRole == null || userRole.UserRoleId != id)
                return BadRequest("Data mismatch.");

            try
            {
                var updatedUserRole = await _userRoleService.UpdateUserRoleAsync(userRole);
                if (updatedUserRole != null)
                    return Ok(updatedUserRole);

                return NotFound("User role not found.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating user role.");
            }
        }

        // DELETE: api/UserRole/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")] // Only admins can delete user roles
        public async Task<ActionResult> DeleteUserRole(int id)
        {
            try
            {
                var userRoles = await _userRoleService.GetUserRoleByUserRoleIdAsync(id);
                if (userRoles == null || !userRoles.Any())
                    return NotFound("User role not found.");

                var result = await _userRoleService.DeleteUserRoleAsync(userRoles.First());
                if (result)
                    return NoContent();

                return BadRequest("Failed to delete user role.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting user role.");
            }
        }
    }
}
