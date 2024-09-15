namespace BankApplicationAPI.Models;

public partial class LoanType
{
    public int LoanTypeId { get; set; }

    public string? LoanTypeName { get; set; }

    public decimal? InterestRate { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<LoanApplication> LoanApplications { get; set; } = new List<LoanApplication>();
}
