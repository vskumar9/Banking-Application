using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Services
{
    public class TransactionLogService
    {
        private readonly ITransactionLog _transactionLog;

        public TransactionLogService(ITransactionLog transactionLog)
        {
            _transactionLog = transactionLog;
        }

        public async Task<bool> CreateTransactionLogAsync(TransactionLog transactionLog)
        {
            try
            {
                return await _transactionLog.CreateTransactionLogAsync(transactionLog);
            }
            catch { throw; }
        }

        public async Task<bool> DeleteTransactionLogAsync(TransactionLog transactionLog)
        {
            try
            {
                return await _transactionLog.DeleteTransactionLogAsync(transactionLog);
            }
            catch { throw; }
        }

        public async Task<TransactionLog> GetTransactionLogAsync(int? TransactionId = null, DateTime? TransactionDate = null, byte? TransactionTypeId = null, decimal? TransactionAmount = null, int? AccountId = null, string? EmployeeId = null, string? CustomerId = null)
        {
            try
            {
                return await _transactionLog.GetTransactionLogAsync(TransactionId, TransactionDate, TransactionTypeId, TransactionAmount, AccountId, EmployeeId, CustomerId);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<TransactionLog>> GetTransactionLogsByTransactionIdAsync(int TransactionId)
        {
            try
            {
                return await _transactionLog.GetTransactionLogsByTransactionIdAsync(TransactionId);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<TransactionLog>> GetTransactionLogsAsync()
        {
            try
            {
                return await _transactionLog.GetTransactionLogsAsync();
            }
            catch { throw; }
        }

        public async Task<TransactionLog> UpdateTransactionLogAsync(TransactionLog transactionLog)

        {
            try
            {
                return await _transactionLog.UpdateTransactionLogAsync(transactionLog);
            }
            catch { throw; }
        }
    }
}
