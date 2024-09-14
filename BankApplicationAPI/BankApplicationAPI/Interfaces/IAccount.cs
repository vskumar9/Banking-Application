using BankApplicationAPI.Models;

namespace BankApplicationAPI.Interfaces
{
    public interface IAccount
    {
        Task<IEnumerable<Account>> GetAccountsAsync();
        Task<IEnumerable<Account>> GetAccountsByAccountIdAsync(int accountId);
        Task<Account> UpdateAccountAsync(Account account);
        Task<Boolean> DeleteAccountAsync(Account account);
        Task<Boolean> CreateAccountAsync(Account account);
        Task<Account> GetAccountAsync(int? AccountId = null,
                                      string? AccountStatusType = null,
                                      string? CustomerId = null);

    }
}
