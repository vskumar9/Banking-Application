using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Repository
{
    public class LoanApplicationRepository : ILoanApplication
    {
        public Task<bool> CreateLoanApplicationAsync(LoanApplication loanApplication)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteLoanApplicationAsync(LoanApplication loanApplication)
        {
            throw new NotImplementedException();
        }

        public Task<LoanApplication> GetLoanApplicationAsync(int? LoanId = null, string? CustomerId = null, int? LoanTypeId = null, DateTime? ApplicationDate = null, string? EmployeeId = null, string? LoanStatus = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LoanApplication>> GetLoanApplicationByLoanApplicationIdAsync(int LoanId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LoanApplication>> GetLoanApplicationsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<LoanApplication> UpdateLoanApplicationAsync(LoanApplication loanApplication)
        {
            throw new NotImplementedException();
        }
    }
}
