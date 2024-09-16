using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Services
{
    public class ComplaintFeedbackService
    {
        private readonly IComplaintFeedback _complaintFeedback;

        public ComplaintFeedbackService(IComplaintFeedback complaintFeedback)
        {
            _complaintFeedback = complaintFeedback;
        }

        public async Task<bool> CreateComplaintFeedbackAsync(ComplaintFeedback feedback)
        {
            try
            {
                return await _complaintFeedback.CreateComplaintFeedbackAsync(feedback);
            }
            catch { throw; }
        }

        public async Task<bool> DeleteComplaintFeedbackAsync(ComplaintFeedback feedback)
        {
            try
            {
                return await _complaintFeedback.DeleteComplaintFeedbackAsync(feedback);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<ComplaintFeedback>> GetComplaintFeedbackAsync(int? feedbackId = null, string? customerId = null, string? complaintId = null, DateTime? feedbackDate = null, byte? feedbackRating = null, string? feedbackComments = null)
        {
            try
            {
                return await _complaintFeedback.GetComplaintFeedbackAsync(feedbackId, customerId, complaintId, feedbackDate,feedbackRating, feedbackComments);
            }
            catch { throw; }
        }

        public async Task<ComplaintFeedback> GetCComplaintFeedbacksByComplaintFeedbackIdAsync(int feedbackId)
        {
            try
            {
                return await _complaintFeedback.GetCComplaintFeedbacksByComplaintFeedbackIdAsync(feedbackId);
            }
            catch { throw; };
        }

        public async Task<IEnumerable<ComplaintFeedback>> GetComplaintFeedbacksAsync()
        {
            try
            {
                return await _complaintFeedback.GetComplaintFeedbacksAsync();
            }
            catch { throw; }
        }

        public async Task<ComplaintFeedback> UpdateComplaintFeedbackAsync(ComplaintFeedback feedback)
        {
            try
            {
                return await _complaintFeedback.UpdateComplaintFeedbackAsync(feedback);
            }
            catch { throw; }
        }

    }
}
