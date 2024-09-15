using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Services
{
    public class TransactionTypeService
    {
        private readonly ITransactionType _transactionType;

        public TransactionTypeService(ITransactionType transactionType)
        {
            _transactionType = transactionType;
        }

        public async Task<bool> CreateTransactionTypeAsync(TransactionType transactionType)
        {
            try
            {
                return await _transactionType.CreateTransactionTypeAsync(transactionType);
            }
            catch { throw; }
        }

        public async Task<bool> DeleteTransactionTypeAsync(TransactionType transactionType)
        {
            try
            {
                return await _transactionType.DeleteTransactionTypeAsync(transactionType);  
            }
            catch { throw; }
        }

        public async Task<TransactionType> GetTransactionTypeAsync(byte? TransactionTypeId = null, string? TransactionTypeName = null, decimal? TransactionFeeAmount = null)
        {
            try
            {
                return await _transactionType.GetTransactionTypeAsync(TransactionTypeId, TransactionTypeName, TransactionFeeAmount);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<TransactionType>> GetTransactionTypeByTransactionTypeIdAsync(byte TransactionTypeId)
        {
            try
            {
                return await _transactionType.GetTransactionTypeByTransactionTypeIdAsync(TransactionTypeId);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<TransactionType>> GetTransactionTypesAsync()
        {
            try
            {
                return await _transactionType.GetTransactionTypesAsync();
            }
            catch { throw; }
        }

        public async Task<TransactionType> UpdateTransactionTypeAsync(TransactionType transactionType)
        {
            try
            {
                return await _transactionType.UpdateTransactionTypeAsync(transactionType);
            }
            catch { throw; }
        }
    }
}
