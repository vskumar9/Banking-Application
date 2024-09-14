namespace BankApplicationAPI.Models;

public partial class ComplaintFeedback
{
    public int FeedbackId { get; set; }

    public int? ComplaintId { get; set; }

    public string? CustomerId { get; set; }

    public DateTime? FeedbackDate { get; set; }

    public byte? FeedbackRating { get; set; }

    public string? FeedbackComments { get; set; }

    public virtual Complaint? Complaint { get; set; }

    public virtual Customer? Customer { get; set; }
}
