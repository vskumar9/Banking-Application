using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Repository
{
    public class ComplaintFeedbackRepository : IComplaintFeedback
    {
        public Task<bool> CreateComplaintFeedbackAsync(ComplaintFeedback Feedback)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteComplaintFeedbackAsync(ComplaintFeedback Feedback)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ComplaintFeedback>> GetCComplaintFeedbacksByComplaintFeedbackIdAsync(int FeedbackId)
        {
            throw new NotImplementedException();
        }

        public Task<ComplaintFeedback> GetComplaintFeedbackAsync(int? FeedbackId = null, string? CustomerId = null, string? ComplaintId = null, DateTime? FeedbackDate = null, byte? FeedbackRating = null, string? FeedbackComments = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ComplaintFeedback>> GetComplaintFeedbacksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ComplaintFeedback> UpdateComplaintFeedbackAsync(ComplaintFeedback Feedback)
        {
            throw new NotImplementedException();
        }
    }
}
