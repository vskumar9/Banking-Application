using BankApplicationAPI.Models;

namespace BankApplicationAPI.Interfaces
{
    public interface IComplaintStatusHistory
    {
        Task<IEnumerable<ComplaintStatusHistory>> GetComplaintStatusHistorysAsync();
        Task<ComplaintStatusHistory> GetComplaintStatusHistoryByComplaintStatusHistoryIdAsync(int StatusHistoryId);
        Task<ComplaintStatusHistory> UpdateComplaintStatusHistoryAsync(ComplaintStatusHistory complaintStatusHistory);
        Task<Boolean> DeleteComplaintStatusHistoryAsync(ComplaintStatusHistory complaintStatusHistory);
        Task<Boolean> CreateComplaintStatusHistoryAsync(ComplaintStatusHistory complaintStatusHistory);
        Task<IEnumerable<ComplaintStatusHistory>> GetComplaintStatusHistoryAsync(int? StatusHistoryId = null,
                                      int? ComplaintId = null,
                                      string? ComplaintStatus = null,
                                      DateTime? StatusDate = null);
    }
}
