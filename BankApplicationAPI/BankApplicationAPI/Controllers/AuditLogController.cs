using BankApplicationAPI.Models;
using BankApplicationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditLogController : ControllerBase
    {
        private readonly AuditLogService _auditLogService;

        public AuditLogController(AuditLogService auditLogService)
        {
            _auditLogService = auditLogService;
        }

        // GET: api/AuditLog
        [HttpGet]
        [Authorize(Roles = "admin, support")] // Only admins and support can view the logs
        public async Task<ActionResult<IEnumerable<AuditLog>>> GetAuditLogs()
        {
            try
            {
                var result = await _auditLogService.GetAuditLogsAsync();
                if (result == null || !result.Any())
                    return NotFound("No audit logs found.");

                return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }

        // GET: api/AuditLog/5
        [HttpGet("{id}")]
        [Authorize(Roles = "admin, support")] // Only admins and support can view a specific log
        public async Task<ActionResult<AuditLog>> GetAuditLog(int id)
        {
            try
            {
                var result = await _auditLogService.GetAuditLogByAuditLogIdAsync(id);
                if (result == null || !result.Any())
                    return NotFound("Audit log not found.");

                return Ok(result.First());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }

        // POST: api/AuditLog
        [HttpPost]
        [Authorize(Roles = "none")] // Role that no one has
        public async Task<ActionResult> CreateAuditLog([FromBody] AuditLog auditLog)
        {
            if (auditLog == null)
                return BadRequest("Invalid data.");

            try
            {
                var created = await _auditLogService.CreateAuditLogAsync(auditLog);
                if (created)
                    return CreatedAtAction(nameof(GetAuditLog), new { id = auditLog.AuditLogId }, auditLog);

                return BadRequest("Failed to create audit log.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating data.");
            }
        }

        // PUT: api/AuditLog/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")] // Only admins can update an audit log
        public async Task<ActionResult> UpdateAuditLog(int id, [FromBody] AuditLog auditLog)
        {
            if (auditLog == null || auditLog.AuditLogId != id)
                return BadRequest("Data mismatch.");

            try
            {
                var updated = await _auditLogService.UpdateAuditLogAsync(auditLog);
                if (updated != null)
                    return Ok(updated);

                return NotFound("Audit log not found.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data.");
            }
        }

        // DELETE: api/AuditLog/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")] // Only admins can delete an audit log
        public async Task<ActionResult> DeleteAuditLog(int id)
        {
            try
            {
                var auditLog = await _auditLogService.GetAuditLogByAuditLogIdAsync(id);
                if (auditLog == null || !auditLog.Any())
                    return NotFound("Audit log not found.");

                var deleted = await _auditLogService.DeleteAuditLogAsync(auditLog.First());
                if (deleted)
                    return NoContent();

                return BadRequest("Failed to delete audit log.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data.");
            }
        }
    }
}
