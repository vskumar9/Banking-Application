using BankApplicationAPI.Models;

namespace BankApplicationAPI.Interfaces
{
    public interface IComplaintResolution
    {
        Task<IEnumerable<ComplaintResolution>> GetComplaintResolutionsAsync();
        Task<IEnumerable<ComplaintResolution>> GetComplaintResolutionsByComplaintResolutionIdAsync(int accountId);
        Task<ComplaintResolution> UpdateComplaintResolutionAsync(ComplaintResolution complaintResolution);
        Task<Boolean> DeleteComplaintResolutionAsync(ComplaintResolution complaintResolution);
        Task<Boolean> CreateComplaintResolutionAsync(ComplaintResolution complaintResolution);
        Task<ComplaintResolution> GetComplaintResolutionAsync(int? ResolutionId = null,
                                                              int? ComplaintId = null,
                                                              string? ResolutionMethod = null,
                                                              DateTime? ResolutionDate = null,
                                                              string? EmployeeId = null);
    }
}
