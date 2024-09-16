using BankApplicationAPI.Models;
using BankApplicationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleService _roleService;

        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }

        // GET: api/Role
        [HttpGet]
        [Authorize(Roles = "admin, support, staff")] // Admins, support, and staff can view all roles
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            try
            {
                var roles = await _roleService.GetRolesAsync();
                return Ok(roles);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving roles.");
            }
        }

        // GET: api/Role/5
        [HttpGet("{id}")]
        [Authorize(Roles = "admin, support, staff")] // Admins, support, and staff can view a specific role
        public async Task<ActionResult<Role>> GetRole(int id)
        {
            try
            {
                var roles = await _roleService.GetRoleByRoleIdAsync(id);
                if (roles == null)
                    return NotFound("Role not found.");

                return Ok(roles);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving role.");
            }
        }

        // POST: api/Role
        [HttpPost]
        [Authorize(Roles = "admin")] // Only admins can create roles
        public async Task<ActionResult> CreateRole([FromBody] Role role)
        {
            if (role == null)
                return BadRequest("Invalid role data.");

            try
            {
                var result = await _roleService.CreateRoleAsync(role);
                if (result)
                    return CreatedAtAction(nameof(GetRole), new { id = role.RoleId }, role);

                return BadRequest("Failed to create role.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating role.");
            }
        }

        // PUT: api/Role/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")] // Only admins can update roles
        public async Task<ActionResult> UpdateRole(int id, [FromBody] Role role)
        {
            if (role == null || role.RoleId != id)
                return BadRequest("Data mismatch.");

            try
            {
                var updatedRole = await _roleService.UpdateRoleAsync(role);
                if (updatedRole != null)
                    return Ok(updatedRole);

                return NotFound("Role not found.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating role.");
            }
        }

        // DELETE: api/Role/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")] // Only admins can delete roles
        public async Task<ActionResult> DeleteRole(int id)
        {
            try
            {
                var roles = await _roleService.GetRoleByRoleIdAsync(id);
                if (roles == null)
                    return NotFound("Role not found.");

                var result = await _roleService.DeleteRoleAsync(roles);
                if (result)
                    return NoContent();

                return BadRequest("Failed to delete role.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting role.");
            }
        }
    }
}
