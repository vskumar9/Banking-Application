using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Services
{
    public class AccountService
    {
        private readonly IAccount _account;

        public AccountService(IAccount account)
        {
            _account = account;
        }

        public async Task<bool> CreateAccountAsync(Account account)
        {
            try
            {
                if(account == null) return false;
                return await _account.CreateAccountAsync(account);
            }
            catch { throw; }
        }

        public async Task<bool> DeleteAccountAsync(Account account)
        {
            try
            {
                if(account == null) return false;
                return await _account.DeleteAccountAsync(account);
            }
            catch { throw; }
        }

        public async Task<Account> GetAccountAsync(int? AccountId = null, string? AccountStatusType = null, string? CustomerId = null)
        {
            try
            {
                return await _account.GetAccountAsync(AccountId, AccountStatusType, CustomerId);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<Account>> GetAccountsAsync()
        {
            try
            {
                return await _account.GetAccountsAsync();
            }
            catch { throw; }
        }

        public async Task<IEnumerable<Account>> GetAccountsByAccountIdAsync(int accountId)
        {
            try
            {
                return await _account.GetAccountsByAccountIdAsync(accountId);
            }
            catch { throw; }
        }

        public async Task<Account> UpdateAccountAsync(Account account)
        {
            try
            {
                if (account == null) return null!;
                return await _account.UpdateAccountAsync(account);
            }
            catch { throw; }
        }



    }
}
