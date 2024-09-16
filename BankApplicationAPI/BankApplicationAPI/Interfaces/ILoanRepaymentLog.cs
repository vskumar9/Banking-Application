using BankApplicationAPI.Models;

namespace BankApplicationAPI.Interfaces
{
    public interface ILoanRepaymentLog
    {
        Task<IEnumerable<LoanRepaymentLog>> GetLoanRepaymentLogsAsync();
        Task<LoanRepaymentLog> GetLoanRepaymentLogByLoanRepaymentLogIdAsync(int RepaymentId);
        Task<LoanRepaymentLog> UpdateLoanRepaymentLogAsync(LoanRepaymentLog loanRepaymentLog);
        Task<Boolean> DeleteLoanRepaymentLogAsync(LoanRepaymentLog loanRepaymentLog);
        Task<Boolean> CreateLoanRepaymentLogAsync(LoanRepaymentLog loanRepaymentLog);
        Task<IEnumerable<LoanRepaymentLog>> GetLoanRepaymentLogAsync(int? RepaymentId = null,
                                      int? LoanId = null,
                                      string? EmployeeId = null,
                                      string? RepaymentMethod = null);
    }
}
