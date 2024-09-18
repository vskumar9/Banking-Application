using BankApplicationAPI.Models;
using BankApplicationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult> CreateLoanApplication([FromBody] LoanApplication loanApplication)
        {
            if (loanApplication == null)
                return BadRequest("Invalid loan application data.");

            try{

                var result = await _loanApplicationService.CreateLoanApplicationAsync(loanApplication);
                if (result)
                    return CreatedAtAction(nameof(GetLoanApplication), new { id = loanApplication.LoanId }, loanApplication);

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
    }
}
