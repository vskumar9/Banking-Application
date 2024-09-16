using BankApplicationAPI.Models;

namespace BankApplicationAPI.Interfaces
{
    public interface IComplaintType
    {
        Task<IEnumerable<ComplaintType>> GetComplaintTypesAsync();
        Task<ComplaintType> GetComplaintTypeByComplaintTypeIdAsync(int ComplaintTypeId);
        Task<ComplaintType> UpdateComplaintTypeAsync(ComplaintType complaintType);
        Task<Boolean> DeleteComplaintTypeAsync(ComplaintType complaintType);
        Task<Boolean> CreateComplaintTypeAsync(ComplaintType complaintType);
        Task<IEnumerable<ComplaintType>> GetComplaintTypeAsync(int? ComplaintTypeId = null,
                                      string? ComplaintTypeName = null,
                                      string? ComplaintTypeDescription = null);
    }
}
