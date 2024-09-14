using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Repository
{
    public class ComplaintResolutionRepository : IComplaintResolution
    {
        public Task<bool> CreateComplaintResolutionAsync(ComplaintResolution complaintResolution)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteComplaintResolutionAsync(ComplaintResolution complaintResolution)
        {
            throw new NotImplementedException();
        }

        public Task<ComplaintResolution> GetComplaintResolutionAsync(int? ResolutionId = null, int? ComplaintId = null, string? ResolutionMethod = null, DateTime? ResolutionDate = null, string? EmployeeId = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ComplaintResolution>> GetComplaintResolutionsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ComplaintResolution>> GetComplaintResolutionsByComplaintResolutionIdAsync(int accountId)
        {
            throw new NotImplementedException();
        }

        public Task<ComplaintResolution> UpdateComplaintResolutionAsync(ComplaintResolution complaintResolution)
        {
            throw new NotImplementedException();
        }
    }
}
