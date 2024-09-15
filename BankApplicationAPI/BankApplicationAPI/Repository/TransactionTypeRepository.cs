using Microsoft.EntityFrameworkCore;
using BankApplicationAPI.Models;
using BankApplicationAPI.Interfaces;

namespace BankApplicationAPI.Repository
{
    public class TransactionTypeRepository : ITransactionType
    {
        private readonly SunBankContext _context;
        private readonly ILogger<TransactionTypeRepository> _logger;

        public TransactionTypeRepository(SunBankContext context, ILogger<TransactionTypeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Create a new TransactionType
        public async Task<bool> CreateTransactionTypeAsync(TransactionType transactionType)
        {
            try
            {
                _context.TransactionTypes.Add(transactionType);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating TransactionType");
                return false;
            }
        }

        // Delete an existing TransactionType
        public async Task<bool> DeleteTransactionTypeAsync(TransactionType transactionType)
        {
            try
            {
                _context.TransactionTypes.Remove(transactionType);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting TransactionType");
                return false;
            }
        }

        // Get a specific TransactionType based on optional parameters
        public async Task<TransactionType> GetTransactionTypeAsync(byte? TransactionTypeId = null, string? TransactionTypeName = null, decimal? TransactionFeeAmount = null)
        {
            try
            {
                var query = _context.TransactionTypes.AsQueryable();

                if (TransactionTypeId.HasValue)
                {
                    query = query.Where(tt => tt.TransactionTypeId == TransactionTypeId.Value);
                }

                if (!string.IsNullOrEmpty(TransactionTypeName))
                {
                    query = query.Where(tt => tt.TransactionTypeName == TransactionTypeName);
                }

                if (TransactionFeeAmount.HasValue)
                {
                    query = query.Where(tt => tt.TransactionFeeAmount == TransactionFeeAmount.Value);
                }

                return await query.FirstOrDefaultAsync() ?? throw new NullReferenceException("TransactionType not found with the provided criteria.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving TransactionType");
                throw;
            }
        }

        // Get a specific TransactionType by its ID
        public async Task<IEnumerable<TransactionType>> GetTransactionTypeByTransactionTypeIdAsync(byte TransactionTypeId)
        {
            try
            {
                return await _context.TransactionTypes
                    .Where(tt => tt.TransactionTypeId == TransactionTypeId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving TransactionType by ID");
                throw;
            }
        }

        // Get all TransactionTypes
        public async Task<IEnumerable<TransactionType>> GetTransactionTypesAsync()
        {
            try
            {
                return await _context.TransactionTypes.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving TransactionTypes");
                throw;
            }
        }

        // Update an existing TransactionType
        public async Task<TransactionType> UpdateTransactionTypeAsync(TransactionType transactionType)
        {
            try
            {
                var existingTransactionType = await _context.TransactionTypes
                    .FirstOrDefaultAsync(tt => tt.TransactionTypeId == transactionType.TransactionTypeId);

                if (existingTransactionType == null)
                {
                    throw new KeyNotFoundException("TransactionType not found");
                }

                // Update fields
                existingTransactionType.TransactionTypeName = transactionType.TransactionTypeName;
                existingTransactionType.TransactionTypeDescription = transactionType.TransactionTypeDescription;
                existingTransactionType.TransactionFeeAmount = transactionType.TransactionFeeAmount;

                _context.TransactionTypes.Update(existingTransactionType);
                await _context.SaveChangesAsync();

                return existingTransactionType!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating TransactionType");
                throw;
            }
        }
    }
}
