using Microsoft.EntityFrameworkCore;
using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Repository
{
    public class SavingsInterestRateRepository : ISavingsInterestRate
    {
        private readonly SunBankContext _context;
        private readonly ILogger<SavingsInterestRateRepository> _logger;

        public SavingsInterestRateRepository(SunBankContext context, ILogger<SavingsInterestRateRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Create a new SavingsInterestRate
        public async Task<bool> CreateSavingsInterestRateAsync(SavingsInterestRate savingsInterestRate)
        {
            try
            {
                if (savingsInterestRate == null)
                {
                    throw new ArgumentNullException(nameof(savingsInterestRate), "SavingsInterestRate cannot be null");
                }

                await _context.SavingsInterestRates.AddAsync(savingsInterestRate);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error creating SavingsInterestRate");
                throw new Exception("An error occurred while creating the SavingsInterestRate.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        // Delete a SavingsInterestRate
        public async Task<bool> DeleteSavingsInterestRateAsync(SavingsInterestRate savingsInterestRate)
        {
            try
            {
                if (savingsInterestRate == null)
                {
                    throw new ArgumentNullException(nameof(savingsInterestRate), "SavingsInterestRate cannot be null");
                }

                var existingInterestRate = await _context.SavingsInterestRates.FindAsync(savingsInterestRate.InterestSavingsRateId);
                if (existingInterestRate == null)
                {
                    throw new KeyNotFoundException("SavingsInterestRate not found");
                }

                _context.SavingsInterestRates.Remove(existingInterestRate);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error deleting SavingsInterestRate");
                throw new Exception("An error occurred while deleting the SavingsInterestRate.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        // Get SavingsInterestRate by optional parameters
        public async Task<SavingsInterestRate> GetSavingsInterestRateAsync(byte? InterestSavingsRateId = null, decimal? InterestRateValue = null)
        {
            try
            {
                var query = _context.SavingsInterestRates.AsQueryable();

                if (InterestSavingsRateId.HasValue)
                    query = query.Where(sir => sir.InterestSavingsRateId == InterestSavingsRateId.Value);

                if (InterestRateValue.HasValue)
                    query = query.Where(sir => sir.InterestRateValue == InterestRateValue.Value);

                return await query.Include(sir => sir.Accounts)
                                  .FirstOrDefaultAsync()
                       ?? throw new KeyNotFoundException("SavingsInterestRate not found with the provided criteria.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching SavingsInterestRate");
                throw new Exception("An error occurred while fetching the SavingsInterestRate.", ex);
            }
        }

        // Get SavingsInterestRate by InterestSavingsRateId
        public async Task<IEnumerable<SavingsInterestRate>> GetSavingsInterestRateBySavingsInterestRateIdAsync(byte InterestSavingsRateId)
        {
            try
            {
                if (InterestSavingsRateId == 0)
                {
                    throw new ArgumentException("Invalid InterestSavingsRateId", nameof(InterestSavingsRateId));
                }

                return await _context.SavingsInterestRates
                                     .Where(sir => sir.InterestSavingsRateId == InterestSavingsRateId)
                                     .Include(sir => sir.Accounts)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching SavingsInterestRate by ID");
                throw new Exception("An error occurred while fetching the SavingsInterestRate by ID.", ex);
            }
        }

        // Get all SavingsInterestRates
        public async Task<IEnumerable<SavingsInterestRate>> GetSavingsInterestRatesAsync()
        {
            try
            {
                return await _context.SavingsInterestRates
                                     .Include(sir => sir.Accounts)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all SavingsInterestRates");
                throw new Exception("An error occurred while fetching all SavingsInterestRates.", ex);
            }
        }

        // Update an existing SavingsInterestRate
        public async Task<SavingsInterestRate> UpdateSavingsInterestRateAsync(SavingsInterestRate savingsInterestRate)
        {
            try
            {
                if (savingsInterestRate == null)
                {
                    throw new ArgumentNullException(nameof(savingsInterestRate), "SavingsInterestRate cannot be null");
                }

                var existingInterestRate = await _context.SavingsInterestRates.FindAsync(savingsInterestRate.InterestSavingsRateId);
                if (existingInterestRate == null)
                {
                    throw new KeyNotFoundException("SavingsInterestRate not found");
                }

                _context.Entry(existingInterestRate).CurrentValues.SetValues(savingsInterestRate);
                await _context.SaveChangesAsync();
                return existingInterestRate;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error updating SavingsInterestRate");
                throw new Exception("An error occurred while updating the SavingsInterestRate.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw new Exception("An unexpected error occurred.", ex);
            }
        }
    }
}
