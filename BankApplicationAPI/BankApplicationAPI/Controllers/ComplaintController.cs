using BankApplicationAPI.Models;
using BankApplicationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintController : ControllerBase
    {
        private readonly ComplaintService _complaintService;

        public ComplaintController(ComplaintService complaintService)
        {
            _complaintService = complaintService;
        }

        // GET: api/Complaint
        [HttpGet]
        [Authorize(Roles = "admin, support")] // Only admins and support can view the complaints
        public async Task<ActionResult<IEnumerable<Complaint>>> GetComplaints()
        {
            try
            {
                var result = await _complaintService.GetComplaintsAsync();
                if (result == null || !result.Any())
                    return NotFound("No complaints found.");

                return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }

        // GET: api/Complaint/5
        [HttpGet("{id}")]
        [Authorize(Roles = "admin, support")] // Only admins and support can view a specific complaint
        public async Task<ActionResult<Complaint>> GetComplaint(int id)
        {
            try
            {
                var result = await _complaintService.GetComplaintsByComplaintIdAsync(id);
                if (result == null || !result.Any())
                    return NotFound("Complaint not found.");

                return Ok(result.First());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }

        // POST: api/Complaint
        [HttpPost]
        [Authorize(Roles = "admin, support")] // Both admins and support can create complaints
        public async Task<ActionResult> CreateComplaint([FromBody] Complaint complaint)
        {
            if (complaint == null)
                return BadRequest("Invalid data.");

            try
            {
                var created = await _complaintService.CreateComplaintAsync(complaint);
                if (created)
                    return CreatedAtAction(nameof(GetComplaint), new { id = complaint.ComplaintId }, complaint);

                return BadRequest("Failed to create complaint.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating data.");
            }
        }

        // PUT: api/Complaint/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin, support")] // Both admins and support can update complaints
        public async Task<ActionResult> UpdateComplaint(int id, [FromBody] Complaint complaint)
        {
            if (complaint == null || complaint.ComplaintId != id)
                return BadRequest("Data mismatch.");

            try
            {
                var updated = await _complaintService.UpdateComplaintAsync(complaint);
                if (updated != null)
                    return Ok(updated);

                return NotFound("Complaint not found.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data.");
            }
        }

        // DELETE: api/Complaint/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")] // Only admins can delete complaints
        public async Task<ActionResult> DeleteComplaint(int id)
        {
            try
            {
                var complaint = await _complaintService.GetComplaintsByComplaintIdAsync(id);
                if (complaint == null || !complaint.Any())
                    return NotFound("Complaint not found.");

                var deleted = await _complaintService.DeleteComplaintAsync(complaint.First());
                if (deleted)
                    return NoContent();

                return BadRequest("Failed to delete complaint.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data.");
            }
        }
    }
}
