using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Services
{
    public class ComplaintTypeService
    {
        private readonly IComplaintType _complaintType;

        public ComplaintTypeService(IComplaintType complaintType)
        {
            _complaintType = complaintType;
        }

        public async Task<bool> CreateComplaintTypeAsync(ComplaintType complaintType)
        {
            try
            {
                return await _complaintType.CreateComplaintTypeAsync(complaintType);
            }
            catch { throw; }
        }

        public async Task<bool> DeleteComplaintTypeAsync(ComplaintType complaintType)
        {
            try
            {
                return await _complaintType.DeleteComplaintTypeAsync(complaintType);
            }
            catch { throw; }
        }

        public async Task<ComplaintType> GetComplaintTypeAsync(int? complaintTypeId = null, string? complaintTypeName = null, string? complaintTypeDescription = null)
        {
            try
            {
                return await _complaintType.GetComplaintTypeAsync(complaintTypeId, complaintTypeName, complaintTypeDescription);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<ComplaintType>> GetComplaintTypeByComplaintTypeIdAsync(int complaintTypeId)
        {
            try
            {
                return await _complaintType.GetComplaintTypeByComplaintTypeIdAsync(complaintTypeId);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<ComplaintType>> GetComplaintTypesAsync()
        {
            try
            {
                return await _complaintType.GetComplaintTypesAsync();
            }
            catch { throw; }
        }

        public async Task<ComplaintType> UpdateComplaintTypeAsync(ComplaintType complaintType)
        {
            try
            {
                return await _complaintType.UpdateComplaintTypeAsync(complaintType);
            }
            catch { throw; }
        }


    }
}
