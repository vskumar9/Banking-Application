using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Services
{
    public class AuditLogService
    {
        private readonly IAuditLog _auditLog;

        public AuditLogService(IAuditLog auditLog)
        {
            _auditLog = auditLog;
        }

        public async Task<bool> CreateAuditLogAsync(AuditLog auditLog)
        {
            try
            {
                if (auditLog == null) return false;
                return await _auditLog.CreateAuditLogAsync(auditLog);
            }
            catch { throw; }
        }

        public async Task<bool> DeleteAuditLogAsync(AuditLog auditLog)
        {
            try
            {
                if (auditLog == null) return false;
                return await _auditLog.DeleteAuditLogAsync(auditLog);
            }
            catch { throw; }
        }

        public async Task<AuditLog> GetAuditLogAsync(int? AuditLogId = null, string? Action = null, string? EmployeeId = null, DateTime? ActionDate = null, string? IpAddress = null, string? Details = null)
        {
            try
            {
                return await _auditLog.GetAuditLogAsync(AuditLogId, Action, EmployeeId, ActionDate, IpAddress, Details);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<AuditLog>> GetAuditLogByAuditLogIdAsync(int AuditLogId)
        {
            try
            {
                return await _auditLog.GetAuditLogByAuditLogIdAsync(AuditLogId);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<AuditLog>> GetAuditLogsAsync()
        {
            try
            {
                return await _auditLog.GetAuditLogsAsync();
            }
            catch { throw; }
        }

        public async Task<AuditLog> UpdateAuditLogAsync(AuditLog auditLog)
        {
            try
            {
                return await _auditLog.UpdateAuditLogAsync(auditLog);
            }
            catch { throw; }
        }
    }
}
