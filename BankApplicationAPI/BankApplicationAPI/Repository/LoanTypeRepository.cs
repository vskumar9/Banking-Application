using Microsoft.EntityFrameworkCore;
using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Repository
{
    public class LoanTypeRepository : ILoanType
    {
        private readonly SunBankContext _context;
        private readonly ILogger<LoanTypeRepository> _logger;

        public LoanTypeRepository(SunBankContext context, ILogger<LoanTypeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Create a new LoanType
        public async Task<bool> CreateLoanTypeAsync(LoanType loanType)
        {
            try
            {
                if (loanType == null)
                {
                    throw new ArgumentNullException(nameof(loanType), "LoanType cannot be null");
                }

                await _context.LoanTypes.AddAsync(loanType);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error creating loan type");
                throw new Exception("An error occurred while creating the loan type.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        // Delete a LoanType
        public async Task<bool> DeleteLoanTypeAsync(LoanType loanType)
        {
            try
            {
                if (loanType == null)
                {
                    throw new ArgumentNullException(nameof(loanType), "LoanType cannot be null");
                }

                var existingLoanType = await _context.LoanTypes.FindAsync(loanType.LoanTypeId);
                if (existingLoanType == null)
                {
                    throw new KeyNotFoundException("LoanType not found");
                }

                _context.LoanTypes.Remove(existingLoanType);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error deleting loan type");
                throw new Exception("An error occurred while deleting the loan type.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        // Get LoanType by optional parameters
        public async Task<IEnumerable<LoanType>> GetLoanTypeAsync(int? LoanTypeId = null, string? LoanTypeName = null)
        {
            try
            {
                var query = _context.LoanTypes.AsQueryable();

                if (LoanTypeId.HasValue)
                    query = query.Where(lt => lt.LoanTypeId == LoanTypeId.Value);

                if (!string.IsNullOrEmpty(LoanTypeName))
                    query = query.Where(lt => lt.LoanTypeName == LoanTypeName);

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching loan type");
                throw new Exception("An error occurred while fetching the loan type.", ex);
            }
        }

        // Get LoanType by LoanTypeId
        public async Task<LoanType> GetLoanTypeByLoanTypeIdAsync(int LoanTypeId)
        {
            try
            {
                if (LoanTypeId <= 0)
                {
                    throw new ArgumentException("Invalid LoanTypeId", nameof(LoanTypeId));
                }

                return await _context.LoanTypes
                                     .Where(lt => lt.LoanTypeId == LoanTypeId)
                                     .FirstOrDefaultAsync() ?? throw new NullReferenceException();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching loan type by ID");
                throw new Exception("An error occurred while fetching the loan type by ID.", ex);
            }
        }

        // Get all LoanTypes
        public async Task<IEnumerable<LoanType>> GetLoanTypesAsync()
        {
            try
            {
                return await _context.LoanTypes.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all loan types");
                throw new Exception("An error occurred while fetching all loan types.", ex);
            }
        }

        // Update an existing LoanType
        public async Task<LoanType> UpdateLoanTypeAsync(LoanType loanType)
        {
            try
            {
                if (loanType == null)
                {
                    throw new ArgumentNullException(nameof(loanType), "LoanType cannot be null");
                }

                var existingLoanType = await _context.LoanTypes.FindAsync(loanType.LoanTypeId);
                if (existingLoanType == null)
                {
                    throw new KeyNotFoundException("LoanType not found");
                }

                _context.Entry(existingLoanType).CurrentValues.SetValues(loanType);
                await _context.SaveChangesAsync();
                return existingLoanType;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error updating loan type");
                throw new Exception("An error occurred while updating the loan type.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw new Exception("An unexpected error occurred.", ex);
            }
        }
    }
}
