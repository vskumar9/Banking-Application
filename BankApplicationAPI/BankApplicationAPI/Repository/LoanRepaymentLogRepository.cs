using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Repository
{
    public class LoanRepaymentLogRepository : ILoanRepaymentLog
    {
        public Task<bool> CreateLoanRepaymentLogAsync(LoanRepaymentLog loanRepaymentLog)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteLoanRepaymentLogAsync(LoanRepaymentLog loanRepaymentLog)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LoanRepaymentLog>> GetLoanLoanRepaymentLogsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<LoanRepaymentLog> GetLoanRepaymentLogAsync(int? RepaymentId = null, int? LoanId = null, string? EmployeeId = null, string? RepaymentMethod = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LoanRepaymentLog>> GetLoanRepaymentLogByLoanRepaymentLogIdAsync(int RepaymentId)
        {
            throw new NotImplementedException();
        }

        public Task<LoanRepaymentLog> UpdateLoanRepaymentLogAsync(LoanRepaymentLog loanRepaymentLog)
        {
            throw new NotImplementedException();
        }
    }
}
