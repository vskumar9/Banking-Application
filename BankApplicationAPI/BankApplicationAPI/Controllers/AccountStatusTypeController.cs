using BankApplicationAPI.Models;
using BankApplicationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountStatusTypeController : ControllerBase
    {
        private readonly AccountStatusTypeService _accountStatusTypeService;

        public AccountStatusTypeController(AccountStatusTypeService accountStatusTypeService)
        {
            _accountStatusTypeService = accountStatusTypeService;
        }

        // GET: api/AccountStatusType
        [HttpGet]
        [Authorize(Roles = "admin, support")] // Only admins and support can view the list
        public async Task<ActionResult<IEnumerable<AccountStatusType>>> GetAccountStatusTypes()
        {
            try
            {
                var result = await _accountStatusTypeService.GetAccountStatusTypesAsync();
                if (result == null || !result.Any())
                    return NotFound("No account status types found.");

                return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }

        // GET: api/AccountStatusType/5
        [HttpGet("{id}")]
        [Authorize(Roles = "admin, support")] // Only admins and support can view a specific account status type
        public async Task<ActionResult<AccountStatusType>> GetAccountStatusType(byte id)
        {
            try
            {
                var result = await _accountStatusTypeService.GetAccountStatusTypeByAccountStatusTypeIdAsync(id);
                if (result == null || !result.Any())
                    return NotFound("Account status type not found.");

                return Ok(result.First());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }

        // POST: api/AccountStatusType
        [HttpPost]
        [Authorize(Roles = "admin")] // Only admins can create a new account status type
        public async Task<ActionResult> CreateAccountStatusType([FromBody] AccountStatusType accountStatusType)
        {
            if (accountStatusType == null)
                return BadRequest("Invalid data.");

            try
            {
                var created = await _accountStatusTypeService.CreateAccountStatusTypeAsync(accountStatusType);
                if (created)
                    return CreatedAtAction(nameof(GetAccountStatusType), new { id = accountStatusType.AccountStatusTypeId }, accountStatusType);

                return BadRequest("Failed to create account status type.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating data.");
            }
        }

        // PUT: api/AccountStatusType/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")] // Only admins can update an account status type
        public async Task<ActionResult> UpdateAccountStatusType(byte id, [FromBody] AccountStatusType accountStatusType)
        {
            if (accountStatusType == null || accountStatusType.AccountStatusTypeId != id)
                return BadRequest("Data mismatch.");

            try
            {
                var updated = await _accountStatusTypeService.UpdateAccountStatusTypeAsync(accountStatusType);
                if (updated != null)
                    return Ok(updated);

                return NotFound("Account status type not found.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data.");
            }
        }

        // DELETE: api/AccountStatusType/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")] // Only admins can delete an account status type
        public async Task<ActionResult> DeleteAccountStatusType(byte id)
        {
            try
            {
                var accountStatusType = await _accountStatusTypeService.GetAccountStatusTypeByAccountStatusTypeIdAsync(id);
                if (accountStatusType == null || !accountStatusType.Any())
                    return NotFound("Account status type not found.");

                var deleted = await _accountStatusTypeService.DeleteAccountStatusTypeAsync(accountStatusType.First());
                if (deleted)
                    return NoContent();

                return BadRequest("Failed to delete account status type.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data.");
            }
        }
    }
}
