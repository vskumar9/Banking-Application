using BankApplicationAPI.Models;

namespace BankApplicationAPI.Interfaces
{
    public interface ITransactionLog
    {
        Task<IEnumerable<TransactionLog>> GetTransactionLogsAsync();
        Task<IEnumerable<TransactionLog>> GetTransactionLogByTransactionLogIdAsync(byte TransactionId);
        Task<TransactionLog> UpdateTransactionLogAsync(TransactionLog transactionLog);
        Task<Boolean> DeleteTransactionLogAsync(TransactionLog transactionLog);
        Task<Boolean> CreateTransactionLogAsync(TransactionLog transactionLog);
        Task<TransactionLog> GetTransactionLogAsync(byte? TransactionId = null,
                                                    byte? TransactionTypeId = null,
                                                    DateTime? TransactionDate = null,
                                                    int? AccountId = null,
                                                    decimal? TransactionAmount = null,
                                                    string? EmployeeId = null,
                                                    string? CustomerId = null,
                                                    int? PermissionId = null);
    }
}
