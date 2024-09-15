namespace BankApplicationAPI.Models;

public partial class LoanApplication
{
    public int LoanId { get; set; }

    public string? CustomerId { get; set; }

    public int? LoanTypeId { get; set; }

    public decimal? LoanAmount { get; set; }

    public DateTime? ApplicationDate { get; set; }

    public string? Files { get; set; }

    public string? LoanStatus { get; set; }

    public string? EmployeeId { get; set; }

    public DateTime? ApprovalDate { get; set; }

    public string? Comments { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<LoanPaymentSchedule> LoanPaymentSchedules { get; set; } = new List<LoanPaymentSchedule>();

    public virtual ICollection<LoanRepaymentLog> LoanRepaymentLogs { get; set; } = new List<LoanRepaymentLog>();

    public virtual LoanType? LoanType { get; set; }
}
