namespace BankApplicationAPI.Models;

public partial class LoanRepaymentLog
{
    public int RepaymentId { get; set; }

    public int? LoanId { get; set; }

    public DateTime? RepaymentDate { get; set; }

    public decimal? RepaymentAmount { get; set; }

    public string? EmployeeId { get; set; }

    public string? RepaymentMethod { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual LoanApplication? Loan { get; set; }
}
