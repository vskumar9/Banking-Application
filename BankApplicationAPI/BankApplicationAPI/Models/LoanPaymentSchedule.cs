namespace BankApplicationAPI.Models;

public partial class LoanPaymentSchedule
{
    public int PaymentId { get; set; }

    public int? LoanId { get; set; }

    public DateTime? PaymentDate { get; set; }

    public decimal? PaymentAmount { get; set; }

    public decimal? BalanceAfterPayment { get; set; }

    public string? PaymentStatus { get; set; }

    public virtual LoanApplication? Loan { get; set; }
}
