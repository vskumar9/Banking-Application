using BankApplicationAPI.Models;
using BankApplicationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintStatusHistoryController : ControllerBase
    {
        private readonly ComplaintStatusHistoryService _complaintStatusHistoryService;

        public ComplaintStatusHistoryController(ComplaintStatusHistoryService complaintStatusHistoryService)
        {
            _complaintStatusHistoryService = complaintStatusHistoryService;
        }

        // GET: api/ComplaintStatusHistory
        [HttpGet]
        [Authorize(Roles = "admin, support")] // Admins and support can view status histories
        public async Task<ActionResult<IEnumerable<ComplaintStatusHistory>>> GetComplaintStatusHistories([FromQuery] int? statusHistoryId = null, [FromQuery] int? complaintId = null)
        {
            try
            {
                if (User.IsInRole("admin") || User.IsInRole("support"))
                {
                    var histories = await _complaintStatusHistoryService.GetComplaintStatusHistorysAsync();
                    return Ok(histories);
                }
                else
                {
                    return Unauthorized("You are not authorized to view complaint status histories.");
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }

        // GET: api/ComplaintStatusHistory/5
        [HttpGet("{id}")]
        [Authorize(Roles = "admin, support")] // Admins and support can view specific status history
        public async Task<ActionResult<ComplaintStatusHistory>> GetComplaintStatusHistory(int id)
        {
            try
            {
                var histories = await _complaintStatusHistoryService.GetComplaintStatusHistoryByComplaintStatusHistoryIdAsync(id);
                if (histories == null || !histories.Any())
                    return NotFound("Complaint status history not found.");

                return Ok(histories.First());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }

        // POST: api/ComplaintStatusHistory
        [HttpPost]
        [Authorize(Roles = "support")] // Only support can create status histories
        public async Task<ActionResult> CreateComplaintStatusHistory([FromBody] ComplaintStatusHistory complaintStatusHistory)
        {
            if (complaintStatusHistory == null)
                return BadRequest("Invalid complaint status history data.");

            try
            {
                var result = await _complaintStatusHistoryService.CreateComplaintStatusHistoryAsync(complaintStatusHistory);
                if (result)
                    return CreatedAtAction(nameof(GetComplaintStatusHistory), new { id = complaintStatusHistory.StatusHistoryId }, complaintStatusHistory);

                return BadRequest("Failed to create complaint status history.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating complaint status history.");
            }
        }

        // PUT: api/ComplaintStatusHistory/5
        [HttpPut("{id}")]
        [Authorize(Roles = "support")] // Only support can update status histories
        public async Task<ActionResult> UpdateComplaintStatusHistory(int id, [FromBody] ComplaintStatusHistory complaintStatusHistory)
        {
            if (complaintStatusHistory == null || complaintStatusHistory.StatusHistoryId != id)
                return BadRequest("Data mismatch.");

            try
            {
                var updatedHistory = await _complaintStatusHistoryService.UpdateComplaintStatusHistoryAsync(complaintStatusHistory);
                if (updatedHistory != null)
                    return Ok(updatedHistory);

                return NotFound("Complaint status history not found.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating complaint status history.");
            }
        }

        // DELETE: api/ComplaintStatusHistory/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "support")] // Only support can delete status histories
        public async Task<ActionResult> DeleteComplaintStatusHistory(int id)
        {
            try
            {
                var history = await _complaintStatusHistoryService.GetComplaintStatusHistoryAsync(statusHistoryId: id);
                if (history == null)
                    return NotFound("Complaint status history not found.");

                var result = await _complaintStatusHistoryService.DeleteComplaintStatusHistoryAsync(history);
                if (result)
                    return NoContent();

                return BadRequest("Failed to delete complaint status history.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting complaint status history.");
            }
        }
    }
}
