using BankApplicationAPI.Models;
using BankApplicationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly PermissionService _permissionService;

        public PermissionController(PermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        // GET: api/Permission
        [HttpGet]
        [Authorize(Roles = "admin, support, staff")] // Admins, support, and staff can view all permissions
        public async Task<ActionResult<IEnumerable<Permission>>> GetPermissions()
        {
            try
            {
                var permissions = await _permissionService.GetPermissionsAsync();
                return Ok(permissions);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving permissions.");
            }
        }

        // GET: api/Permission/5
        [HttpGet("{id}")]
        [Authorize(Roles = "admin, support, staff")] // Admins, support, and staff can view a specific permission
        public async Task<ActionResult<Permission>> GetPermission(int id)
        {
            try
            {
                var permissions = await _permissionService.GetPermissionByPermissionIdAsync(id);
                if (permissions == null || !permissions.Any())
                    return NotFound("Permission not found.");

                return Ok(permissions.First());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving permission.");
            }
        }

        // POST: api/Permission
        [HttpPost]
        [Authorize(Roles = "admin")] // Only admins can create permissions
        public async Task<ActionResult> CreatePermission([FromBody] Permission permission)
        {
            if (permission == null)
                return BadRequest("Invalid permission data.");

            try
            {
                var result = await _permissionService.CreatePermissionAsync(permission);
                if (result)
                    return CreatedAtAction(nameof(GetPermission), new { id = permission.PermissionId }, permission);

                return BadRequest("Failed to create permission.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating permission.");
            }
        }

        // PUT: api/Permission/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")] // Only admins can update permissions
        public async Task<ActionResult> UpdatePermission(int id, [FromBody] Permission permission)
        {
            if (permission == null || permission.PermissionId != id)
                return BadRequest("Data mismatch.");

            try
            {
                var updatedPermission = await _permissionService.UpdatePermissionAsync(permission);
                if (updatedPermission != null)
                    return Ok(updatedPermission);

                return NotFound("Permission not found.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating permission.");
            }
        }

        // DELETE: api/Permission/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")] // Only admins can delete permissions
        public async Task<ActionResult> DeletePermission(int id)
        {
            try
            {
                var permissions = await _permissionService.GetPermissionByPermissionIdAsync(id);
                if (permissions == null || !permissions.Any())
                    return NotFound("Permission not found.");

                var result = await _permissionService.DeletePermissionAsync(permissions.First());
                if (result)
                    return NoContent();

                return BadRequest("Failed to delete permission.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting permission.");
            }
        }
    }
}
