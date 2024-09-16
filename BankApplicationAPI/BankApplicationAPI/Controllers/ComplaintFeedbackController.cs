using BankApplicationAPI.Models;
using BankApplicationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintFeedbackController : ControllerBase
    {
        private readonly ComplaintFeedbackService _complaintFeedbackService;

        public ComplaintFeedbackController(ComplaintFeedbackService complaintFeedbackService)
        {
            _complaintFeedbackService = complaintFeedbackService;
        }

        // GET: api/ComplaintFeedback
        [HttpGet]
        [Authorize(Roles = "admin, support, customer")] // Admins, support, and customers can view feedbacks
        public async Task<ActionResult<IEnumerable<ComplaintFeedback>>> GetComplaintFeedbacks()
        {
            try
            {
                var result = await _complaintFeedbackService.GetComplaintFeedbacksAsync();
                if (result == null || !result.Any())
                    return NotFound("No feedbacks found.");

                return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }

        // GET: api/ComplaintFeedback/5
        [HttpGet("{id}")]
        [Authorize(Roles = "admin, support, customer")] // Admins, support, and customers can view specific feedback
        public async Task<ActionResult<ComplaintFeedback>> GetComplaintFeedback(int id)
        {
            try
            {
                var result = await _complaintFeedbackService.GetCComplaintFeedbacksByComplaintFeedbackIdAsync(id);
                if (result == null)
                    return NotFound("Feedback not found.");

                return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }

        // POST: api/ComplaintFeedback
        [HttpPost]
        [Authorize(Roles = "customer")] // Only customers can create feedback
        public async Task<ActionResult> CreateComplaintFeedback([FromBody] ComplaintFeedback feedback)
        {
            if (feedback == null)
                return BadRequest("Invalid data.");

            try
            {
                var created = await _complaintFeedbackService.CreateComplaintFeedbackAsync(feedback);
                if (created)
                    return CreatedAtAction(nameof(GetComplaintFeedback), new { id = feedback.FeedbackId }, feedback);

                return BadRequest("Failed to create feedback.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating data.");
            }
        }

        // PUT: api/ComplaintFeedback/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin, support")] // Only admins and support can update feedback
        public async Task<ActionResult> UpdateComplaintFeedback(int id, [FromBody] ComplaintFeedback feedback)
        {
            if (feedback == null || feedback.FeedbackId != id)
                return BadRequest("Data mismatch.");

            try
            {
                var updated = await _complaintFeedbackService.UpdateComplaintFeedbackAsync(feedback);
                if (updated != null)
                    return Ok(updated);

                return NotFound("Feedback not found.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data.");
            }
        }

        // DELETE: api/ComplaintFeedback/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")] // Only admins and support can delete feedback
        public async Task<ActionResult> DeleteComplaintFeedback(int id)
        {
            try
            {
                var feedbacks = await _complaintFeedbackService.GetCComplaintFeedbacksByComplaintFeedbackIdAsync(id);
                if (feedbacks == null)
                    return NotFound("Feedback not found.");

                var deleted = await _complaintFeedbackService.DeleteComplaintFeedbackAsync(feedbacks);
                if (deleted)
                    return NoContent();

                return BadRequest("Failed to delete feedback.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data.");
            }
        }
    }
}
