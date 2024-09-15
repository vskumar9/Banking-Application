using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Services
{
    public class TransactionLogService
    {
        private readonly ITransactionLog _transactionLog;
        private readonly IAccount _account;

        public TransactionLogService(ITransactionLog transactionLog, IAccount account)
        {
            _transactionLog = transactionLog;
            _account = account;
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

        public async Task<bool> TransferFundsAsync(int fromAccountId, int toAccountId, decimal amount, string employeeId, string customerId)
        {
            // Fetch accounts
            var fromAccount = await _account.GetAccountsByAccountIdAsync(fromAccountId);
            var toAccount = await _account.GetAccountsByAccountIdAsync(toAccountId);

            if (fromAccount == null || toAccount == null)
                return false;

            if (fromAccount.CurrentBalance < amount)
                return false;

            // Create transaction logs
            var transactionLogFrom = new TransactionLog
            {
                TransactionDate = DateTime.UtcNow,
                TransactionTypeId = 1, // Assuming 1 is for 'Debit'
                TransactionAmount = amount,
                NewBalance = fromAccount.CurrentBalance - amount,
                AccountId = fromAccountId,
                EmployeeId = employeeId,
                CustomerId = customerId
            };

            var transactionLogTo = new TransactionLog
            {
                TransactionDate = DateTime.UtcNow,
                TransactionTypeId = 2, // Assuming 2 is for 'Credit'
                TransactionAmount = amount,
                NewBalance = toAccount.CurrentBalance + amount,
                AccountId = toAccountId,
                EmployeeId = employeeId,
                CustomerId = customerId
            };

            // Update account balances
            fromAccount.CurrentBalance -= amount;
            toAccount.CurrentBalance += amount;

            // Persist changes
            var resultFrom = await _transactionLog.CreateTransactionLogAsync(transactionLogFrom);
            var resultTo = await _transactionLog.CreateTransactionLogAsync(transactionLogTo);

            if (!resultFrom || !resultTo)
                return false;

            await _account.UpdateAccountAsync(fromAccount);
            await _account.UpdateAccountAsync(toAccount);

            return true;
        }


    }
}
