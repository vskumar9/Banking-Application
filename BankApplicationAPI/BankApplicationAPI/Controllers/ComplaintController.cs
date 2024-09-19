using BankApplicationAPI.DTO;
using BankApplicationAPI.Models;
using BankApplicationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [Authorize(Roles = "admin, support")]
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
        [Authorize(Roles = "admin, support, customer")]
        public async Task<ActionResult<Complaint>> GetComplaint(int id)
        {
            try
            {
                var result = await _complaintService.GetComplaintsByComplaintIdAsync(id);
                if (result == null)
                    return NotFound("Complaint not found.");

                return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }

        // POST: api/Complaint
        [HttpPost]
        [Authorize(Roles = "admin, support, customer")]
        public async Task<ActionResult> CreateComplaint([FromForm] ComplaintDTO complaintDto)
        {
            if (complaintDto == null)
                return BadRequest("Invalid data.");

            try
            {
                var customerId = User.FindFirstValue(ClaimTypes.PrimarySid);
                if (complaintDto.File == null || complaintDto.File.Length == 0)
                    return BadRequest(new { message = "No file uploaded." });

                // Create upload directory if it doesn't exist
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                // Save file
                var fileName = Path.GetFileName(complaintDto.File.FileName);
                var filePath = Path.Combine(uploadPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await complaintDto.File.CopyToAsync(stream);
                }

                var complaint = new Complaint
                {
                    ComplaintTypeId = complaintDto.ComplaintTypeId,
                    ComplaintDate = complaintDto.ComplaintDate,
                    ComplaintDescription = complaintDto.ComplaintDescription,
                    ComplaintStatus = complaintDto.ComplaintStatus,
                    EmployeeId = complaintDto.EmployeeId,
                    Files = $"/uploads/{fileName}" // Store relative path for easier access
                };

                if (User.IsInRole("customer"))
                    complaint.CustomerId = customerId;
                else if (User.IsInRole("admin, support"))
                    complaint.EmployeeId = customerId;

                var created = await _complaintService.CreateComplaintAsync(complaint);
                return CreatedAtAction(nameof(GetComplaint), new { id = complaint.ComplaintId }, complaint);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating data.");
            }
        }

        // PUT: api/Complaint/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin, support, staff")]
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
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteComplaint(int id)
        {
            try
            {
                var complaint = await _complaintService.GetComplaintsByComplaintIdAsync(id);
                if (complaint == null)
                    return NotFound("Complaint not found.");

                var deleted = await _complaintService.DeleteComplaintAsync(complaint);
                if (deleted)
                    return NoContent();

                return BadRequest("Failed to delete complaint.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data.");
            }
        }

        // GET: api/Complaint/download/5
        [HttpGet("download/{id}")]
        [Authorize]
        public async Task<IActionResult> DownloadFile(int id)
        {
            var complaint = await _complaintService.GetComplaintsByComplaintIdAsync(id);
            if (complaint == null || string.IsNullOrEmpty(complaint.Files))
                return NotFound("File not found.");

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", complaint.Files.TrimStart('/'));
            if (!System.IO.File.Exists(filePath))
                return NotFound("File not found.");

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            //var mimeType = "application/octet-stream"; // Default MIME type, change according to file type if needed
            return new FileStreamResult(fileStream, GetContentType(filePath))
            {
                FileDownloadName = Path.GetFileName(filePath)
            };
        }

        private string GetContentType(string filePath)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(filePath).ToLowerInvariant();
            return types.ContainsKey(ext) ? types[ext] : "application/octet-stream";
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}
