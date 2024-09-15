using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Services
{
    public class SavingsInterestRateService
    {
        private readonly ISavingsInterestRate _savingsInterestRate;

        public SavingsInterestRateService(ISavingsInterestRate savingsInterestRate)
        {
            _savingsInterestRate = savingsInterestRate;
        }

        public async Task<bool> CreateSavingsInterestRateAsync(SavingsInterestRate savingsInterestRate)
        {
            try
            {
                return await _savingsInterestRate.CreateSavingsInterestRateAsync(savingsInterestRate);
            }
            catch { throw; }
        }

            
        public async Task<bool> DeleteSavingsInterestRateAsync(SavingsInterestRate savingsInterestRate)
        {
            try
            {
                return await _savingsInterestRate.DeleteSavingsInterestRateAsync(savingsInterestRate);
            }
            catch { throw; }
        }

        public async Task<SavingsInterestRate> GetSavingsInterestRateAsync(byte? InterestSavingsRateId = null, decimal? InterestRateValue = null)
        {
            try
            {
                return await _savingsInterestRate.GetSavingsInterestRateAsync(InterestSavingsRateId, InterestRateValue);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<SavingsInterestRate>> GetSavingsInterestRateBySavingsInterestRateIdAsync(byte InterestSavingsRateId)
        {
            try
            {
                return await _savingsInterestRate.GetSavingsInterestRateBySavingsInterestRateIdAsync(InterestSavingsRateId);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<SavingsInterestRate>> GetSavingsInterestRatesAsync()
        {
            try
            {
                return await _savingsInterestRate.GetSavingsInterestRatesAsync();
            }
            catch { throw; }
        }

        public async Task<SavingsInterestRate> UpdateSavingsInterestRateAsync(SavingsInterestRate savingsInterestRate)
        {
            try
            {
                return await _savingsInterestRate.UpdateSavingsInterestRateAsync(savingsInterestRate);
            }
            catch { throw; }
        }
    }
}
