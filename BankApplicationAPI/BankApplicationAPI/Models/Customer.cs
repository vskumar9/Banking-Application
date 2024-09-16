namespace BankApplicationAPI.Models;

public partial class Customer
{
    public string? CustomerId { get; set; } = null!;
    public string? PasswordHash { get; set; }
    public string? CustomerFirstName { get; set; }
    public string? CustomerLastName { get; set; }
    public string? CustomerAddress1 { get; set; }
    public string? CustomerAddress2 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
    public string? EmailAddress { get; set; }
    public string? CellPhone { get; set; }
    public string? HomePhone { get; set; }
    public string? WorkPhone { get; set; }
    public bool? IsActive { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public virtual ICollection<Account>? Accounts { get; set; } = new List<Account>();

    public virtual ICollection<ComplaintFeedback>? ComplaintFeedbacks { get; set; } = new List<ComplaintFeedback>();

    public virtual ICollection<Complaint>? Complaints { get; set; } = new List<Complaint>();

    public virtual ICollection<LoanApplication>? LoanApplications { get; set; } = new List<LoanApplication>();

    public virtual ICollection<TransactionLog>? TransactionLogs { get; set; } = new List<TransactionLog>();
}
