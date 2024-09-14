using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Repository
{
    public class AccountStatusTypeRepository : IAccountStatusType
    {
        public Task<bool> CreateAccountStatusTypeAsync(AccountStatusType accountStatusType)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAccountStatusTypeAsync(AccountStatusType accountStatusType)
        {
            throw new NotImplementedException();
        }

        public Task<AccountStatusType> GetAccountStatusTypeAsync(byte? AccountStatusTypeId = null, string? AccountStatusDescription = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AccountStatusType>> GetAccountStatusTypeByAccountStatusTypeIdAsync(byte AccountStatusTypeId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AccountStatusType>> GetAccountStatusTypesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AccountStatusType> UpdateAccountStatusTypeAsync(AccountStatusType AccountStatusType)
        {
            throw new NotImplementedException();
        }
    }
}
