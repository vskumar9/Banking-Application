using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Services
{
    public class LoanPaymentScheduleService
    {
        private readonly ILoanPaymentSchedule _loanPaymentSchedule;

        public LoanPaymentScheduleService(ILoanPaymentSchedule loanPaymentSchedule)
        {
            _loanPaymentSchedule = loanPaymentSchedule;
        }

        public async Task<bool> CreateLoanPaymentScheduleAsync(LoanPaymentSchedule loanPaymentSchedule)
        {
            try
            {
                return await _loanPaymentSchedule.CreateLoanPaymentScheduleAsync(loanPaymentSchedule);
            }
            catch { throw; }
        }

        public async Task<bool> DeleteLoanPaymentScheduleAsync(LoanPaymentSchedule loanPaymentSchedule)
        {
            try
            {
                return await _loanPaymentSchedule.DeleteLoanPaymentScheduleAsync(loanPaymentSchedule); 
            }
            catch { throw; }
        }

        public async Task<LoanPaymentSchedule> GetLoanPaymentScheduleAsync(int? PaymentId = null, int? LoanId = null, string? PaymentStatus = null, string? LoanStatus = null)
        {
            try
            {
                return await _loanPaymentSchedule.GetLoanPaymentScheduleAsync(PaymentId, LoanId, PaymentStatus, LoanStatus);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<LoanPaymentSchedule>> GetLoanPaymentScheduleByLoanPaymentScheduleIdAsync(int PaymentId)
        {
            try
            {
                return await _loanPaymentSchedule.GetLoanPaymentScheduleByLoanPaymentScheduleIdAsync(PaymentId);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<LoanPaymentSchedule>> GetLoanPaymentSchedulesAsync()
        {
            try
            {
                return await _loanPaymentSchedule.GetLoanPaymentSchedulesAsync();
            }
            catch { throw; }
        }

        public async Task<LoanPaymentSchedule> UpdateLoanPaymentScheduleAsync(LoanPaymentSchedule loanPaymentSchedule)
        {
            try
            {
                return await _loanPaymentSchedule.UpdateLoanPaymentScheduleAsync(loanPaymentSchedule);
            }
            catch { throw; }
        }

    }
}
