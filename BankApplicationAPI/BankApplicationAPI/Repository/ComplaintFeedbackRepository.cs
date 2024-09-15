using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApplicationAPI.Repository
{
    public class ComplaintFeedbackRepository : IComplaintFeedback
    {
        private readonly SunBankContext _context;
        private readonly ILogger<ComplaintFeedbackRepository> _logger;

        public ComplaintFeedbackRepository(SunBankContext context, ILogger<ComplaintFeedbackRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Create a new ComplaintFeedback
        public async Task<bool> CreateComplaintFeedbackAsync(ComplaintFeedback feedback)
        {
            try
            {
                await _context.ComplaintFeedbacks.AddAsync(feedback);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Error creating complaint feedback: {ex.Message}", ex);
                throw new Exception("An error occurred while creating the complaint feedback.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred: {ex.Message}", ex);
                throw new Exception("An unexpected error occurred.");
            }
        }

        // Delete an existing ComplaintFeedback
        public async Task<bool> DeleteComplaintFeedbackAsync(ComplaintFeedback feedback)
        {
            try
            {
                _context.ComplaintFeedbacks.Remove(feedback);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Error deleting complaint feedback: {ex.Message}", ex);
                throw new Exception("An error occurred while deleting the complaint feedback.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred: {ex.Message}", ex);
                throw new Exception("An unexpected error occurred.");
            }
        }

        // Get ComplaintFeedback by optional parameters
        public async Task<ComplaintFeedback> GetComplaintFeedbackAsync(int? feedbackId = null, string? customerId = null, string? complaintId = null, DateTime? feedbackDate = null, byte? feedbackRating = null, string? feedbackComments = null)
        {
            try
            {
                var query = _context.ComplaintFeedbacks.AsQueryable();

                if (feedbackId.HasValue)
                    query = query.Where(f => f.FeedbackId == feedbackId.Value);

                if (!string.IsNullOrEmpty(customerId))
                    query = query.Where(f => f.CustomerId == customerId);

                if (!string.IsNullOrEmpty(complaintId))
                    query = query.Where(f => f.ComplaintId.ToString() == complaintId);

                if (feedbackDate.HasValue)
                    query = query.Where(f => f.FeedbackDate == feedbackDate.Value);

                if (feedbackRating.HasValue)
                    query = query.Where(f => f.FeedbackRating == feedbackRating);

                if (!string.IsNullOrEmpty(feedbackComments))
                    query = query.Where(f => f.FeedbackComments!.Contains(feedbackComments));

                return await query.Include(f => f.Customer).Include(f => f.Complaint)
                                  .FirstOrDefaultAsync() ?? throw new NullReferenceException();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching complaint feedback: {ex.Message}", ex);
                throw new Exception("An error occurred while fetching the complaint feedback.");
            }
        }

        // Get ComplaintFeedback by FeedbackId
        public async Task<IEnumerable<ComplaintFeedback>> GetCComplaintFeedbacksByComplaintFeedbackIdAsync(int feedbackId)
        {
            try
            {
                return await _context.ComplaintFeedbacks
                                     .Where(f => f.FeedbackId == feedbackId)
                                     .Include(f => f.Customer)
                                     .Include(f => f.Complaint)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching complaint feedback by ID: {ex.Message}", ex);
                throw new Exception("An error occurred while fetching the complaint feedback by ID.");
            }
        }

        // Get all ComplaintFeedbacks
        public async Task<IEnumerable<ComplaintFeedback>> GetComplaintFeedbacksAsync()
        {
            try
            {
                return await _context.ComplaintFeedbacks
                                     .Include(f => f.Customer)
                                     .Include(f => f.Complaint)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching all complaint feedbacks: {ex.Message}", ex);
                throw new Exception("An error occurred while fetching all complaint feedbacks.");
            }
        }

        // Update an existing ComplaintFeedback
        public async Task<ComplaintFeedback> UpdateComplaintFeedbackAsync(ComplaintFeedback feedback)
        {
            try
            {
                _context.ComplaintFeedbacks.Update(feedback);
                await _context.SaveChangesAsync();
                return feedback;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Error updating complaint feedback: {ex.Message}", ex);
                throw new Exception("An error occurred while updating the complaint feedback.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred: {ex.Message}", ex);
                throw new Exception("An unexpected error occurred.");
            }
        }
    }
}
