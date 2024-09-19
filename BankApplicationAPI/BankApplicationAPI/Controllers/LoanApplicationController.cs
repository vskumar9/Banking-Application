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
    public class LoanApplicationController : ControllerBase
    {
        private readonly LoanApplicationService _loanApplicationService;

        public LoanApplicationController(LoanApplicationService loanApplicationService)
        {
            _loanApplicationService = loanApplicationService;
        }

        // GET: api/LoanApplication
        [HttpGet]
        [Authorize(Roles = "admin, support, staff")] // Admins, support, and staff can view all loan applications
        public async Task<ActionResult<IEnumerable<LoanApplication>>> GetLoanApplications()
        {
            try
            {
                var loanApplications = await _loanApplicationService.GetLoanApplicationsAsync();
                return Ok(loanApplications);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving loan applications.");
            }
        }

        // GET: api/LoanApplication/5
        [HttpGet("{id}")]
        [Authorize(Roles = "admin, support, customer, staff")] // Admins, support, customers, and staff can view a specific loan application
        public async Task<ActionResult<LoanApplication>> GetLoanApplication(int id)
        {
            try
            {
                var loanApplications = await _loanApplicationService.GetLoanApplicationByLoanApplicationIdAsync(id);
                if (loanApplications == null)
                    return NotFound("Loan application not found.");

                return Ok(loanApplications);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving loan application.");
            }
        }

        // POST: api/LoanApplication
        [HttpPost]
        [Authorize(Roles = "customer")] // Only customers can create loan applications
        public async Task<ActionResult> CreateLoanApplication([FromForm] LoanDTO loanApplication)
        {
            if (loanApplication == null)
                return BadRequest("Invalid loan application data.");

            try{
                var customerId = User.FindFirstValue(ClaimTypes.PrimarySid);

                if (loanApplication.File! == null || loanApplication.File.Length == 0)
                    return BadRequest(new { message = "No file uploaded." });

                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                // Save file
                var fileName = Path.GetFileName(loanApplication.File.FileName);
                var filePath = Path.Combine(uploadPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await loanApplication.File.CopyToAsync(stream);
                }

                var Loan = new LoanApplication
                {
                    CustomerId = customerId,
                    LoanTypeId = loanApplication.LoanTypeId,
                    LoanAmount = loanApplication.LoanAmount,
                    ApplicationDate = loanApplication.ApplicationDate,
                    Comments = loanApplication.Comments,
                    EmployeeId = loanApplication.EmployeeId,
                    Files = $"/uploads/{fileName}" // Store relative path for easier access
                };

                var result = await _loanApplicationService.CreateLoanApplicationAsync(Loan);
                if (result)
                    return CreatedAtAction(nameof(GetLoanApplication), new { id = Loan.LoanId }, Loan);

                return BadRequest("Failed to create loan application.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating loan application.");
            }
        }

        // PUT: api/LoanApplication/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin, staff")] // Admins and staff can update loan applications
        public async Task<ActionResult> UpdateLoanApplication(int id, [FromBody] LoanApplication loanApplication)
        {
            if (loanApplication == null || loanApplication.LoanId != id)
                return BadRequest("Data mismatch.");

            try
            {
                var updatedLoanApplication = await _loanApplicationService.UpdateLoanApplicationAsync(loanApplication);
                if (updatedLoanApplication != null)
                    return Ok(updatedLoanApplication);

                return NotFound("Loan application not found.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating loan application.");
            }
        }

        // DELETE: api/LoanApplication/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin, staff")] // Admins and staff can delete loan applications
        public async Task<ActionResult> DeleteLoanApplication(int id)
        {
            try
            {
                var loanApplications = await _loanApplicationService.GetLoanApplicationByLoanApplicationIdAsync(id);
                if (loanApplications == null)
                    return NotFound("Loan application not found.");

                var result = await _loanApplicationService.DeleteLoanApplicationAsync(loanApplications);
                if (result)
                    return NoContent();

                return BadRequest("Failed to delete loan application.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting loan application.");
            }
        }

        // GET: api/Complaint/download/5
        [HttpGet("download/{id}")]
        [Authorize]
        public async Task<IActionResult> DownloadFile(int id)
        {
            var complaint = await _loanApplicationService.GetLoanApplicationByLoanApplicationIdAsync(id);
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
