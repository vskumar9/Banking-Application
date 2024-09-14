using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Repository
{
    public class ComplaintStatusHistoryRepository : IComplaintStatusHistory
    {
        public Task<bool> CreateComplaintStatusHistoryAsync(ComplaintStatusHistory complaintStatusHistory)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteComplaintStatusHistoryAsync(ComplaintStatusHistory complaintStatusHistory)
        {
            throw new NotImplementedException();
        }

        public Task<ComplaintStatusHistory> GetComplaintStatusHistoryAsync(int? StatusHistoryId = null, int? ComplaintId = null, string? ComplaintStatus = null, DateTime? StatusDate = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ComplaintStatusHistory>> GetComplaintStatusHistoryByComplaintStatusHistoryIdAsync(int StatusHistoryId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ComplaintStatusHistory>> GetComplaintStatusHistorysAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ComplaintStatusHistory> UpdateComplaintStatusHistoryAsync(ComplaintStatusHistory complaintStatusHistory)
        {
            throw new NotImplementedException();
        }
    }
}
