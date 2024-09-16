using BankApplicationAPI.Models;
using BankApplicationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavingsInterestRateController : ControllerBase
    {
        private readonly SavingsInterestRateService _savingsInterestRateService;

        public SavingsInterestRateController(SavingsInterestRateService savingsInterestRateService)
        {
            _savingsInterestRateService = savingsInterestRateService;
        }

        // GET: api/SavingsInterestRate
        [HttpGet]
        [Authorize(Roles = "admin, support")] // Admins and support can view all savings interest rates
        public async Task<ActionResult<IEnumerable<SavingsInterestRate>>> GetSavingsInterestRates()
        {
            try
            {
                var interestRates = await _savingsInterestRateService.GetSavingsInterestRatesAsync();
                return Ok(interestRates);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving savings interest rates.");
            }
        }

        // GET: api/SavingsInterestRate/5
        [HttpGet("{id}")]
        [Authorize(Roles = "admin, support, staff")] // Admins, support, and staff can view a specific savings interest rate
        public async Task<ActionResult<SavingsInterestRate>> GetSavingsInterestRate(byte id)
        {
            try
            {
                var interestRates = await _savingsInterestRateService.GetSavingsInterestRateBySavingsInterestRateIdAsync(id);
                if (interestRates == null)
                    return NotFound("Savings interest rate not found.");

                return Ok(interestRates);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving savings interest rate.");
            }
        }

        // POST: api/SavingsInterestRate
        [HttpPost]
        [Authorize(Roles = "admin")] // Only admins can create savings interest rates
        public async Task<ActionResult> CreateSavingsInterestRate([FromBody] SavingsInterestRate savingsInterestRate)
        {
            if (savingsInterestRate == null)
                return BadRequest("Invalid savings interest rate data.");

            try
            {
                var result = await _savingsInterestRateService.CreateSavingsInterestRateAsync(savingsInterestRate);
                if (result)
                    return CreatedAtAction(nameof(GetSavingsInterestRate), new { id = savingsInterestRate.InterestSavingsRateId }, savingsInterestRate);

                return BadRequest("Failed to create savings interest rate.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating savings interest rate.");
            }
        }

        // PUT: api/SavingsInterestRate/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")] // Only admins can update savings interest rates
        public async Task<ActionResult> UpdateSavingsInterestRate(byte id, [FromBody] SavingsInterestRate savingsInterestRate)
        {
            if (savingsInterestRate == null || savingsInterestRate.InterestSavingsRateId != id)
                return BadRequest("Data mismatch.");

            try
            {
                var updatedInterestRate = await _savingsInterestRateService.UpdateSavingsInterestRateAsync(savingsInterestRate);
                if (updatedInterestRate != null)
                    return Ok(updatedInterestRate);

                return NotFound("Savings interest rate not found.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating savings interest rate.");
            }
        }

        // DELETE: api/SavingsInterestRate/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")] // Only admins can delete savings interest rates
        public async Task<ActionResult> DeleteSavingsInterestRate(byte id)
        {
            try
            {
                var interestRates = await _savingsInterestRateService.GetSavingsInterestRateBySavingsInterestRateIdAsync(id);
                if (interestRates == null)
                    return NotFound("Savings interest rate not found.");

                var result = await _savingsInterestRateService.DeleteSavingsInterestRateAsync(interestRates);
                if (result)
                    return NoContent();

                return BadRequest("Failed to delete savings interest rate.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting savings interest rate.");
            }
        }
    }
}
