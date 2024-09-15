using BankApplicationAPI.Models;

namespace BankApplicationAPI.Interfaces
{
    public interface ILoanPaymentSchedule
    {
        Task<IEnumerable<LoanPaymentSchedule>> GetLoanPaymentSchedulesAsync();
        Task<IEnumerable<LoanPaymentSchedule>> GetLoanPaymentScheduleByLoanPaymentScheduleIdAsync(int PaymentId);
        Task<LoanPaymentSchedule> UpdateLoanPaymentScheduleAsync(LoanPaymentSchedule loanPaymentSchedule);
        Task<Boolean> DeleteLoanPaymentScheduleAsync(LoanPaymentSchedule loanPaymentSchedule);
        Task<Boolean> CreateLoanPaymentScheduleAsync(LoanPaymentSchedule loanPaymentSchedule);
        Task<LoanPaymentSchedule> GetLoanPaymentScheduleAsync(int? PaymentId = null,
                                      int? LoanId = null,
                                      string? PaymentStatus = null,
                                      string? LoanStatus = null);
    }
}
