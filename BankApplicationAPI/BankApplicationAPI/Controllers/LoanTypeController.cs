using BankApplicationAPI.Models;
using BankApplicationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanTypeController : ControllerBase
    {
        private readonly LoanTypeService _loanTypeService;

        public LoanTypeController(LoanTypeService loanTypeService)
        {
            _loanTypeService = loanTypeService;
        }

        // GET: api/LoanType
        [HttpGet]
        [Authorize(Roles = "admin, support, staff")] // Admins, support, and staff can view all loan types
        public async Task<ActionResult<IEnumerable<LoanType>>> GetLoanTypes()
        {
            try
            {
                var loanTypes = await _loanTypeService.GetLoanTypesAsync();
                return Ok(loanTypes);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving loan types.");
            }
        }

        // GET: api/LoanType/5
        [HttpGet("{id}")]
        [Authorize(Roles = "admin, support, staff")] // Admins, support, and staff can view a specific loan type
        public async Task<ActionResult<LoanType>> GetLoanType(int id)
        {
            try
            {
                var loanTypes = await _loanTypeService.GetLoanTypeByLoanTypeIdAsync(id);
                if (loanTypes == null)
                    return NotFound("Loan type not found.");

                return Ok(loanTypes);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving loan type.");
            }
        }

        // POST: api/LoanType
        [HttpPost]
        [Authorize(Roles = "admin")] // Only admins can create loan types
        public async Task<ActionResult> CreateLoanType([FromBody] LoanType loanType)
        {
            if (loanType == null)
                return BadRequest("Invalid loan type data.");

            try
            {
                var result = await _loanTypeService.CreateLoanTypeAsync(loanType);
                if (result)
                    return CreatedAtAction(nameof(GetLoanType), new { id = loanType.LoanTypeId }, loanType);

                return BadRequest("Failed to create loan type.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating loan type.");
            }
        }

        // PUT: api/LoanType/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")] // Only admins can update loan types
        public async Task<ActionResult> UpdateLoanType(int id, [FromBody] LoanType loanType)
        {
            if (loanType == null || loanType.LoanTypeId != id)
                return BadRequest("Data mismatch.");

            try
            {
                var updatedLoanType = await _loanTypeService.UpdateLoanTypeAsync(loanType);
                if (updatedLoanType != null)
                    return Ok(updatedLoanType);

                return NotFound("Loan type not found.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating loan type.");
            }
        }

        // DELETE: api/LoanType/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")] // Only admins can delete loan types
        public async Task<ActionResult> DeleteLoanType(int id)
        {
            try
            {
                var loanTypes = await _loanTypeService.GetLoanTypeByLoanTypeIdAsync(id);
                if (loanTypes == null)
                    return NotFound("Loan type not found.");

                var result = await _loanTypeService.DeleteLoanTypeAsync(loanTypes);
                if (result)
                    return NoContent();

                return BadRequest("Failed to delete loan type.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting loan type.");
            }
        }
    }
}
