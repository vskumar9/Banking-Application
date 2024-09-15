namespace BankApplicationAPI.Models;

public partial class AccountStatusType
{
    public byte AccountStatusTypeId { get; set; }

    public string? AccountStatusDescription { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
