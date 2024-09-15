using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Services
{
    public class ComplaintResolutionService
    {
        private readonly IComplaintResolution _complaintResolution;

        public ComplaintResolutionService(IComplaintResolution complaintResolution)
        {
            _complaintResolution = complaintResolution;
        }

        public async Task<bool> CreateComplaintResolutionAsync(ComplaintResolution complaintResolution)
        {
            try
            {
                return await _complaintResolution.CreateComplaintResolutionAsync(complaintResolution);
            }
            catch { throw; }
        }

        public async Task<bool> DeleteComplaintResolutionAsync(ComplaintResolution complaintResolution)
        {
            try
            {
                return await _complaintResolution.DeleteComplaintResolutionAsync(complaintResolution);
            }
            catch { throw; }
        }

        public async Task<ComplaintResolution> GetComplaintResolutionAsync(int? resolutionId = null, int? complaintId = null, string? resolutionMethod = null, DateTime? resolutionDate = null, string? employeeId = null)
        {
            try
            {
                return await _complaintResolution.GetComplaintResolutionAsync(resolutionId, complaintId, resolutionMethod, resolutionDate, employeeId);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<ComplaintResolution>> GetComplaintResolutionsAsync()
        {
            try
            {
                return await _complaintResolution.GetComplaintResolutionsAsync();
            }
            catch { throw; }
        }

        public async Task<IEnumerable<ComplaintResolution>> GetComplaintResolutionsByComplaintResolutionIdAsync(int resolutionId)
        {
            try
            {
                return await _complaintResolution.GetComplaintResolutionsByComplaintResolutionIdAsync(resolutionId);
            }
            catch { throw; }
        }

        public async Task<ComplaintResolution> UpdateComplaintResolutionAsync(ComplaintResolution complaintResolution)
        {
            try
            {
                return await _complaintResolution.UpdateComplaintResolutionAsync(complaintResolution);
            }
            catch { throw; }
        }
    }
}
