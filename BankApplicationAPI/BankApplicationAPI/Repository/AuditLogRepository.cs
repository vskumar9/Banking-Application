using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Repository
{
    public class AuditLogRepository : IAuditLog
    {
        public Task<bool> CreateAuditLogAsync(AuditLog auditLog)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAuditLogAsync(AuditLog auditLog)
        {
            throw new NotImplementedException();
        }

        public Task<AuditLog> GetAuditLogAsync(int? AuditLogId = null, string? Action = null, string? EmployeeId = null, DateTime? ActionDate = null, string? IpAddress = null, string? Details = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AuditLog>> GetAuditLogByAuditLogIdAsync(int AuditLogId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AuditLog>> GetAuditLogsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AuditLog> UpdateAuditLogAsync(AuditLog auditLog)
        {
            throw new NotImplementedException();
        }
    }
}
