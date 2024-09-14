namespace BankApplicationAPI.Models;

public partial class ComplaintStatusHistory
{
    public int StatusHistoryId { get; set; }

    public int? ComplaintId { get; set; }

    public DateTime? StatusDate { get; set; }

    public string? ComplaintStatus { get; set; }

    public string? StatusComments { get; set; }

    public virtual Complaint? Complaint { get; set; }
}
