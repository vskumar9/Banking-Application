namespace BankApplicationAPI.Models;

public partial class SavingsInterestRate
{
    public byte InterestSavingsRateId { get; set; }

    public decimal? InterestRateValue { get; set; }

    public string? InterestRateDescription { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
