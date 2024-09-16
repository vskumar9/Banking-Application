using BankApplicationAPI.Models;
using BankApplicationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanRepaymentLogController : ControllerBase
    {
        private readonly LoanRepaymentLogService _loanRepaymentLogService;

        public LoanRepaymentLogController(LoanRepaymentLogService loanRepaymentLogService)
        {
            _loanRepaymentLogService = loanRepaymentLogService;
        }

        // GET: api/LoanRepaymentLog
        [HttpGet]
        [Authorize(Roles = "admin, support, staff")] // Admins, support, and staff can view all loan repayment logs
        public async Task<ActionResult<IEnumerable<LoanRepaymentLog>>> GetLoanRepaymentLogs()
        {
            try
            {
                var loanRepaymentLogs = await _loanRepaymentLogService.GetLoanRepaymentLogsAsync();
                return Ok(loanRepaymentLogs);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving loan repayment logs.");
            }
        }

        // GET: api/LoanRepaymentLog/5
        [HttpGet("{id}")]
        [Authorize(Roles = "admin, support, cashier, staff")] // Admins, support, cashiers, and staff can view a specific loan repayment log
        public async Task<ActionResult<LoanRepaymentLog>> GetLoanRepaymentLog(int id)
        {
            try
            {
                var loanRepaymentLogs = await _loanRepaymentLogService.GetLoanRepaymentLogByLoanRepaymentLogIdAsync(id);
                if (loanRepaymentLogs == null)
                    return NotFound("Loan repayment log not found.");

                return Ok(loanRepaymentLogs);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving loan repayment log.");
            }
        }

        // POST: api/LoanRepaymentLog
        [HttpPost]
        [Authorize(Roles = "cashier")] // Only cashiers can create loan repayment logs
        public async Task<ActionResult> CreateLoanRepaymentLog([FromBody] LoanRepaymentLog loanRepaymentLog)
        {
            if (loanRepaymentLog == null)
                return BadRequest("Invalid loan repayment log data.");

            try
            {
                var result = await _loanRepaymentLogService.CreateLoanRepaymentLogAsync(loanRepaymentLog);
                if (result)
                    return CreatedAtAction(nameof(GetLoanRepaymentLog), new { id = loanRepaymentLog.RepaymentId }, loanRepaymentLog);

                return BadRequest("Failed to create loan repayment log.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating loan repayment log.");
            }
        }

        // PUT: api/LoanRepaymentLog/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin, staff")] // Admins and staff can update loan repayment logs
        public async Task<ActionResult> UpdateLoanRepaymentLog(int id, [FromBody] LoanRepaymentLog loanRepaymentLog)
        {
            if (loanRepaymentLog == null || loanRepaymentLog.RepaymentId != id)
                return BadRequest("Data mismatch.");

            try
            {
                var updatedLoanRepaymentLog = await _loanRepaymentLogService.UpdateLoanRepaymentLogAsync(loanRepaymentLog);
                if (updatedLoanRepaymentLog != null)
                    return Ok(updatedLoanRepaymentLog);

                return NotFound("Loan repayment log not found.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating loan repayment log.");
            }
        }

        // DELETE: api/LoanRepaymentLog/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin, staff")] // Admins and staff can delete loan repayment logs
        public async Task<ActionResult> DeleteLoanRepaymentLog(int id)
        {
            try
            {
                var loanRepaymentLogs = await _loanRepaymentLogService.GetLoanRepaymentLogByLoanRepaymentLogIdAsync(id);
                if (loanRepaymentLogs == null)
                    return NotFound("Loan repayment log not found.");

                var result = await _loanRepaymentLogService.DeleteLoanRepaymentLogAsync(loanRepaymentLogs);
                if (result)
                    return NoContent();

                return BadRequest("Failed to delete loan repayment log.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting loan repayment log.");
            }
        }
    }
}
