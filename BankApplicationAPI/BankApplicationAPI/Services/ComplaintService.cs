using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Services
{
    public class ComplaintService
    {
        private readonly IComplaint _complaint;

        public ComplaintService(IComplaint complaint)
        {
            _complaint = complaint;
        }

        public async Task<bool> CreateComplaintAsync(Complaint complaint)
        {
            try
            {
                return await _complaint.CreateComplaintAsync(complaint);
            }
            catch { throw; }
        }

        public async Task<bool> DeleteComplaintAsync(Complaint complaint)
        {
            try
            {
                return await _complaint.DeleteComplaintAsync(complaint);
            }
            catch { throw; }
        }

        public async Task<Complaint> GetComplaintAsync(int? ComplaintId = null, string? CustomerId = null, string? ComplaintTypeId = null, DateTime? ComplaintDate = null, string? ComplaintDescription = null, string? ComplaintStatus = null, string? EmployeeId = null, DateTime? ResolutionDate = null, string? ResolutionComments = null)
        {
            try
            {
                return await _complaint.GetComplaintAsync(ComplaintId, CustomerId, ComplaintTypeId, ComplaintDate, ComplaintDescription, ComplaintStatus, EmployeeId, ResolutionDate, ResolutionComments);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<Complaint>> GetComplaintsAsync()
        {
            try
            {
                return await _complaint.GetComplaintsAsync();
            }
            catch { throw; }
        }

        public async Task<IEnumerable<Complaint>> GetComplaintsByComplaintIdAsync(int ComplaintId)
        {
            try
            {
                return await _complaint.GetComplaintsByComplaintIdAsync(ComplaintId);
            }
            catch { throw; }
        }

        public async Task<Complaint> UpdateComplaintAsync(Complaint complaint)
        {
            try
            {
                return await _complaint.UpdateComplaintAsync(complaint);
            }
            catch { throw; }
        }
    }
}
