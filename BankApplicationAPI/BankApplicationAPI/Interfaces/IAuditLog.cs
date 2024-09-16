using BankApplicationAPI.Models;

namespace BankApplicationAPI.Interfaces
{
    public interface IAuditLog
    {
        Task<IEnumerable<AuditLog>> GetAuditLogsAsync();
        Task<AuditLog> GetAuditLogByAuditLogIdAsync(int AuditLogId);
        Task<AuditLog> UpdateAuditLogAsync(AuditLog auditLog);
        Task<Boolean> DeleteAuditLogAsync(AuditLog auditLog);
        Task<Boolean> CreateAuditLogAsync(AuditLog auditLog);
        Task<IEnumerable<AuditLog>> GetAuditLogAsync(int? AuditLogId = null,
                                      string? Action = null,
                                      string? EmployeeId = null,
                                      DateTime? ActionDate = null,
                                      string? IpAddress = null,
                                      string? Details = null);
    }
}
