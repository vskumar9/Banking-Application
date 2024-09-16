namespace BankApplicationAPI.Models;

public partial class TransactionType
{
    public byte? TransactionTypeId { get; set; }

    public string? TransactionTypeName { get; set; }

    public string? TransactionTypeDescription { get; set; }

    public decimal? TransactionFeeAmount { get; set; }

    public virtual ICollection<TransactionLog>? TransactionLogs { get; set; } = new List<TransactionLog>();
}
