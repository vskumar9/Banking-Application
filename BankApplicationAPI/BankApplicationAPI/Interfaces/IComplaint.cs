using BankApplicationAPI.Models;

namespace BankApplicationAPI.Interfaces
{
    public interface IComplaint
    {
        Task<IEnumerable<Complaint>> GetComplaintsAsync();
        Task<IEnumerable<Complaint>> GetComplaintsByComplaintIdAsync(int ComplaintId);
        Task<Complaint> UpdateComplaintAsync(Complaint complaint);
        Task<Boolean> DeleteComplaintAsync(Complaint complaint);
        Task<Boolean> CreateComplaintAsync(Complaint complaint);
        Task<Complaint> GetComplaintAsync(int? ComplaintId = null,
                                      string? CustomerId = null,
                                      string? ComplaintTypeId = null,
                                      DateTime? ComplaintDate = null,
                                      string? ComplaintDescription = null,
                                      string? ComplaintStatus = null,
                                      string? EmployeeId = null,
                                      DateTime? ResolutionDate = null,
                                      string? ResolutionComments = null);
    }
}
