namespace BankApplicationAPI.Models;

public partial class ComplaintResolution
{
    public int ResolutionId { get; set; }

    public int? ComplaintId { get; set; }

    public DateTime? ResolutionDate { get; set; }

    public string? ResolutionMethod { get; set; }

    public string? ResolutionDescription { get; set; }

    public string? EmployeeId { get; set; }

    public virtual Complaint? Complaint { get; set; }

    public virtual Employee? Employee { get; set; }
}
