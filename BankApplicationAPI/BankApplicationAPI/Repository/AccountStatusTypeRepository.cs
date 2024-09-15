using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApplicationAPI.Repository
{
    public class AccountStatusTypeRepository : IAccountStatusType
    {
        private readonly SunBankContext _context;
        private readonly ILogger<AccountStatusTypeRepository> _logger;

        public AccountStatusTypeRepository(SunBankContext context, ILogger<AccountStatusTypeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Create a new AccountStatusType
        public async Task<bool> CreateAccountStatusTypeAsync(AccountStatusType accountStatusType)
        {
            try
            {
                await _context.AccountStatusTypes.AddAsync(accountStatusType);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Error creating account status type: {ex.Message}", ex);
                throw new Exception("An error occurred while creating the account status type.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred: {ex.Message}", ex);
                throw new Exception("An unexpected error occurred.");
            }
        }

        // Delete an AccountStatusType
        public async Task<bool> DeleteAccountStatusTypeAsync(AccountStatusType accountStatusType)
        {
            try
            {
                _context.AccountStatusTypes.Remove(accountStatusType);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Error deleting account status type: {ex.Message}", ex);
                throw new Exception("An error occurred while deleting the account status type.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred: {ex.Message}", ex);
                throw new Exception("An unexpected error occurred.");
            }
        }

        // Get AccountStatusType by optional parameters (AccountStatusTypeId or AccountStatusDescription)
        public async Task<AccountStatusType> GetAccountStatusTypeAsync(byte? AccountStatusTypeId = null, string? AccountStatusDescription = null)
        {
            try
            {
                var query = _context.AccountStatusTypes.AsQueryable();

                if (AccountStatusTypeId.HasValue)
                    query = query.Where(a => a.AccountStatusTypeId == AccountStatusTypeId);

                if (!string.IsNullOrEmpty(AccountStatusDescription))
                    query = query.Where(a => a.AccountStatusDescription == AccountStatusDescription);

                return await query.FirstOrDefaultAsync() ?? throw new NullReferenceException();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching account status type: {ex.Message}", ex);
                throw new Exception("An error occurred while fetching the account status type.");
            }
        }

        // Get AccountStatusType by AccountStatusTypeId
        public async Task<IEnumerable<AccountStatusType>> GetAccountStatusTypeByAccountStatusTypeIdAsync(byte AccountStatusTypeId)
        {
            try
            {
                return await _context.AccountStatusTypes
                                     .Where(a => a.AccountStatusTypeId == AccountStatusTypeId)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching account status type by ID: {ex.Message}", ex);
                throw new Exception("An error occurred while fetching the account status type by ID.");
            }
        }

        // Get all AccountStatusTypes
        public async Task<IEnumerable<AccountStatusType>> GetAccountStatusTypesAsync()
        {
            try
            {
                return await _context.AccountStatusTypes.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching account status types: {ex.Message}", ex);
                throw new Exception("An error occurred while fetching account status types.");
            }
        }

        // Update AccountStatusType
        public async Task<AccountStatusType> UpdateAccountStatusTypeAsync(AccountStatusType accountStatusType)
        {
            try
            {
                _context.AccountStatusTypes.Update(accountStatusType);
                await _context.SaveChangesAsync();
                return accountStatusType;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Error updating account status type: {ex.Message}", ex);
                throw new Exception("An error occurred while updating the account status type.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred: {ex.Message}", ex);
                throw new Exception("An unexpected error occurred.");
            }
        }
    }
}
