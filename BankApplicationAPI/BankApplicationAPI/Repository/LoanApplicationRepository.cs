using Microsoft.EntityFrameworkCore;
using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Repository
{
    public class LoanApplicationRepository : ILoanApplication
    {
        private readonly SunBankContext _context;
        private readonly ILogger<LoanApplicationRepository> _logger;

        public LoanApplicationRepository(SunBankContext context, ILogger<LoanApplicationRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Create a new LoanApplication
        public async Task<bool> CreateLoanApplicationAsync(LoanApplication loanApplication)
        {
            try
            {
                if (loanApplication == null)
                {
                    throw new ArgumentNullException(nameof(loanApplication), "LoanApplication cannot be null");
                }

                await _context.LoanApplications.AddAsync(loanApplication);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error creating loan application");
                throw new Exception("An error occurred while creating the loan application.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        // Delete a LoanApplication
        public async Task<bool> DeleteLoanApplicationAsync(LoanApplication loanApplication)
        {
            try
            {
                if (loanApplication == null)
                {
                    throw new ArgumentNullException(nameof(loanApplication), "LoanApplication cannot be null");
                }

                var existingLoanApplication = await _context.LoanApplications.FindAsync(loanApplication.LoanId);
                if (existingLoanApplication == null)
                {
                    throw new KeyNotFoundException("LoanApplication not found");
                }

                _context.LoanApplications.Remove(existingLoanApplication);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error deleting loan application");
                throw new Exception("An error occurred while deleting the loan application.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        // Get LoanApplication by optional parameters
        public async Task<IEnumerable<LoanApplication>> GetLoanApplicationAsync(int? LoanId = null, string? CustomerId = null, int? LoanTypeId = null, DateTime? ApplicationDate = null, string? EmployeeId = null, string? LoanStatus = null)
        {
            try
            {
                var query = _context.LoanApplications.AsQueryable();

                if (LoanId.HasValue)
                    query = query.Where(la => la.LoanId == LoanId.Value);

                if (!string.IsNullOrEmpty(CustomerId))
                    query = query.Where(la => la.CustomerId == CustomerId);

                if (LoanTypeId.HasValue)
                    query = query.Where(la => la.LoanTypeId == LoanTypeId.Value);

                if (ApplicationDate.HasValue)
                    query = query.Where(la => la.ApplicationDate == ApplicationDate.Value);

                if (!string.IsNullOrEmpty(EmployeeId))
                    query = query.Where(la => la.EmployeeId == EmployeeId);

                if (!string.IsNullOrEmpty(LoanStatus))
                    query = query.Where(la => la.LoanStatus == LoanStatus);

                return await query.Include(la => la.Customer)
                                  .Include(la => la.Employee)
                                  .Include(la => la.LoanType)
                                  .Include(la => la.LoanPaymentSchedules)
                                  .Include(la => la.LoanRepaymentLogs)
                                  .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching loan application");
                throw new Exception("An error occurred while fetching the loan application.", ex);
            }
        }

        // Get LoanApplication by LoanId
        public async Task<LoanApplication> GetLoanApplicationByLoanApplicationIdAsync(int LoanId)
        {
            try
            {
                if (LoanId <= 0)
                {
                    throw new ArgumentException("Invalid LoanId", nameof(LoanId));
                }

                return await _context.LoanApplications
                                     .Where(la => la.LoanId == LoanId)
                                     .Include(la => la.Customer)
                                     .Include(la => la.Employee)
                                     .Include(la => la.LoanType)
                                     .Include(la => la.LoanPaymentSchedules)
                                     .Include(la => la.LoanRepaymentLogs)
                                     .FirstOrDefaultAsync() ?? throw new NullReferenceException();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching loan application by ID");
                throw new Exception("An error occurred while fetching the loan application by ID.", ex);
            }
        }

        // Get all LoanApplications
        public async Task<IEnumerable<LoanApplication>> GetLoanApplicationsAsync()
        {
            try
            {
                return await _context.LoanApplications
                                     .Include(la => la.Customer)
                                     .Include(la => la.Employee)
                                     .Include(la => la.LoanType)
                                     .Include(la => la.LoanPaymentSchedules)
                                     .Include(la => la.LoanRepaymentLogs)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all loan applications");
                throw new Exception("An error occurred while fetching all loan applications.", ex);
            }
        }

        // Update an existing LoanApplication
        public async Task<LoanApplication> UpdateLoanApplicationAsync(LoanApplication loanApplication)
        {
            try
            {
                if (loanApplication == null)
                {
                    throw new ArgumentNullException(nameof(loanApplication), "LoanApplication cannot be null");
                }

                var existingLoanApplication = await _context.LoanApplications.FindAsync(loanApplication.LoanId);
                if (existingLoanApplication == null)
                {
                    throw new KeyNotFoundException("LoanApplication not found");
                }

                _context.Entry(existingLoanApplication).CurrentValues.SetValues(loanApplication);
                await _context.SaveChangesAsync();
                return existingLoanApplication;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error updating loan application");
                throw new Exception("An error occurred while updating the loan application.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw new Exception("An unexpected error occurred.", ex);
            }
        }
    }
}
