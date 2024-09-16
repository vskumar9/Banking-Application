using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApplicationAPI.Repository
{
    public class ComplaintRepository : IComplaint
    {
        private readonly SunBankContext _context;
        private readonly ILogger<ComplaintRepository> _logger;

        public ComplaintRepository(SunBankContext context, ILogger<ComplaintRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Create a new Complaint
        public async Task<bool> CreateComplaintAsync(Complaint complaint)
        {
            try
            {
                await _context.Complaints.AddAsync(complaint);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Error creating complaint: {ex.Message}", ex);
                throw new Exception("An error occurred while creating the complaint.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred: {ex.Message}", ex);
                throw new Exception("An unexpected error occurred.");
            }
        }

        // Delete an existing Complaint
        public async Task<bool> DeleteComplaintAsync(Complaint complaint)
        {
            try
            {
                _context.Complaints.Remove(complaint);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Error deleting complaint: {ex.Message}", ex);
                throw new Exception("An error occurred while deleting the complaint.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred: {ex.Message}", ex);
                throw new Exception("An unexpected error occurred.");
            }
        }

        // Get Complaint by optional parameters
        public async Task<IEnumerable<Complaint>> GetComplaintAsync(int? ComplaintId = null, string? CustomerId = null, string? ComplaintTypeId = null, DateTime? ComplaintDate = null, string? ComplaintDescription = null, string? ComplaintStatus = null, string? EmployeeId = null, DateTime? ResolutionDate = null, string? ResolutionComments = null)
        {
            try
            {
                var query = _context.Complaints.AsQueryable();

                if (ComplaintId.HasValue)
                    query = query.Where(c => c.ComplaintId == ComplaintId.Value);

                if (!string.IsNullOrEmpty(CustomerId))
                    query = query.Where(c => c.CustomerId == CustomerId);

                if (!string.IsNullOrEmpty(ComplaintTypeId))
                    query = query.Where(c => c.ComplaintTypeId.ToString() == ComplaintTypeId);

                if (ComplaintDate.HasValue)
                    query = query.Where(c => c.ComplaintDate == ComplaintDate.Value);

                if (!string.IsNullOrEmpty(ComplaintDescription))
                    query = query.Where(c => c.ComplaintDescription!.Contains(ComplaintDescription));

                if (!string.IsNullOrEmpty(ComplaintStatus))
                    query = query.Where(c => c.ComplaintStatus == ComplaintStatus);

                if (!string.IsNullOrEmpty(EmployeeId))
                    query = query.Where(c => c.EmployeeId == EmployeeId);

                if (ResolutionDate.HasValue)
                    query = query.Where(c => c.ResolutionDate == ResolutionDate.Value);

                if (!string.IsNullOrEmpty(ResolutionComments))
                    query = query.Where(c => c.ResolutionComments!.Contains(ResolutionComments));

                return await query.Include(c => c.Customer).Include(c => c.ComplaintType)
                                  .Include(c => c.Employee).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching complaint: {ex.Message}", ex);
                throw new Exception("An error occurred while fetching the complaint.");
            }
        }

        // Get all Complaints
        public async Task<IEnumerable<Complaint>> GetComplaintsAsync()
        {
            try
            {
                return await _context.Complaints
                                     .Include(c => c.Customer)
                                     .Include(c => c.ComplaintType)
                                     .Include(c => c.Employee)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching all complaints: {ex.Message}", ex);
                throw new Exception("An error occurred while fetching all complaints.");
            }
        }

        // Get Complaints by ComplaintId
        public async Task<Complaint> GetComplaintsByComplaintIdAsync(int ComplaintId)
        {
            try
            {
                return await _context.Complaints
                                     .Where(c => c.ComplaintId == ComplaintId)
                                     .Include(c => c.Customer)
                                     .Include(c => c.ComplaintType)
                                     .Include(c => c.Employee)
                                     .FirstOrDefaultAsync() ?? throw new NullReferenceException();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching complaint by ID: {ex.Message}", ex);
                throw new Exception("An error occurred while fetching the complaint by ID.");
            }
        }

        // Update an existing Complaint
        public async Task<Complaint> UpdateComplaintAsync(Complaint complaint)
        {
            try
            {
                _context.Complaints.Update(complaint);
                await _context.SaveChangesAsync();
                return complaint;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Error updating complaint: {ex.Message}", ex);
                throw new Exception("An error occurred while updating the complaint.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred: {ex.Message}", ex);
                throw new Exception("An unexpected error occurred.");
            }
        }
    }
}
