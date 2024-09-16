using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Services
{
    public class LoanApplicationService
    {
        private readonly ILoanApplication _loanApplication;

        public LoanApplicationService(ILoanApplication loanApplication)
        {
            _loanApplication = loanApplication;
        }

        public async Task<bool> CreateLoanApplicationAsync(LoanApplication loanApplication)
        {
            try
            {
                return await _loanApplication.CreateLoanApplicationAsync(loanApplication);
            }
            catch { throw; }
        }

        public async Task<bool> DeleteLoanApplicationAsync(LoanApplication loanApplication)
        {
            try
            {
                return await _loanApplication.DeleteLoanApplicationAsync(loanApplication);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<LoanApplication>> GetLoanApplicationAsync(int? LoanId = null, string? CustomerId = null, int? LoanTypeId = null, DateTime? ApplicationDate = null, string? EmployeeId = null, string? LoanStatus = null)
        {
            try
            {
                return await _loanApplication.GetLoanApplicationAsync(LoanId, CustomerId, LoanTypeId, ApplicationDate, EmployeeId, LoanStatus);
            }
            catch { throw; }
        }

        public async Task<LoanApplication> GetLoanApplicationByLoanApplicationIdAsync(int LoanId)
        {
            try
            {
                return await _loanApplication.GetLoanApplicationByLoanApplicationIdAsync(LoanId);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<LoanApplication>> GetLoanApplicationsAsync()
        {
            try
            {
                return await _loanApplication.GetLoanApplicationsAsync();
            }
            catch { throw; }
        }

        public async Task<LoanApplication> UpdateLoanApplicationAsync(LoanApplication loanApplication)
        {
            try
            {
                return await _loanApplication.UpdateLoanApplicationAsync(loanApplication);
            }
            catch { throw; }
        }
    }
}
