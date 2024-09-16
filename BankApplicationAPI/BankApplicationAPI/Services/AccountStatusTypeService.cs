using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Services
{
    public class AccountStatusTypeService
    {
        private readonly IAccountStatusType _accountStatusType;

        public AccountStatusTypeService(IAccountStatusType accountStatusType)
        {
            _accountStatusType = accountStatusType;
        }

        public async Task<bool> CreateAccountStatusTypeAsync(AccountStatusType accountStatusType)
        {
            try
            {
                if (accountStatusType == null) return false;
                return await _accountStatusType.CreateAccountStatusTypeAsync(accountStatusType);
            }
            catch { throw; }
        }

        public async Task<bool> DeleteAccountStatusTypeAsync(AccountStatusType accountStatusType)
        {
            try
            {
                if (accountStatusType == null) return false;
                return await _accountStatusType.DeleteAccountStatusTypeAsync(accountStatusType);
            }
            catch { throw; };
        }

        public async Task<IEnumerable<AccountStatusType>> GetAccountStatusTypeAsync(byte? AccountStatusTypeId = null, string? AccountStatusDescription = null)
        {
            try
            {
                return await _accountStatusType.GetAccountStatusTypeAsync(AccountStatusTypeId, AccountStatusDescription);
            }
            catch { throw; }
        }

        public async Task<AccountStatusType> GetAccountStatusTypeByAccountStatusTypeIdAsync(byte AccountStatusTypeId)
        {
            try
            {
                return await _accountStatusType.GetAccountStatusTypeByAccountStatusTypeIdAsync(AccountStatusTypeId);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<AccountStatusType>> GetAccountStatusTypesAsync()
        {
            try
            {
                return await _accountStatusType.GetAccountStatusTypesAsync();
            }
            catch { throw; }
        }

        public async Task<AccountStatusType> UpdateAccountStatusTypeAsync(AccountStatusType accountStatusType)
        {
            try
            {
                return await _accountStatusType.UpdateAccountStatusTypeAsync(accountStatusType);
            }
            catch { throw; }
        }
    }
}
