using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApplicationAPI.Repository
{
    public class AccountRepository : IAccount
    {
        private readonly SunBankContext _context;
        private readonly ILogger<AccountRepository> _logger;

        public AccountRepository(SunBankContext context, ILogger<AccountRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Create Account with exception handling
        public async Task<bool> CreateAccountAsync(Account account)
        {
            try
            {
                await _context.Accounts.AddAsync(account);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Error creating account: {ex.Message}", ex);
                throw new Exception("An error occurred while creating the account.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred: {ex.Message}", ex);
                throw new Exception("An unexpected error occurred.");
            }
        }

        // Delete Account with exception handling
        public async Task<bool> DeleteAccountAsync(Account account)
        {
            try
            {
                _context.Accounts.Remove(account);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Error deleting account: {ex.Message}", ex);
                throw new Exception("An error occurred while deleting the account.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred: {ex.Message}", ex);
                throw new Exception("An unexpected error occurred.");
            }
        }

        // Get Account by optional parameters with exception handling
        public async Task<Account> GetAccountAsync(int? AccountId = null, string? AccountStatusType = null, string? CustomerId = null)
        {
            try
            {
                var query = _context.Accounts.AsQueryable();

                if (AccountId.HasValue)
                    query = query.Where(a => a.AccountId == AccountId);

                if (!string.IsNullOrEmpty(AccountStatusType))
                    query = query.Where(a => a.AccountStatusType!.AccountStatusDescription == AccountStatusType);

                if (!string.IsNullOrEmpty(CustomerId))
                    query = query.Where(a => a.CustomerId == CustomerId);

                return await query.Include(a => a.AccountStatusType).Include(a => a.Customer).Include(a => a.InterestSavingsRate)
                                                                    .FirstOrDefaultAsync() ?? throw new NullReferenceException();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching account: {ex.Message}", ex);
                throw new Exception("An error occurred while fetching the account.");
            }
        }

        // Get all Accounts with exception handling
        public async Task<IEnumerable<Account>> GetAccountsAsync()
        {
            try
            {
                return await _context.Accounts.Include(a => a.AccountStatusType).Include(a => a.Customer)
                                     .Include(a => a.InterestSavingsRate).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching accounts: {ex.Message}", ex);
                throw new Exception("An error occurred while fetching the accounts.");
            }
        }

        // Get Accounts by AccountId with exception handling
        public async Task<Account> GetAccountsByAccountIdAsync(int accountId)
        {
            try
            {
                return await _context.Accounts.Where(a => a.AccountId == accountId).Include(a => a.AccountStatusType)
                                     .Include(a => a.Customer).Include(a => a.InterestSavingsRate)
                                     .FirstOrDefaultAsync() ?? throw new NullReferenceException("Account not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching accounts by AccountId: {ex.Message}", ex);
                throw new Exception("An error occurred while fetching the account by ID.");
            }
        }

        // Update Account with exception handling
        public async Task<Account> UpdateAccountAsync(Account account)
        {
            try
            {
                _context.Accounts.Update(account);
                await _context.SaveChangesAsync();
                return account;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Error updating account: {ex.Message}", ex);
                throw new Exception("An error occurred while updating the account.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred: {ex.Message}", ex);
                throw new Exception("An unexpected error occurred.");
            }
        }

        public async Task<IEnumerable<Account>> GetAccountsByCustomerIdAsync(string customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("Customer ID cannot be null or empty", nameof(customerId));
            }

            try
            {
                var accounts = await _context.Accounts
                    .Where(a => a.CustomerId == customerId) 
                    .Include(a => a.Customer)
                    .ToListAsync(); 
                return accounts;
            }
            catch (Exception ex)
            {
                 _logger.LogError(ex, "An error occurred while retrieving accounts for customer ID: {CustomerId}", customerId);
                throw new ApplicationException("An error occurred while retrieving accounts.", ex);
            }
        }

    }
}
