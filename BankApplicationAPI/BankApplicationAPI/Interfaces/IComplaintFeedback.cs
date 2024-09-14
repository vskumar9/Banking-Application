using BankApplicationAPI.Models;

namespace BankApplicationAPI.Interfaces
{
    public interface IComplaintFeedback
    {
        Task<IEnumerable<ComplaintFeedback>> GetComplaintFeedbacksAsync();
        Task<IEnumerable<ComplaintFeedback>> GetCComplaintFeedbacksByComplaintFeedbackIdAsync(int FeedbackId);
        Task<ComplaintFeedback> UpdateComplaintFeedbackAsync(ComplaintFeedback Feedback);
        Task<Boolean> DeleteComplaintFeedbackAsync(ComplaintFeedback Feedback);
        Task<Boolean> CreateComplaintFeedbackAsync(ComplaintFeedback Feedback);
        Task<ComplaintFeedback> GetComplaintFeedbackAsync(int? FeedbackId = null,
                                                          string? CustomerId = null,
                                                          string? ComplaintId = null,
                                                          DateTime? FeedbackDate = null,
                                                          byte? FeedbackRating = null,
                                                          string? FeedbackComments = null);
    }
}
