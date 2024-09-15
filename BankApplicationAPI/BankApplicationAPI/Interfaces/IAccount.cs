using BankApplicationAPI.Models;

namespace BankApplicationAPI.Interfaces
{
    public interface IAccount
    {
        Task<IEnumerable<Account>> GetAccountsAsync();
        Task<Account> GetAccountsByAccountIdAsync(int accountId);
        Task<IEnumerable<Account>> GetAccountsByCustomerIdAsync(string accountId);
        Task<Account> UpdateAccountAsync(Account account);
        Task<Boolean> DeleteAccountAsync(Account account);
        Task<Boolean> CreateAccountAsync(Account account);
        Task<Account> GetAccountAsync(int? AccountId = null,
                                      string? AccountStatusType = null,
                                      string? CustomerId = null);

    }
}
