using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Repository
{
    public class ComplaintTypeRepository : IComplaintType
    {
        public Task<bool> CreateComplaintTypeAsync(ComplaintType complaintType)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteComplaintTypeAsync(ComplaintType complaintType)
        {
            throw new NotImplementedException();
        }

        public Task<ComplaintType> GetComplaintTypeAsync(int? ComplaintTypeId = null, string? ComplaintTypeName = null, string? ComplaintTypeDescription = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ComplaintType>> GetComplaintTypeByComplaintTypeIdAsync(int ComplaintTypeId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ComplaintType>> GetComplaintTypesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ComplaintType> UpdateComplaintTypeAsync(ComplaintType complaintType)
        {
            throw new NotImplementedException();
        }
    }
}
