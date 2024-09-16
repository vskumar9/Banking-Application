using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApplicationAPI.Repository
{
    public class AuditLogRepository : IAuditLog
    {
        private readonly SunBankContext _context;
        private readonly ILogger<AuditLogRepository> _logger;

        public AuditLogRepository(SunBankContext context, ILogger<AuditLogRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Create a new AuditLog
        public async Task<bool> CreateAuditLogAsync(AuditLog auditLog)
        {
            try
            {
                await _context.AuditLogs.AddAsync(auditLog);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Error creating audit log: {ex.Message}", ex);
                throw new Exception("An error occurred while creating the audit log.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred: {ex.Message}", ex);
                throw new Exception("An unexpected error occurred.");
            }
        }

        // Delete an AuditLog
        public async Task<bool> DeleteAuditLogAsync(AuditLog auditLog)
        {
            try
            {
                _context.AuditLogs.Remove(auditLog);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Error deleting audit log: {ex.Message}", ex);
                throw new Exception("An error occurred while deleting the audit log.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred: {ex.Message}", ex);
                throw new Exception("An unexpected error occurred.");
            }
        }

        // Get AuditLog by optional parameters
        public async Task<IEnumerable<AuditLog>> GetAuditLogAsync(int? AuditLogId = null, string? Action = null, string? EmployeeId = null, DateTime? ActionDate = null, string? IpAddress = null, string? Details = null)
        {
            try
            {
                var query = _context.AuditLogs.AsQueryable();

                if (AuditLogId.HasValue)
                    query = query.Where(a => a.AuditLogId == AuditLogId.Value);

                if (!string.IsNullOrEmpty(Action))
                    query = query.Where(a => a.Action == Action);

                if (!string.IsNullOrEmpty(EmployeeId))
                    query = query.Where(a => a.EmployeeId == EmployeeId);

                if (ActionDate.HasValue)
                    query = query.Where(a => a.ActionDate == ActionDate.Value);

                if (!string.IsNullOrEmpty(IpAddress))
                    query = query.Where(a => a.IpAddress == IpAddress);

                if (!string.IsNullOrEmpty(Details))
                    query = query.Where(a => a.Details == Details);

                return await query.Include(a => a.Employee).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching audit log: {ex.Message}", ex);
                throw new Exception("An error occurred while fetching the audit log.");
            }
        }

        // Get AuditLog by AuditLogId
        public async Task<AuditLog> GetAuditLogByAuditLogIdAsync(int AuditLogId)
        {
            try
            {
                return await _context.AuditLogs
                                     .Where(a => a.AuditLogId == AuditLogId)
                                     .Include(a => a.Employee).FirstOrDefaultAsync() ?? throw new NullReferenceException();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching audit log by ID: {ex.Message}", ex);
                throw new Exception("An error occurred while fetching the audit log by ID.");
            }
        }

        // Get all AuditLogs
        public async Task<IEnumerable<AuditLog>> GetAuditLogsAsync()
        {
            try
            {
                return await _context.AuditLogs
                                     .Include(a => a.Employee)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching all audit logs: {ex.Message}", ex);
                throw new Exception("An error occurred while fetching the audit logs.");
            }
        }

        // Update an AuditLog
        public async Task<AuditLog> UpdateAuditLogAsync(AuditLog auditLog)
        {
            try
            {
                _context.AuditLogs.Update(auditLog);
                await _context.SaveChangesAsync();
                return auditLog;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Error updating audit log: {ex.Message}", ex);
                throw new Exception("An error occurred while updating the audit log.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred: {ex.Message}", ex);
                throw new Exception("An unexpected error occurred.");
            }
        }
    }
}
