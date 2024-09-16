using BankApplicationAPI.Models;
using BankApplicationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanPaymentScheduleController : ControllerBase
    {
        private readonly LoanPaymentScheduleService _loanPaymentScheduleService;

        public LoanPaymentScheduleController(LoanPaymentScheduleService loanPaymentScheduleService)
        {
            _loanPaymentScheduleService = loanPaymentScheduleService;
        }

        // Create Loan Payment Schedule
        [HttpPost]
        [Authorize(Roles = "admin, staff")] // Only admin and staff can create
        public async Task<IActionResult> CreateLoanPaymentSchedule([FromBody] LoanPaymentSchedule loanPaymentSchedule)
        {
            if (loanPaymentSchedule == null) return BadRequest("Invalid data.");

            var result = await _loanPaymentScheduleService.CreateLoanPaymentScheduleAsync(loanPaymentSchedule);

            if (result)
                return CreatedAtAction(nameof(GetLoanPaymentSchedule), new { paymentId = loanPaymentSchedule.PaymentId }, loanPaymentSchedule);
            else
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating the Loan Payment Schedule.");
        }

        // Delete Loan Payment Schedule
        [HttpDelete("{paymentId}")]
        [Authorize(Roles = "admin, staff")] // Only admin and staff can delete
        public async Task<IActionResult> DeleteLoanPaymentSchedule(int paymentId)
        {
            var schedule = await _loanPaymentScheduleService.GetLoanPaymentScheduleByLoanPaymentScheduleIdAsync(PaymentId: paymentId);

            if (schedule == null) return NotFound("Loan Payment Schedule not found.");

            var result = await _loanPaymentScheduleService.DeleteLoanPaymentScheduleAsync(schedule);

            if (result)
                return NoContent();
            else
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting the Loan Payment Schedule.");
        }

        // Get Loan Payment Schedule by ID
        [HttpGet("{paymentId}")]
        [Authorize(Roles = "admin, staff, support")] // Admin, staff, and support can view
        public async Task<IActionResult> GetLoanPaymentSchedule(int paymentId)
        {
            var schedule = await _loanPaymentScheduleService.GetLoanPaymentScheduleByLoanPaymentScheduleIdAsync(paymentId);

            if (schedule == null) return NotFound("Loan Payment Schedule not found.");

            return Ok(schedule);
        }

        // Get all Loan Payment Schedules
        [HttpGet]
        [Authorize(Roles = "admin, staff, support")] // Admin, staff, and support can view
        public async Task<IActionResult> GetLoanPaymentSchedules()
        {
            var schedules = await _loanPaymentScheduleService.GetLoanPaymentSchedulesAsync();

            return Ok(schedules);
        }

        // Update Loan Payment Schedule
        [HttpPut]
        [Authorize(Roles = "admin, staff")] // Only admin and staff can update
        public async Task<IActionResult> UpdateLoanPaymentSchedule([FromBody] LoanPaymentSchedule loanPaymentSchedule)
        {
            if (loanPaymentSchedule == null) return BadRequest("Invalid data.");

            var updatedSchedule = await _loanPaymentScheduleService.UpdateLoanPaymentScheduleAsync(loanPaymentSchedule);

            return Ok(updatedSchedule);
        }
    }
}
