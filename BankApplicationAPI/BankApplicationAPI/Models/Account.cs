namespace BankApplicationAPI.Models;

public partial class Account
{
    public int? AccountId { get; set; }

    public decimal? CurrentBalance { get; set; }

    public byte? InterestSavingsRateId { get; set; }

    public byte? AccountStatusTypeId { get; set; }

    public string? CustomerId { get; set; }

    public virtual AccountStatusType? AccountStatusType { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual SavingsInterestRate? InterestSavingsRate { get; set; }

    public virtual ICollection<TransactionLog>? TransactionLogs { get; set; } = new List<TransactionLog>();
}
