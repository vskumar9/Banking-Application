using BankApplicationAPI.Models;
using BankApplicationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintResolutionController : ControllerBase
    {
        private readonly ComplaintResolutionService _complaintResolutionService;

        public ComplaintResolutionController(ComplaintResolutionService complaintResolutionService)
        {
            _complaintResolutionService = complaintResolutionService;
        }

        // GET: api/ComplaintResolution
        [HttpGet]
        [Authorize(Roles = "admin, support")] // Admins and support can view complaint resolutions
        public async Task<ActionResult<IEnumerable<ComplaintResolution>>> GetComplaintResolutions([FromQuery] int? resolutionId = null, [FromQuery] int? complaintId = null)
        {
            try
            {
                if (User.IsInRole("admin") || User.IsInRole("support"))
                {
                    var resolutions = await _complaintResolutionService.GetComplaintResolutionsAsync();
                    return Ok(resolutions);
                }
                else
                {
                    return Unauthorized("You are not authorized to view complaint resolutions.");
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }

        // GET: api/ComplaintResolution/5
        [HttpGet("{id}")]
        [Authorize(Roles = "admin, support")] // Admins and support can view specific complaint resolutions
        public async Task<ActionResult<ComplaintResolution>> GetComplaintResolution(int id)
        {
            try
            {
                var resolutions = await _complaintResolutionService.GetComplaintResolutionsByComplaintResolutionIdAsync(id);
                if (resolutions == null)
                    return NotFound("Complaint resolution not found.");

                return Ok(resolutions);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }

        // POST: api/ComplaintResolution
        [HttpPost]
        [Authorize(Roles = "support")] // Only support can create complaint resolutions
        public async Task<ActionResult> CreateComplaintResolution([FromBody] ComplaintResolution complaintResolution)
        {
            if (complaintResolution == null)
                return BadRequest("Invalid complaint resolution data.");

            try
            {
                var result = await _complaintResolutionService.CreateComplaintResolutionAsync(complaintResolution);
                if (result)
                    return CreatedAtAction(nameof(GetComplaintResolution), new { id = complaintResolution.ResolutionId }, complaintResolution);

                return BadRequest("Failed to create complaint resolution.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating complaint resolution.");
            }
        }

        // PUT: api/ComplaintResolution/5
        [HttpPut("{id}")]
        [Authorize(Roles = "support")] // Only support can update complaint resolutions
        public async Task<ActionResult> UpdateComplaintResolution(int id, [FromBody] ComplaintResolution complaintResolution)
        {
            if (complaintResolution == null || complaintResolution.ResolutionId != id)
                return BadRequest("Data mismatch.");

            try
            {
                var updatedResolution = await _complaintResolutionService.UpdateComplaintResolutionAsync(complaintResolution);
                if (updatedResolution != null)
                    return Ok(updatedResolution);

                return NotFound("Complaint resolution not found.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating complaint resolution.");
            }
        }

        // DELETE: api/ComplaintResolution/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "support")] // Only support can delete complaint resolutions
        public async Task<ActionResult> DeleteComplaintResolution(int id)
        {
            try
            {
                var resolution = await _complaintResolutionService.GetComplaintResolutionsByComplaintResolutionIdAsync(resolutionId: id);
                if (resolution == null)
                    return NotFound("Complaint resolution not found.");

                var result = await _complaintResolutionService.DeleteComplaintResolutionAsync(resolution);
                if (result)
                    return NoContent();

                return BadRequest("Failed to delete complaint resolution.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting complaint resolution.");
            }
        }
    }
}
