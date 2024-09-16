using BankApplicationAPI.Models;

namespace BankApplicationAPI.Interfaces
{
    public interface ITransactionType
    {
        Task<IEnumerable<TransactionType>> GetTransactionTypesAsync();
        Task<TransactionType> GetTransactionTypeByTransactionTypeIdAsync(byte TransactionTypeId);
        Task<TransactionType> UpdateTransactionTypeAsync(TransactionType transactionType);
        Task<Boolean> DeleteTransactionTypeAsync(TransactionType transactionType);
        Task<Boolean> CreateTransactionTypeAsync(TransactionType transactionType);
        Task<IEnumerable<TransactionType>> GetTransactionTypeAsync(byte? TransactionTypeId = null,
                                                    string? TransactionTypeName = null,
                                                    decimal? TransactionFeeAmount = null);
    }
}
