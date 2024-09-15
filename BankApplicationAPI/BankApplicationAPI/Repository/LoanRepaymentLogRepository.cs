using Microsoft.EntityFrameworkCore;
using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Repository
{
    public class LoanRepaymentLogRepository : ILoanRepaymentLog
    {
        private readonly SunBankContext _context;
        private readonly ILogger<LoanRepaymentLogRepository> _logger;

        public LoanRepaymentLogRepository(SunBankContext context, ILogger<LoanRepaymentLogRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Create a new LoanRepaymentLog
        public async Task<bool> CreateLoanRepaymentLogAsync(LoanRepaymentLog loanRepaymentLog)
        {
            try
            {
                if (loanRepaymentLog == null)
                {
                    throw new ArgumentNullException(nameof(loanRepaymentLog), "LoanRepaymentLog cannot be null");
                }

                await _context.LoanRepaymentLogs.AddAsync(loanRepaymentLog);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error creating loan repayment log");
                throw new Exception("An error occurred while creating the loan repayment log.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        // Delete a LoanRepaymentLog
        public async Task<bool> DeleteLoanRepaymentLogAsync(LoanRepaymentLog loanRepaymentLog)
        {
            try
            {
                if (loanRepaymentLog == null)
                {
                    throw new ArgumentNullException(nameof(loanRepaymentLog), "LoanRepaymentLog cannot be null");
                }

                var existingLog = await _context.LoanRepaymentLogs.FindAsync(loanRepaymentLog.RepaymentId);
                if (existingLog == null)
                {
                    throw new KeyNotFoundException("LoanRepaymentLog not found");
                }

                _context.LoanRepaymentLogs.Remove(existingLog);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error deleting loan repayment log");
                throw new Exception("An error occurred while deleting the loan repayment log.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        // Get LoanRepaymentLog by optional parameters
        public async Task<LoanRepaymentLog> GetLoanRepaymentLogAsync(int? RepaymentId = null, int? LoanId = null, string? EmployeeId = null, string? RepaymentMethod = null)
        {
            try
            {
                var query = _context.LoanRepaymentLogs.AsQueryable();

                if (RepaymentId.HasValue)
                    query = query.Where(lrl => lrl.RepaymentId == RepaymentId.Value);

                if (LoanId.HasValue)
                    query = query.Where(lrl => lrl.LoanId == LoanId.Value);

                if (!string.IsNullOrEmpty(EmployeeId))
                    query = query.Where(lrl => lrl.EmployeeId == EmployeeId);

                if (!string.IsNullOrEmpty(RepaymentMethod))
                    query = query.Where(lrl => lrl.RepaymentMethod == RepaymentMethod);

                return await query.Include(lrl => lrl.Loan)
                                  .Include(lrl => lrl.Employee)
                                  .FirstOrDefaultAsync()
                       ?? throw new KeyNotFoundException("LoanRepaymentLog not found with the provided criteria.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching loan repayment log");
                throw new Exception("An error occurred while fetching the loan repayment log.", ex);
            }
        }

        // Get LoanRepaymentLog by RepaymentId
        public async Task<IEnumerable<LoanRepaymentLog>> GetLoanRepaymentLogByLoanRepaymentLogIdAsync(int RepaymentId)
        {
            try
            {
                if (RepaymentId <= 0)
                {
                    throw new ArgumentException("Invalid RepaymentId", nameof(RepaymentId));
                }

                return await _context.LoanRepaymentLogs
                                     .Where(lrl => lrl.RepaymentId == RepaymentId)
                                     .Include(lrl => lrl.Loan)
                                     .Include(lrl => lrl.Employee)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching loan repayment log by ID");
                throw new Exception("An error occurred while fetching the loan repayment log by ID.", ex);
            }
        }

        // Get all LoanRepaymentLogs
        public async Task<IEnumerable<LoanRepaymentLog>> GetLoanRepaymentLogsAsync()
        {
            try
            {
                return await _context.LoanRepaymentLogs
                                     .Include(lrl => lrl.Loan)
                                     .Include(lrl => lrl.Employee)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all loan repayment logs");
                throw new Exception("An error occurred while fetching all loan repayment logs.", ex);
            }
        }

        // Update an existing LoanRepaymentLog
        public async Task<LoanRepaymentLog> UpdateLoanRepaymentLogAsync(LoanRepaymentLog loanRepaymentLog)
        {
            try
            {
                if (loanRepaymentLog == null)
                {
                    throw new ArgumentNullException(nameof(loanRepaymentLog), "LoanRepaymentLog cannot be null");
                }

                var existingLog = await _context.LoanRepaymentLogs.FindAsync(loanRepaymentLog.RepaymentId);
                if (existingLog == null)
                {
                    throw new KeyNotFoundException("LoanRepaymentLog not found");
                }

                _context.Entry(existingLog).CurrentValues.SetValues(loanRepaymentLog);
                await _context.SaveChangesAsync();
                return existingLog;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error updating loan repayment log");
                throw new Exception("An error occurred while updating the loan repayment log.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw new Exception("An unexpected error occurred.", ex);
            }
        }
    }
}
