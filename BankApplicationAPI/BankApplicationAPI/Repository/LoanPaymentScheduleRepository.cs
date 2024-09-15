using Microsoft.EntityFrameworkCore;
using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Repository
{
    public class LoanPaymentScheduleRepository : ILoanPaymentSchedule
    {
        private readonly SunBankContext _context;
        private readonly ILogger<LoanPaymentScheduleRepository> _logger;

        public LoanPaymentScheduleRepository(SunBankContext context, ILogger<LoanPaymentScheduleRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Create a new LoanPaymentSchedule
        public async Task<bool> CreateLoanPaymentScheduleAsync(LoanPaymentSchedule loanPaymentSchedule)
        {
            try
            {
                if (loanPaymentSchedule == null)
                {
                    throw new ArgumentNullException(nameof(loanPaymentSchedule), "LoanPaymentSchedule cannot be null");
                }

                await _context.LoanPaymentSchedules.AddAsync(loanPaymentSchedule);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error creating loan payment schedule");
                throw new Exception("An error occurred while creating the loan payment schedule.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        // Delete a LoanPaymentSchedule
        public async Task<bool> DeleteLoanPaymentScheduleAsync(LoanPaymentSchedule loanPaymentSchedule)
        {
            try
            {
                if (loanPaymentSchedule == null)
                {
                    throw new ArgumentNullException(nameof(loanPaymentSchedule), "LoanPaymentSchedule cannot be null");
                }

                var existingSchedule = await _context.LoanPaymentSchedules.FindAsync(loanPaymentSchedule.PaymentId);
                if (existingSchedule == null)
                {
                    throw new KeyNotFoundException("LoanPaymentSchedule not found");
                }

                _context.LoanPaymentSchedules.Remove(existingSchedule);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error deleting loan payment schedule");
                throw new Exception("An error occurred while deleting the loan payment schedule.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        // Get LoanPaymentSchedule by optional parameters, filters based on provided parameters
        public async Task<LoanPaymentSchedule> GetLoanPaymentScheduleAsync(int? PaymentId = null, int? LoanId = null, string? PaymentStatus = null, string? LoanStatus = null)
        {
            try
            {
                var query = _context.LoanPaymentSchedules.AsQueryable();
 
                if (PaymentId.HasValue)
                    query = query.Where(lps => lps.PaymentId == PaymentId.Value);

                if (LoanId.HasValue)
                    query = query.Where(lps => lps.LoanId == LoanId.Value);

                if (!string.IsNullOrEmpty(PaymentStatus))
                    query = query.Where(lps => lps.PaymentStatus == PaymentStatus);

                if (!string.IsNullOrEmpty(LoanStatus))
                {
                    query = query.Include(lps => lps.Loan).Where(lps => lps.Loan!.LoanStatus == LoanStatus);
                }

                return await query.FirstOrDefaultAsync() ?? throw new KeyNotFoundException("LoanPaymentSchedule not found with the provided criteria.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching loan payment schedule");
                throw new Exception("An error occurred while fetching the loan payment schedule.", ex);
            }
        }

        // Get LoanPaymentSchedule by PaymentId
        public async Task<IEnumerable<LoanPaymentSchedule>> GetLoanPaymentScheduleByLoanPaymentScheduleIdAsync(int PaymentId)
        {
            try
            {
                if (PaymentId <= 0)
                {
                    throw new ArgumentException("Invalid PaymentId", nameof(PaymentId));
                }

                return await _context.LoanPaymentSchedules
                                     .Where(lps => lps.PaymentId == PaymentId)
                                     .Include(lps => lps.Loan)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching loan payment schedule by ID");
                throw new Exception("An error occurred while fetching the loan payment schedule by ID.", ex);
            }
        }

        // Get all LoanPaymentSchedules
        public async Task<IEnumerable<LoanPaymentSchedule>> GetLoanPaymentSchedulesAsync()
        {
            try
            {
                return await _context.LoanPaymentSchedules
                                     .Include(lps => lps.Loan)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all loan payment schedules");
                throw new Exception("An error occurred while fetching all loan payment schedules.", ex);
            }
        }

        // Update an existing LoanPaymentSchedule
        public async Task<LoanPaymentSchedule> UpdateLoanPaymentScheduleAsync(LoanPaymentSchedule loanPaymentSchedule)
        {
            try
            {
                if (loanPaymentSchedule == null)
                {
                    throw new ArgumentNullException(nameof(loanPaymentSchedule), "LoanPaymentSchedule cannot be null");
                }

                var existingSchedule = await _context.LoanPaymentSchedules.FindAsync(loanPaymentSchedule.PaymentId);
                if (existingSchedule == null)
                {
                    throw new KeyNotFoundException("LoanPaymentSchedule not found");
                }

                _context.Entry(existingSchedule).CurrentValues.SetValues(loanPaymentSchedule);
                await _context.SaveChangesAsync();
                return existingSchedule;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error updating loan payment schedule");
                throw new Exception("An error occurred while updating the loan payment schedule.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw new Exception("An unexpected error occurred.", ex);
            }
        }
    }
}
