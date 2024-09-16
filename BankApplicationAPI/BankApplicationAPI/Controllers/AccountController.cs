using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BankApplicationAPI.Models;
using BankApplicationAPI.Services;
using System.Security.Claims;

namespace BankApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Ensure that all actions require authentication
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Authorize(Roles = "admin,staff")] // Only admin and staff can create an account
        public async Task<IActionResult> CreateAccount([FromBody] Account account)
        {
            var result = await _accountService.CreateAccountAsync(account);
            if (result)
            {
                return Ok("Account created successfully.");
            }
            return BadRequest("Failed to create account.");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin,staff")] // Only admin and staff can delete an account
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var account = await _accountService.GetAccountsByAccountIdAsync(id);
            if (account == null)
            {
                return NotFound("Account not found.");
            }

            var result = await _accountService.DeleteAccountAsync(account);
            if (result)
            {
                return Ok("Account deleted successfully.");
            }
            return BadRequest("Failed to delete account.");
        }

        [HttpGet]
        [Authorize(Roles = "admin,staff")] // Only admin and staff can list accounts
        public async Task<IActionResult> GetAccounts()
        {
            var accounts = await _accountService.GetAccountsAsync();
            return Ok(accounts);
        }

        [HttpGet("byId/{accountId}")]
        [Authorize(Roles = "admin, staff, support")]
        public async Task<IActionResult> GetAccountsByAccountId(int accountId)
        {
            var accounts = await _accountService.GetAccountsByAccountIdAsync(accountId);
            return Ok(accounts);
        }

        [HttpGet("customer")]
        [Authorize(Roles = "customer")] // Admins, staff, and the specific customer can get their accounts
        public async Task<IActionResult> GetAccountsByCustomerId()
        {

            var customerId = User.FindFirstValue(ClaimTypes.PrimarySid);
            if (string.IsNullOrEmpty(customerId))
                return Unauthorized("Invalid token.");

            var accounts = await _accountService.GetAccountsByCustomerIdAsync(customerId);
            if (accounts == null)
            {
                return NotFound("No accounts found for the given customer.");
            }

            return Ok(accounts);
        }

        [HttpPut]
        [Authorize(Roles = "admin,staff")] // Only admin and staff can update an account
        public async Task<IActionResult> UpdateAccount([FromBody] Account account)
        {
            var updatedAccount = await _accountService.UpdateAccountAsync(account);
            if (updatedAccount == null)
            {
                return NotFound("Account not found.");
            }
            return Ok(updatedAccount);
        }
    }
}
