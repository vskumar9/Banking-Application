using BankApplicationAPI.Models;

namespace BankApplicationAPI.Interfaces
{
    public interface ITransactionLog
    {
        Task<IEnumerable<TransactionLog>> GetTransactionLogsAsync();
        Task<TransactionLog> GetTransactionLogsByTransactionIdAsync(int TransactionId);
        Task<TransactionLog> UpdateTransactionLogAsync(TransactionLog transactionLog);
        Task<Boolean> DeleteTransactionLogAsync(TransactionLog transactionLog);
        Task<Boolean> CreateTransactionLogAsync(TransactionLog transactionLog);
        Task<IEnumerable<TransactionLog>> GetTransactionLogAsync(int? TransactionId = null, 
                                                    DateTime? TransactionDate = null, 
                                                    byte? TransactionTypeId = null, 
                                                    decimal? TransactionAmount = null, 
                                                    int? AccountId = null, 
                                                    string? EmployeeId = null, 
                                                    string? CustomerId = null);
    }
}
