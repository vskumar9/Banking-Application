using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Repository
{
    public class ComplaintRepository : IComplaint
    {
        public Task<bool> CreateComplaintAsync(Complaint complaint)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteComplaintAsync(Complaint complaint)
        {
            throw new NotImplementedException();
        }

        public Task<Complaint> GetComplaintAsync(int? ComplaintId = null, string? CustomerId = null, string? ComplaintTypeId = null, DateTime? ComplaintDate = null, string? ComplaintDescription = null, string? ComplaintStatus = null, string? EmployeeId = null, DateTime? ResolutionDate = null, string? ResolutionComments = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Complaint>> GetComplaintsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Complaint>> GetComplaintsByComplaintIdAsync(int ComplaintId)
        {
            throw new NotImplementedException();
        }

        public Task<Complaint> UpdateComplaintAsync(Complaint complaint)
        {
            throw new NotImplementedException();
        }
    }
}
