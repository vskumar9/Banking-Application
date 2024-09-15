using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BankApplicationAPI.Repository
{
    public class ComplaintTypeRepository : IComplaintType
    {
        private readonly SunBankContext _context;
        private readonly ILogger<ComplaintTypeRepository> _logger;

        public ComplaintTypeRepository(SunBankContext context, ILogger<ComplaintTypeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Create a new ComplaintType
        public async Task<bool> CreateComplaintTypeAsync(ComplaintType complaintType)
        {
            try
            {
                await _context.ComplaintTypes.AddAsync(complaintType);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Error creating complaint type: {ex.Message}", ex);
                throw new Exception("An error occurred while creating the complaint type.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred: {ex.Message}", ex);
                throw new Exception("An unexpected error occurred.");
            }
        }

        // Delete an existing ComplaintType
        public async Task<bool> DeleteComplaintTypeAsync(ComplaintType complaintType)
        {
            try
            {
                _context.ComplaintTypes.Remove(complaintType);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Error deleting complaint type: {ex.Message}", ex);
                throw new Exception("An error occurred while deleting the complaint type.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred: {ex.Message}", ex);
                throw new Exception("An unexpected error occurred.");
            }
        }

        // Get ComplaintType by optional parameters
        public async Task<ComplaintType> GetComplaintTypeAsync(int? complaintTypeId = null, string? complaintTypeName = null, string? complaintTypeDescription = null)
        {
            try
            {
                var query = _context.ComplaintTypes.AsQueryable();

                if (complaintTypeId.HasValue)
                    query = query.Where(ct => ct.ComplaintTypeId == complaintTypeId.Value);

                if (!string.IsNullOrEmpty(complaintTypeName))
                    query = query.Where(ct => ct.ComplaintTypeName == complaintTypeName);

                if (!string.IsNullOrEmpty(complaintTypeDescription))
                    query = query.Where(ct => ct.ComplaintTypeDescription == complaintTypeDescription);

                return await query.FirstOrDefaultAsync() ?? throw new NullReferenceException();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching complaint type: {ex.Message}", ex);
                throw new Exception("An error occurred while fetching the complaint type.");
            }
        }

        // Get ComplaintType by ComplaintTypeId
        public async Task<IEnumerable<ComplaintType>> GetComplaintTypeByComplaintTypeIdAsync(int complaintTypeId)
        {
            try
            {
                return await _context.ComplaintTypes
                                     .Where(ct => ct.ComplaintTypeId == complaintTypeId)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching complaint type by ID: {ex.Message}", ex);
                throw new Exception("An error occurred while fetching the complaint type by ID.");
            }
        }

        // Get all ComplaintTypes
        public async Task<IEnumerable<ComplaintType>> GetComplaintTypesAsync()
        {
            try
            {
                return await _context.ComplaintTypes.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching all complaint types: {ex.Message}", ex);
                throw new Exception("An error occurred while fetching all complaint types.");
            }
        }

        // Update an existing ComplaintType
        public async Task<ComplaintType> UpdateComplaintTypeAsync(ComplaintType complaintType)
        {
            try
            {
                _context.ComplaintTypes.Update(complaintType);
                await _context.SaveChangesAsync();
                return complaintType;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Error updating complaint type: {ex.Message}", ex);
                throw new Exception("An error occurred while updating the complaint type.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred: {ex.Message}", ex);
                throw new Exception("An unexpected error occurred.");
            }
        }
    }
}
