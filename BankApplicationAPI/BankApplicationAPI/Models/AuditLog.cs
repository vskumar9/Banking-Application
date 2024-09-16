
namespace BankApplicationAPI.Models;

public partial class AuditLog
{
    public int? AuditLogId { get; set; }

    public string? Action { get; set; } = null!;

    public string? EmployeeId { get; set; } = null!;

    public DateTime? ActionDate { get; set; }

    public string? IpAddress { get; set; }

    public string? Details { get; set; }
    
    public virtual Employee? Employee { get; set; } = null!;
}
