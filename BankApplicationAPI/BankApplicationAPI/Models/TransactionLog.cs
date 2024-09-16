namespace BankApplicationAPI.Models;

public partial class TransactionLog
{
    public int? TransactionId { get; set; }

    public DateTime? TransactionDate { get; set; }

    public byte? TransactionTypeId { get; set; }

    public decimal? TransactionAmount { get; set; }

    public decimal? NewBalance { get; set; }

    public int? AccountId { get; set; }

    public string? EmployeeId { get; set; }

    public string? CustomerId { get; set; }

    public virtual Account? Account { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual TransactionType? TransactionType { get; set; }
}
