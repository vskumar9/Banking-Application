using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Repository
{
    public class TransactionTypeRepository : ITransactionType
    {
        public Task<bool> CreateTransactionTypeAsync(TransactionType transactionType)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTransactionTypeAsync(TransactionType transactionType)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionType> GetTransactionTypeAsync(byte? TransactionTypeId = null, string? TransactionTypeName = null, decimal? TransactionFeeAmount = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TransactionType>> GetTransactionTypeByTransactionTypeIdAsync(byte TransactionTypeId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TransactionType>> GetTransactionTypesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TransactionType> UpdateTransactionTypeAsync(TransactionType transactionType)
        {
            throw new NotImplementedException();
        }
    }
}
