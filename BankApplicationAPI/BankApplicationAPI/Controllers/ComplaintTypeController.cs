using BankApplicationAPI.Models;
using BankApplicationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintTypeController : ControllerBase
    {
        private readonly ComplaintTypeService _complaintTypeService;

        public ComplaintTypeController(ComplaintTypeService complaintTypeService)
        {
            _complaintTypeService = complaintTypeService;
        }

        // GET: api/ComplaintType
        [HttpGet]
        [Authorize(Roles = "admin, support")] // Admins and support can view complaint types
        public async Task<ActionResult<IEnumerable<ComplaintType>>> GetComplaintTypes()
        {
            try
            {
                var complaintTypes = await _complaintTypeService.GetComplaintTypesAsync();
                return Ok(complaintTypes);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }

        // GET: api/ComplaintType/5
        [HttpGet("{id}")]
        [Authorize(Roles = "admin, support")] // Admins and support can view specific complaint type
        public async Task<ActionResult<ComplaintType>> GetComplaintType(int id)
        {
            try
            {
                var complaintTypes = await _complaintTypeService.GetComplaintTypeByComplaintTypeIdAsync(id);
                if (complaintTypes == null)
                    return NotFound("Complaint type not found.");

                return Ok(complaintTypes);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }

        // POST: api/ComplaintType
        [HttpPost]
        [Authorize(Roles = "admin")] // Only admins can create complaint types
        public async Task<ActionResult> CreateComplaintType([FromBody] ComplaintType complaintType)
        {
            if (complaintType == null)
                return BadRequest("Invalid complaint type data.");

            try
            {
                var result = await _complaintTypeService.CreateComplaintTypeAsync(complaintType);
                if (result)
                    return CreatedAtAction(nameof(GetComplaintType), new { id = complaintType.ComplaintTypeId }, complaintType);

                return BadRequest("Failed to create complaint type.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating complaint type.");
            }
        }

        // PUT: api/ComplaintType/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")] // Only admins can update complaint types
        public async Task<ActionResult> UpdateComplaintType(int id, [FromBody] ComplaintType complaintType)
        {
            if (complaintType == null || complaintType.ComplaintTypeId != id)
                return BadRequest("Data mismatch.");

            try
            {
                var updatedComplaintType = await _complaintTypeService.UpdateComplaintTypeAsync(complaintType);
                if (updatedComplaintType != null)
                    return Ok(updatedComplaintType);

                return NotFound("Complaint type not found.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating complaint type.");
            }
        }

        // DELETE: api/ComplaintType/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")] // Only admins can delete complaint types
        public async Task<ActionResult> DeleteComplaintType(int id)
        {
            try
            {
                var complaintType = await _complaintTypeService.GetComplaintTypeByComplaintTypeIdAsync(complaintTypeId: id);
                if (complaintType == null)
                    return NotFound("Complaint type not found.");

                var result = await _complaintTypeService.DeleteComplaintTypeAsync(complaintType);
                if (result)
                    return NoContent();

                return BadRequest("Failed to delete complaint type.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting complaint type.");
            }
        }
    }
}
