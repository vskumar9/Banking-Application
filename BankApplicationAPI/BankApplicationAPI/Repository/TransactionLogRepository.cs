using Microsoft.EntityFrameworkCore;
using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Repository
{
    public class TransactionLogRepository : ITransactionLog
    {
        private readonly SunBankContext _context;
        private readonly ILogger<TransactionLogRepository> _logger;

        public TransactionLogRepository(SunBankContext context, ILogger<TransactionLogRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Create a new TransactionLog
        public async Task<bool> CreateTransactionLogAsync(TransactionLog transactionLog)
        {
            try
            {
                await _context.TransactionLogs.AddAsync(transactionLog);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating TransactionLog");
                return false;
            }
        }

        // Delete a TransactionLog
        public async Task<bool> DeleteTransactionLogAsync(TransactionLog transactionLog)
        {
            try
            {
                _context.TransactionLogs.Remove(transactionLog);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting TransactionLog");
                return false;
            }
        }

        // Get a specific TransactionLog based on various optional parameters
        public async Task<IEnumerable<TransactionLog>> GetTransactionLogAsync(int? TransactionId = null, DateTime? TransactionDate = null, byte? TransactionTypeId = null, decimal? TransactionAmount = null, int? AccountId = null, string? EmployeeId = null, string? CustomerId = null)
        {
            try
            {
                var query = _context.TransactionLogs.AsQueryable();

                if (TransactionId.HasValue)
                {
                    query = query.Where(tl => tl.TransactionId == TransactionId.Value);
                }

                if (TransactionDate.HasValue)
                {
                    query = query.Where(tl => tl.TransactionDate == TransactionDate.Value);
                }

                if (TransactionTypeId.HasValue)
                {
                    query = query.Where(tl => tl.TransactionTypeId == TransactionTypeId.Value);
                }

                if (TransactionAmount.HasValue)
                {
                    query = query.Where(tl => tl.TransactionAmount == TransactionAmount.Value);
                }

                if (AccountId.HasValue)
                {
                    query = query.Where(tl => tl.AccountId == AccountId.Value);
                }

                if (!string.IsNullOrEmpty(EmployeeId))
                {
                    query = query.Where(tl => tl.EmployeeId == EmployeeId);
                }

                if (!string.IsNullOrEmpty(CustomerId))
                {
                    query = query.Where(tl => tl.CustomerId == CustomerId);
                }

                var result = await query
                    .Include(tl => tl.Account)
                    .Include(tl => tl.Customer)
                    .Include(tl => tl.Employee)
                    .Include(tl => tl.TransactionType)
                    .ToListAsync();

                if (result == null)
                {
                    throw new KeyNotFoundException("TransactionLog not found with the provided criteria.");
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving TransactionLog");
                throw;
            }
        }

        // Get TransactionLogs by TransactionId
        public async Task<TransactionLog> GetTransactionLogsByTransactionIdAsync(int TransactionId)
        {
            try
            {
                return await _context.TransactionLogs
                    .Where(tl => tl.TransactionId == TransactionId)
                    .Include(tl => tl.Account)
                    .Include(tl => tl.Customer)
                    .Include(tl => tl.Employee)
                    .Include(tl => tl.TransactionType)
                    .FirstOrDefaultAsync() ?? throw new NullReferenceException();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving TransactionLogs by TransactionId");
                throw;
            }
        }

        // Get all TransactionLogs
        public async Task<IEnumerable<TransactionLog>> GetTransactionLogsAsync()
        {
            try
            {
                return await _context.TransactionLogs
                    .Include(tl => tl.Account)
                    .Include(tl => tl.Customer)
                    .Include(tl => tl.Employee)
                    .Include(tl => tl.TransactionType)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all TransactionLogs");
                throw;
            }
        }

        // Update an existing TransactionLog
        public async Task<TransactionLog> UpdateTransactionLogAsync(TransactionLog transactionLog)
        {
            try
            {
                _context.TransactionLogs.Update(transactionLog);
                await _context.SaveChangesAsync();
                return transactionLog;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating TransactionLog");
                throw;
            }
        }

    }
}
