namespace BankApplicationAPI.Models;

public partial class Employee
{
    public string? EmployeeId { get; set; } = null!;

    public string? EmailAddress { get; set; }

    public string? PasswordHash { get; set; }

    public string? EmployeeFirstName { get; set; }

    public string? EmployeeLastName { get; set; }

    //public string? EmployeeRole { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? LastLoginDate { get; set; }

    public virtual ICollection<AuditLog>? AuditLogs { get; set; } = new List<AuditLog>();

    public virtual ICollection<ComplaintResolution>? ComplaintResolutions { get; set; } = new List<ComplaintResolution>();

    public virtual ICollection<Complaint>? Complaints { get; set; } = new List<Complaint>();

    public virtual ICollection<Configuration>? Configurations { get; set; } = new List<Configuration>();

    public virtual ICollection<LoanApplication>? LoanApplications { get; set; } = new List<LoanApplication>();

    public virtual ICollection<LoanRepaymentLog>? LoanRepaymentLogs { get; set; } = new List<LoanRepaymentLog>();

    public virtual ICollection<TransactionLog>? TransactionLogs { get; set; } = new List<TransactionLog>();

    public virtual ICollection<UserRole>? UserRoles { get; set; } = new List<UserRole>();
}
