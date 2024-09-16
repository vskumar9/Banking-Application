using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Services
{
    public class LoanRepaymentLogService
    {
        private readonly ILoanRepaymentLog _loanRepaymentLog;

        public LoanRepaymentLogService(ILoanRepaymentLog loanRepaymentLog)
        {
            _loanRepaymentLog = loanRepaymentLog;
        }

        public async Task<bool> CreateLoanRepaymentLogAsync(LoanRepaymentLog loanRepaymentLog)
        {
            try
            {
                return await _loanRepaymentLog.CreateLoanRepaymentLogAsync(loanRepaymentLog);
            }
            catch { throw; }
        }

        public async Task<bool> DeleteLoanRepaymentLogAsync(LoanRepaymentLog loanRepaymentLog)
        {
            try
            {
                return await _loanRepaymentLog.DeleteLoanRepaymentLogAsync(loanRepaymentLog);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<LoanRepaymentLog>> GetLoanRepaymentLogAsync(int? RepaymentId = null, int? LoanId = null, string? EmployeeId = null, string? RepaymentMethod = null)
        {
            try
            {
                return await _loanRepaymentLog.GetLoanRepaymentLogAsync(RepaymentId, LoanId, EmployeeId, RepaymentMethod);
            }
            catch { throw; }
        }

        public async Task<LoanRepaymentLog> GetLoanRepaymentLogByLoanRepaymentLogIdAsync(int RepaymentId)
        {
            try
            {
                return await _loanRepaymentLog.GetLoanRepaymentLogByLoanRepaymentLogIdAsync(RepaymentId);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<LoanRepaymentLog>> GetLoanRepaymentLogsAsync()
        {
            try
            {
                return await _loanRepaymentLog.GetLoanRepaymentLogsAsync();
            }
            catch { throw; }
        }

        public async Task<LoanRepaymentLog> UpdateLoanRepaymentLogAsync(LoanRepaymentLog loanRepaymentLog)
        {
            try
            {
                return await _loanRepaymentLog.UpdateLoanRepaymentLogAsync(loanRepaymentLog);
            }
            catch { throw; }
        }


    }
}
