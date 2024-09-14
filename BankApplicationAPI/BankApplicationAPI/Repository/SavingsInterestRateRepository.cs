using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Repository
{
    public class SavingsInterestRateRepository : ISavingsInterestRate
    {
        public Task<bool> CreateSavingsInterestRateAsync(SavingsInterestRate savingsInterestRate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteSavingsInterestRateAsync(SavingsInterestRate savingsInterestRate)
        {
            throw new NotImplementedException();
        }

        public Task<SavingsInterestRate> GetSavingsInterestRateAsync(byte? InterestSavingsRateId = null, decimal? InterestRateValue = null, int? PermissionId = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SavingsInterestRate>> GetSavingsInterestRateBySavingsInterestRateIdAsync(int InterestSavingsRateId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SavingsInterestRate>> GetSavingsInterestRatesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SavingsInterestRate> UpdateSavingsInterestRateAsync(SavingsInterestRate savingsInterestRate)
        {
            throw new NotImplementedException();
        }
    }
}
