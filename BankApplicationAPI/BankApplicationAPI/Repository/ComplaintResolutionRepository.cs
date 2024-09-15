using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApplicationAPI.Repository
{
    public class ComplaintResolutionRepository : IComplaintResolution
    {
        private readonly SunBankContext _context;
        private readonly ILogger<ComplaintResolutionRepository> _logger;

        public ComplaintResolutionRepository(SunBankContext context, ILogger<ComplaintResolutionRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Create a new ComplaintResolution
        public async Task<bool> CreateComplaintResolutionAsync(ComplaintResolution complaintResolution)
        {
            try
            {
                await _context.ComplaintResolutions.AddAsync(complaintResolution);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Error creating complaint resolution: {ex.Message}", ex);
                throw new Exception("An error occurred while creating the complaint resolution.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred: {ex.Message}", ex);
                throw new Exception("An unexpected error occurred.");
            }
        }

        // Delete an existing ComplaintResolution
        public async Task<bool> DeleteComplaintResolutionAsync(ComplaintResolution complaintResolution)
        {
            try
            {
                _context.ComplaintResolutions.Remove(complaintResolution);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Error deleting complaint resolution: {ex.Message}", ex);
                throw new Exception("An error occurred while deleting the complaint resolution.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred: {ex.Message}", ex);
                throw new Exception("An unexpected error occurred.");
            }
        }

        // Get ComplaintResolution by optional parameters
        public async Task<ComplaintResolution> GetComplaintResolutionAsync(int? resolutionId = null, int? complaintId = null, string? resolutionMethod = null, DateTime? resolutionDate = null, string? employeeId = null)
        {
            try
            {
                var query = _context.ComplaintResolutions.AsQueryable();

                if (resolutionId.HasValue)
                    query = query.Where(r => r.ResolutionId == resolutionId.Value);

                if (complaintId.HasValue)
                    query = query.Where(r => r.ComplaintId == complaintId.Value);

                if (!string.IsNullOrEmpty(resolutionMethod))
                    query = query.Where(r => r.ResolutionMethod == resolutionMethod);

                if (resolutionDate.HasValue)
                    query = query.Where(r => r.ResolutionDate == resolutionDate.Value);

                if (!string.IsNullOrEmpty(employeeId))
                    query = query.Where(r => r.EmployeeId == employeeId);

                return await query.Include(r => r.Complaint).Include(r => r.Employee)
                                  .FirstOrDefaultAsync() ?? throw new NullReferenceException();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching complaint resolution: {ex.Message}", ex);
                throw new Exception("An error occurred while fetching the complaint resolution.");
            }
        }

        // Get all ComplaintResolutions
        public async Task<IEnumerable<ComplaintResolution>> GetComplaintResolutionsAsync()
        {
            try
            {
                return await _context.ComplaintResolutions
                                     .Include(r => r.Complaint)
                                     .Include(r => r.Employee)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching all complaint resolutions: {ex.Message}", ex);
                throw new Exception("An error occurred while fetching all complaint resolutions.");
            }
        }

        // Get ComplaintResolutions by ResolutionId
        public async Task<IEnumerable<ComplaintResolution>> GetComplaintResolutionsByComplaintResolutionIdAsync(int resolutionId)
        {
            try
            {
                return await _context.ComplaintResolutions
                                     .Where(r => r.ResolutionId == resolutionId)
                                     .Include(r => r.Complaint)
                                     .Include(r => r.Employee)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching complaint resolution by ID: {ex.Message}", ex);
                throw new Exception("An error occurred while fetching the complaint resolution by ID.");
            }
        }

        // Update an existing ComplaintResolution
        public async Task<ComplaintResolution> UpdateComplaintResolutionAsync(ComplaintResolution complaintResolution)
        {
            try
            {
                _context.ComplaintResolutions.Update(complaintResolution);
                await _context.SaveChangesAsync();
                return complaintResolution;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Error updating complaint resolution: {ex.Message}", ex);
                throw new Exception("An error occurred while updating the complaint resolution.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred: {ex.Message}", ex);
                throw new Exception("An unexpected error occurred.");
            }
        }
    }
}
