using BankApplicationAPI.Models;

namespace BankApplicationAPI.Interfaces
{
    public interface ILoanApplication
    {
        Task<IEnumerable<LoanApplication>> GetLoanApplicationsAsync();
        Task<IEnumerable<LoanApplication>> GetLoanApplicationByLoanApplicationIdAsync(int LoanId);
        Task<LoanApplication> UpdateLoanApplicationAsync(LoanApplication loanApplication);
        Task<Boolean> DeleteLoanApplicationAsync(LoanApplication loanApplication);
        Task<Boolean> CreateLoanApplicationAsync(LoanApplication loanApplication);
        Task<LoanApplication> GetLoanApplicationAsync(int? LoanId = null,
                                      string? CustomerId = null,
                                      int? LoanTypeId = null,
                                      DateTime? ApplicationDate = null,
                                      string? EmployeeId = null,
                                      string? LoanStatus = null);
    }
}
