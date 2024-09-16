using BankApplicationAPI.Models;

namespace BankApplicationAPI.Interfaces
{
    public interface ISavingsInterestRate
    {
        Task<IEnumerable<SavingsInterestRate>> GetSavingsInterestRatesAsync();
        Task<SavingsInterestRate> GetSavingsInterestRateBySavingsInterestRateIdAsync(byte InterestSavingsRateId);
        Task<SavingsInterestRate> UpdateSavingsInterestRateAsync(SavingsInterestRate savingsInterestRate);
        Task<Boolean> DeleteSavingsInterestRateAsync(SavingsInterestRate savingsInterestRate);
        Task<Boolean> CreateSavingsInterestRateAsync(SavingsInterestRate savingsInterestRate);
        Task<IEnumerable<SavingsInterestRate>> GetSavingsInterestRateAsync(byte? InterestSavingsRateId = null,
                                                    decimal? InterestRateValue = null);
    }
}
