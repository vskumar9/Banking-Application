using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Services
{
    public class ComplaintStatusHistoryService
    {
        private readonly IComplaintStatusHistory _complaintStatusHistory;

        public ComplaintStatusHistoryService(IComplaintStatusHistory complaintStatusHistory)
        {
            _complaintStatusHistory = complaintStatusHistory;
        }

        public async Task<bool> CreateComplaintStatusHistoryAsync(ComplaintStatusHistory complaintStatusHistory)
        {
            try
            {
                return await _complaintStatusHistory.CreateComplaintStatusHistoryAsync(complaintStatusHistory);
            }
            catch { throw; }
        }

        public async Task<bool> DeleteComplaintStatusHistoryAsync(ComplaintStatusHistory complaintStatusHistory)
        {
            try
            {
                return await _complaintStatusHistory.DeleteComplaintStatusHistoryAsync(complaintStatusHistory);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<ComplaintStatusHistory>> GetComplaintStatusHistoryAsync(int? statusHistoryId = null, int? complaintId = null, string? complaintStatus = null, DateTime? statusDate = null)
        {
            try
            {
                return await _complaintStatusHistory.GetComplaintStatusHistoryAsync(statusHistoryId, complaintId, complaintStatus, statusDate);
            }
            catch { throw; }
        }

        public async Task<ComplaintStatusHistory> GetComplaintStatusHistoryByComplaintStatusHistoryIdAsync(int statusHistoryId)
        {
            try
            {
                return await _complaintStatusHistory.GetComplaintStatusHistoryByComplaintStatusHistoryIdAsync(statusHistoryId);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<ComplaintStatusHistory>> GetComplaintStatusHistorysAsync()
        {
            try
            {
                return await _complaintStatusHistory.GetComplaintStatusHistorysAsync();
            }
            catch { throw; }
        }

        public async Task<ComplaintStatusHistory> UpdateComplaintStatusHistoryAsync(ComplaintStatusHistory complaintStatusHistory)
        {
            try
            {
                return await _complaintStatusHistory.UpdateComplaintStatusHistoryAsync(complaintStatusHistory);
            }
            catch { throw; }
        }
    }
}
