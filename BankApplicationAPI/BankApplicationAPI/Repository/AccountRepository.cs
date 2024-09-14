using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Repository
{
    public class AccountRepository : IAccount
    {
        public Task<bool> CreateAccountAsync(Account account)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAccountAsync(Account account)
        {
            throw new NotImplementedException();
        }

        public Task<Account> GetAccountAsync(int? AccountId = null, string? AccountStatusType = null, string? CustomerId = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Account>> GetAccountsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Account>> GetAccountsByAccountIdAsync(int accountId)
        {
            throw new NotImplementedException();
        }

        public Task<Account> UpdateAccountAsync(Account account)
        {
            throw new NotImplementedException();
        }
    }
}
