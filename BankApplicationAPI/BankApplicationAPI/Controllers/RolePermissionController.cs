using BankApplicationAPI.Models;
using BankApplicationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolePermissionController : ControllerBase
    {
        private readonly RolePermissionService _rolePermissionService;

        public RolePermissionController(RolePermissionService rolePermissionService)
        {
            _rolePermissionService = rolePermissionService;
        }

        // GET: api/RolePermission
        [HttpGet]
        [Authorize(Roles = "admin, support")] // Admins and support can view all role-permissions
        public async Task<ActionResult<IEnumerable<RolePermission>>> GetRolePermissions()
        {
            try
            {
                var rolePermissions = await _rolePermissionService.GetRolePermissionsAsync();
                return Ok(rolePermissions);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving role permissions.");
            }
        }

        // GET: api/RolePermission/5
        [HttpGet("{id}")]
        [Authorize(Roles = "admin, support, staff")] // Admins, support, and staff can view a specific role-permission
        public async Task<ActionResult<RolePermission>> GetRolePermission(int id)
        {
            try
            {
                var rolePermissions = await _rolePermissionService.GetRolePermissionByRolePermissionIdAsync(id);
                if (rolePermissions == null || !rolePermissions.Any())
                    return NotFound("Role permission not found.");

                return Ok(rolePermissions.First());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving role permission.");
            }
        }

        // POST: api/RolePermission
        [HttpPost]
        [Authorize(Roles = "admin")] // Only admins can create role-permissions
        public async Task<ActionResult> CreateRolePermission([FromBody] RolePermission rolePermission)
        {
            if (rolePermission == null)
                return BadRequest("Invalid role permission data.");

            try
            {
                var result = await _rolePermissionService.CreateRolePermissionAsync(rolePermission);
                if (result)
                    return CreatedAtAction(nameof(GetRolePermission), new { id = rolePermission.RolePermissionId }, rolePermission);

                return BadRequest("Failed to create role permission.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating role permission.");
            }
        }

        // PUT: api/RolePermission/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")] // Only admins can update role-permissions
        public async Task<ActionResult> UpdateRolePermission(int id, [FromBody] RolePermission rolePermission)
        {
            if (rolePermission == null || rolePermission.RolePermissionId != id)
                return BadRequest("Data mismatch.");

            try
            {
                var updatedRolePermission = await _rolePermissionService.UpdateRolePermissionAsync(rolePermission);
                if (updatedRolePermission != null)
                    return Ok(updatedRolePermission);

                return NotFound("Role permission not found.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating role permission.");
            }
        }

        // DELETE: api/RolePermission/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")] // Only admins can delete role-permissions
        public async Task<ActionResult> DeleteRolePermission(int id)
        {
            try
            {
                var rolePermissions = await _rolePermissionService.GetRolePermissionByRolePermissionIdAsync(id);
                if (rolePermissions == null || !rolePermissions.Any())
                    return NotFound("Role permission not found.");

                var result = await _rolePermissionService.DeleteRolePermissionAsync(rolePermissions.First());
                if (result)
                    return NoContent();

                return BadRequest("Failed to delete role permission.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting role permission.");
            }
        }
    }
}
