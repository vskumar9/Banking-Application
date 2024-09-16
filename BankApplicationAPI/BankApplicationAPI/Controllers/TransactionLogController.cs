using BankApplicationAPI.Models;
using BankApplicationAPI.Repository;
using BankApplicationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionLogController : ControllerBase
    {
        private readonly TransactionLogService _transactionLogService;
        private readonly ILogger<TransactionLogController> _logger;

        public TransactionLogController(TransactionLogService transactionLogService, ILogger<TransactionLogController> logger)
        {
            _transactionLogService = transactionLogService;
            _logger = logger;
        }

        // GET: api/TransactionLog
        [HttpGet]
        [Authorize(Roles = "admin, support, cashier")] // Admins, support, and cashiers can view all transaction logs
        public async Task<ActionResult<IEnumerable<TransactionLog>>> GetTransactionLogs()
        {
            try
            {
                var transactionLogs = await _transactionLogService.GetTransactionLogsAsync();
                return Ok(transactionLogs);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving transaction logs.");
            }
        }

        // GET: api/TransactionLog/5
        [HttpGet("{id}")]
        [Authorize(Roles = "admin, support, cashier, staff")] // Admins, support, cashiers, and staff can view a specific transaction log
        public async Task<ActionResult<TransactionLog>> GetTransactionLog(int id)
        {
            try
            {
                var transactionLogs = await _transactionLogService.GetTransactionLogsByTransactionIdAsync(id);
                if (transactionLogs == null)
                    return NotFound("Transaction log not found.");

                return Ok(transactionLogs);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving transaction log.");
            }
        }

        // POST: api/TransactionLog
        [HttpPost]
        [Authorize(Roles = "admin, cashier")] // Admins and cashiers can create transaction logs
        public async Task<ActionResult> CreateTransactionLog([FromBody] TransactionLog transactionLog)
        {
            if (transactionLog == null)
                return BadRequest("Invalid transaction log data.");

            try
            {
                var result = await _transactionLogService.CreateTransactionLogAsync(transactionLog);
                if (result)
                    return CreatedAtAction(nameof(GetTransactionLog), new { id = transactionLog.TransactionId }, transactionLog);

                return BadRequest("Failed to create transaction log.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating transaction log.");
            }
        }

        // PUT: api/TransactionLog/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin, cashier")] // Admins and cashiers can update transaction logs
        public async Task<ActionResult> UpdateTransactionLog(int id, [FromBody] TransactionLog transactionLog)
        {
            if (transactionLog == null || transactionLog.TransactionId != id)
                return BadRequest("Data mismatch.");

            try
            {
                var updatedTransactionLog = await _transactionLogService.UpdateTransactionLogAsync(transactionLog);
                if (updatedTransactionLog != null)
                    return Ok(updatedTransactionLog);

                return NotFound("Transaction log not found.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating transaction log.");
            }
        }

        // DELETE: api/TransactionLog/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")] // Only admins can delete transaction logs
        public async Task<ActionResult> DeleteTransactionLog(int id)
        {
            try
            {
                var transactionLogs = await _transactionLogService.GetTransactionLogsByTransactionIdAsync(id);
                if (transactionLogs == null)
                    return NotFound("Transaction log not found.");

                var result = await _transactionLogService.DeleteTransactionLogAsync(transactionLogs);
                if (result)
                    return NoContent();

                return BadRequest("Failed to delete transaction log.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting transaction log.");
            }
        }

        [HttpPost("transfer")]
        [Authorize(Roles = "admin, staff, cashier, customer")]
        public async Task<ActionResult> TransferFunds(byte TransactionTypeId, [FromBody] TransferRequest request)
        {
            if (request == null || request.Amount <= 0 || request.FromAccountId <= 0 || request.ToAccountId <= 0)
                return BadRequest("Invalid transfer request.");

            try
            {
                var employeeId = User.FindFirst("EmployeeId")?.Value;
                var customerId = User.FindFirst("CustomerId")?.Value;

                if (string.IsNullOrEmpty(employeeId) || string.IsNullOrEmpty(customerId))
                    return Unauthorized("Invalid user credentials.");

                var result = await _transactionLogService.TransferFundsAsync(request.FromAccountId, request.ToAccountId, request.Amount, employeeId, customerId, TransactionTypeId);

                if (result)
                    return Ok("Transfer successful.");

                return BadRequest("Failed to complete the transfer. Check account details and balance.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing the transfer: {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error processing the transfer.");
            }
        }


    }
}
