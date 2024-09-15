using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApplicationAPI.Repository
{
    public class ComplaintStatusHistoryRepository : IComplaintStatusHistory
    {
        private readonly SunBankContext _context;
        private readonly ILogger<ComplaintStatusHistoryRepository> _logger;

        public ComplaintStatusHistoryRepository(SunBankContext context, ILogger<ComplaintStatusHistoryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Create a new ComplaintStatusHistory
        public async Task<bool> CreateComplaintStatusHistoryAsync(ComplaintStatusHistory complaintStatusHistory)
        {
            try
            {
                await _context.ComplaintStatusHistories.AddAsync(complaintStatusHistory);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Error creating complaint status history: {ex.Message}", ex);
                throw new Exception("An error occurred while creating the complaint status history.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred: {ex.Message}", ex);
                throw new Exception("An unexpected error occurred.");
            }
        }

        // Delete an existing ComplaintStatusHistory
        public async Task<bool> DeleteComplaintStatusHistoryAsync(ComplaintStatusHistory complaintStatusHistory)
        {
            try
            {
                _context.ComplaintStatusHistories.Remove(complaintStatusHistory);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Error deleting complaint status history: {ex.Message}", ex);
                throw new Exception("An error occurred while deleting the complaint status history.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred: {ex.Message}", ex);
                throw new Exception("An unexpected error occurred.");
            }
        }

        // Get ComplaintStatusHistory by optional parameters
        public async Task<ComplaintStatusHistory> GetComplaintStatusHistoryAsync(int? statusHistoryId = null, int? complaintId = null, string? complaintStatus = null, DateTime? statusDate = null)
        {
            try
            {
                var query = _context.ComplaintStatusHistories.AsQueryable();

                if (statusHistoryId.HasValue)
                    query = query.Where(c => c.StatusHistoryId == statusHistoryId.Value);

                if (complaintId.HasValue)
                    query = query.Where(c => c.ComplaintId == complaintId.Value);

                if (!string.IsNullOrEmpty(complaintStatus))
                    query = query.Where(c => c.ComplaintStatus == complaintStatus);

                if (statusDate.HasValue)
                    query = query.Where(c => c.StatusDate == statusDate.Value);

                return await query.Include(c => c.Complaint)
                                  .FirstOrDefaultAsync() ?? throw new NullReferenceException();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching complaint status history: {ex.Message}", ex);
                throw new Exception("An error occurred while fetching the complaint status history.");
            }
        }

        // Get ComplaintStatusHistory by StatusHistoryId
        public async Task<IEnumerable<ComplaintStatusHistory>> GetComplaintStatusHistoryByComplaintStatusHistoryIdAsync(int statusHistoryId)
        {
            try
            {
                return await _context.ComplaintStatusHistories
                                     .Where(c => c.StatusHistoryId == statusHistoryId)
                                     .Include(c => c.Complaint)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching complaint status history by ID: {ex.Message}", ex);
                throw new Exception("An error occurred while fetching the complaint status history by ID.");
            }
        }

        // Get all ComplaintStatusHistories
        public async Task<IEnumerable<ComplaintStatusHistory>> GetComplaintStatusHistorysAsync()
        {
            try
            {
                return await _context.ComplaintStatusHistories
                                     .Include(c => c.Complaint)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching all complaint status histories: {ex.Message}", ex);
                throw new Exception("An error occurred while fetching all complaint status histories.");
            }
        }

        // Update an existing ComplaintStatusHistory
        public async Task<ComplaintStatusHistory> UpdateComplaintStatusHistoryAsync(ComplaintStatusHistory complaintStatusHistory)
        {
            try
            {
                _context.ComplaintStatusHistories.Update(complaintStatusHistory);
                await _context.SaveChangesAsync();
                return complaintStatusHistory;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Error updating complaint status history: {ex.Message}", ex);
                throw new Exception("An error occurred while updating the complaint status history.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred: {ex.Message}", ex);
                throw new Exception("An unexpected error occurred.");
            }
        }
    }
}
