namespace BankApplicationAPI.Models;

public partial class ComplaintType
{
    public int ComplaintTypeId { get; set; }

    public string? ComplaintTypeName { get; set; }

    public string? ComplaintTypeDescription { get; set; }

    public virtual ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();
}
