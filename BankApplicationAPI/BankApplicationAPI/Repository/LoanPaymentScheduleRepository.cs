using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Repository
{
    public class LoanPaymentScheduleRepository : ILoanPaymentSchedule
    {
        public Task<bool> CreateLoanPaymentScheduleAsync(LoanPaymentSchedule loanPaymentSchedule)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteLoanPaymentScheduleAsync(LoanPaymentSchedule loanPaymentSchedule)
        {
            throw new NotImplementedException();
        }

        public Task<LoanPaymentSchedule> GetLoanPaymentScheduleAsync(int? PaymentId = null, int? LoanId = null, string? PaymentStatus = null, string? LoanStatus = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LoanPaymentSchedule>> GetLoanPaymentScheduleByLoanPaymentScheduleIdAsync(int PaymentId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LoanPaymentSchedule>> GetLoanPaymentSchedulesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<LoanPaymentSchedule> UpdateLoanPaymentScheduleAsync(LoanPaymentSchedule loanPaymentSchedule)
        {
            throw new NotImplementedException();
        }
    }
}
