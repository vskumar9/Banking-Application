using BankApplicationAPI.Models;
using BankApplicationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionTypeController : ControllerBase
    {
        private readonly TransactionTypeService _transactionTypeService;

        public TransactionTypeController(TransactionTypeService transactionTypeService)
        {
            _transactionTypeService = transactionTypeService;
        }

        // GET: api/TransactionType
        [HttpGet]
        [Authorize(Roles = "admin, support, cashier")] // Admins, support, and cashiers can view all transaction types
        public async Task<ActionResult<IEnumerable<TransactionType>>> GetTransactionTypes()
        {
            try
            {
                var transactionTypes = await _transactionTypeService.GetTransactionTypesAsync();
                return Ok(transactionTypes);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving transaction types.");
            }
        }

        // GET: api/TransactionType/5
        [HttpGet("{id}")]
        [Authorize(Roles = "admin, support, cashier")] // Admins, support, and cashiers can view a specific transaction type
        public async Task<ActionResult<TransactionType>> GetTransactionType(byte id)
        {
            try
            {
                var transactionTypes = await _transactionTypeService.GetTransactionTypeByTransactionTypeIdAsync(id);
                if (transactionTypes == null || !transactionTypes.Any())
                    return NotFound("Transaction type not found.");

                return Ok(transactionTypes.First());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving transaction type.");
            }
        }

        // POST: api/TransactionType
        [HttpPost]
        [Authorize(Roles = "admin")] // Only admins can create transaction types
        public async Task<ActionResult> CreateTransactionType([FromBody] TransactionType transactionType)
        {
            if (transactionType == null)
                return BadRequest("Invalid transaction type data.");

            try
            {
                var result = await _transactionTypeService.CreateTransactionTypeAsync(transactionType);
                if (result)
                    return CreatedAtAction(nameof(GetTransactionType), new { id = transactionType.TransactionTypeId }, transactionType);

                return BadRequest("Failed to create transaction type.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating transaction type.");
            }
        }

        // PUT: api/TransactionType/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")] // Only admins can update transaction types
        public async Task<ActionResult> UpdateTransactionType(byte id, [FromBody] TransactionType transactionType)
        {
            if (transactionType == null || transactionType.TransactionTypeId != id)
                return BadRequest("Data mismatch.");

            try
            {
                var updatedTransactionType = await _transactionTypeService.UpdateTransactionTypeAsync(transactionType);
                if (updatedTransactionType != null)
                    return Ok(updatedTransactionType);

                return NotFound("Transaction type not found.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating transaction type.");
            }
        }

        // DELETE: api/TransactionType/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")] // Only admins can delete transaction types
        public async Task<ActionResult> DeleteTransactionType(byte id)
        {
            try
            {
                var transactionTypes = await _transactionTypeService.GetTransactionTypeByTransactionTypeIdAsync(id);
                if (transactionTypes == null || !transactionTypes.Any())
                    return NotFound("Transaction type not found.");

                var result = await _transactionTypeService.DeleteTransactionTypeAsync(transactionTypes.First());
                if (result)
                    return NoContent();

                return BadRequest("Failed to delete transaction type.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting transaction type.");
            }
        }
    }
}
