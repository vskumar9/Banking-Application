using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Services
{
    public class TransactionLogService
    {
        private readonly ITransactionLog _transactionLog;
        private readonly IAccount _account;
        private readonly ITransactionType _transactionType;

        public TransactionLogService(ITransactionLog transactionLog, IAccount account, ITransactionType transactionType)
        {
            _transactionLog = transactionLog;
            _account = account;
            _transactionType = transactionType;
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

        public async Task<IEnumerable<TransactionLog>> GetTransactionLogAsync(int? TransactionId = null, DateTime? TransactionDate = null, byte? TransactionTypeId = null, decimal? TransactionAmount = null, int? AccountId = null, string? EmployeeId = null, string? CustomerId = null)
        {
            try
            {
                return await _transactionLog.GetTransactionLogAsync(TransactionId, TransactionDate, TransactionTypeId, TransactionAmount, AccountId, EmployeeId, CustomerId);
            }
            catch { throw; }
        }

        public async Task<TransactionLog> GetTransactionLogsByTransactionIdAsync(int TransactionId)
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

        public async Task<bool> TransferFundsAsync(int fromAccountId, int toAccountId, decimal amount, string employeeId, string customerId, byte TransactionTypeId)
        {

            var fromAccount = await _account.GetAccountsByAccountIdAsync(fromAccountId);
            var toAccount = await _account.GetAccountsByAccountIdAsync(toAccountId);

            // Validate accounts and balance
            if (fromAccount == null || toAccount == null)
                return false;

            if (fromAccount.CurrentBalance < amount)
                return false;

            var types = await _transactionType.GetTransactionTypeByTransactionTypeIdAsync(TransactionTypeId);

            if (types == null) return false;

            decimal transferFee = 0;
            
            transferFee += types.TransactionFeeAmount ?? 0;

            var totalDeduction = amount + transferFee;


            if (fromAccount.CurrentBalance < totalDeduction)
                return false;

            // Create transaction log for 'fromAccount' (Withdrawal + Fee)
            var transactionLogFrom = new TransactionLog
            {
                TransactionDate = DateTime.UtcNow,
                TransactionTypeId = TransactionTypeId, // 2 for Withdrawal
                TransactionAmount = totalDeduction,
                NewBalance = fromAccount.CurrentBalance - totalDeduction,
                AccountId = fromAccountId,
                EmployeeId = employeeId,
                CustomerId = customerId
            };

            var toCustomerId = await _account.GetAccountsByAccountIdAsync(toAccountId);

            // Create transaction log for 'toAccount' (Deposit)
            var transactionLogTo = new TransactionLog
            {
                TransactionDate = DateTime.UtcNow,
                TransactionTypeId = 1, // 1 for Deposit
                TransactionAmount = amount,
                NewBalance = toAccount.CurrentBalance + amount,
                AccountId = toAccountId,
                EmployeeId = employeeId,
                CustomerId = toCustomerId.CustomerId
            };

            // Update balances
            fromAccount.CurrentBalance -= totalDeduction;
            toAccount.CurrentBalance += amount;

            // Save the transaction logs
            var resultFrom = await _transactionLog.CreateTransactionLogAsync(transactionLogFrom);
            var resultTo = await _transactionLog.CreateTransactionLogAsync(transactionLogTo);

            // Check if both transactions were saved
            if (!resultFrom || !resultTo)
                return false;

            // Persist the updated account balances
            await _account.UpdateAccountAsync(fromAccount);
            await _account.UpdateAccountAsync(toAccount);

            return true;
        }
    }
}
