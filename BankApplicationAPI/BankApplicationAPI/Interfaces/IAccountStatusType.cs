using BankApplicationAPI.Models;

namespace BankApplicationAPI.Interfaces
{
    public interface IAccountStatusType
    {
        Task<IEnumerable<AccountStatusType>> GetAccountStatusTypesAsync();
        Task<IEnumerable<AccountStatusType>> GetAccountStatusTypeByAccountStatusTypeIdAsync(byte AccountStatusTypeId);
        Task<AccountStatusType> UpdateAccountStatusTypeAsync(AccountStatusType AccountStatusType);
        Task<Boolean> DeleteAccountStatusTypeAsync(AccountStatusType accountStatusType);
        Task<Boolean> CreateAccountStatusTypeAsync(AccountStatusType accountStatusType);
        Task<AccountStatusType> GetAccountStatusTypeAsync(byte? AccountStatusTypeId = null,
                                                          string? AccountStatusDescription = null);
    }
}
