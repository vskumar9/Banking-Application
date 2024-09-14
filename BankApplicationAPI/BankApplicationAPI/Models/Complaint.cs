namespace BankApplicationAPI.Models;

public partial class Complaint
{
    public int ComplaintId { get; set; }

    public string? CustomerId { get; set; }

    public int? ComplaintTypeId { get; set; }

    public DateTime? ComplaintDate { get; set; }

    public string? ComplaintDescription { get; set; }

    public string? Files { get; set; }

    public string? ComplaintStatus { get; set; }

    public string? EmployeeId { get; set; }

    public DateTime? ResolutionDate { get; set; }

    public string? ResolutionComments { get; set; }

    public virtual ICollection<ComplaintFeedback> ComplaintFeedbacks { get; set; } = new List<ComplaintFeedback>();

    public virtual ICollection<ComplaintResolution> ComplaintResolutions { get; set; } = new List<ComplaintResolution>();

    public virtual ICollection<ComplaintStatusHistory> ComplaintStatusHistories { get; set; } = new List<ComplaintStatusHistory>();

    public virtual ComplaintType? ComplaintType { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Employee? Employee { get; set; }
}
