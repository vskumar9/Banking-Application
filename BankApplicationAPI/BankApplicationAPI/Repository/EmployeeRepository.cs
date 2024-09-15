using Microsoft.EntityFrameworkCore;
using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Repository
{
    public class EmployeeRepository : IEmployee
    {
        private readonly SunBankContext _context;
        private readonly ILogger<EmployeeRepository> _logger;

        public EmployeeRepository(SunBankContext context, ILogger<EmployeeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Create a new Employee
        public async Task<bool> CreateEmployeeAsync(Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    throw new ArgumentNullException(nameof(employee), "Employee cannot be null");
                }

                await _context.Employees.AddAsync(employee);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error creating employee");
                throw new Exception("An error occurred while creating the employee.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        // Delete an Employee
        public async Task<bool> DeleteEmployeeAsync(Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    throw new ArgumentNullException(nameof(employee), "Employee cannot be null");
                }

                var existingEmployee = await _context.Employees.FindAsync(employee.EmployeeId);
                if (existingEmployee == null)
                {
                    throw new KeyNotFoundException("Employee not found");
                }

                _context.Employees.Remove(existingEmployee);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error deleting employee");
                throw new Exception("An error occurred while deleting the employee.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        // Get Employee by optional parameters
        public async Task<Employee> GetEmployeeAsync(string? EmployeeId = null, string? EmailAddress = null, string? EmployeeFirstName = null, string? EmployeeLastName = null, string? RoleName = null)
        {
            try
            {
                var query = _context.Employees.AsQueryable();

                if (!string.IsNullOrEmpty(EmployeeId))
                    query = query.Where(e => e.EmployeeId == EmployeeId);

                if (!string.IsNullOrEmpty(EmailAddress))
                    query = query.Where(e => e.EmailAddress == EmailAddress);

                if (!string.IsNullOrEmpty(EmployeeFirstName))
                    query = query.Where(e => e.EmployeeFirstName == EmployeeFirstName);

                if (!string.IsNullOrEmpty(EmployeeLastName))
                    query = query.Where(e => e.EmployeeLastName == EmployeeLastName);

                if (!string.IsNullOrEmpty(RoleName))
                {
                    query = query.Where(e => e.UserRoles.Any(ur => ur.Role.RoleName == RoleName));
                }

                return await query.Include(e => e.AuditLogs)
                                  .Include(e => e.ComplaintResolutions)
                                  .Include(e => e.Complaints)
                                  .Include(e => e.Configurations)
                                  .Include(e => e.LoanApplications)
                                  .Include(e => e.LoanRepaymentLogs)
                                  .Include(e => e.TransactionLogs)
                                  .FirstOrDefaultAsync() ?? throw new NullReferenceException("Employee not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching employee");
                throw new Exception("An error occurred while fetching the employee.", ex);
            }
        }

        // Get Employee by EmployeeId
        public async Task<IEnumerable<Employee>> GetEmployeeByEmployeeIdAsync(string EmployeeId)
        {
            try
            {
                if (string.IsNullOrEmpty(EmployeeId))
                {
                    throw new ArgumentException("EmployeeId cannot be null or empty", nameof(EmployeeId));
                }

                return await _context.Employees
                                     .Where(e => e.EmployeeId == EmployeeId)
                                     .Include(e => e.AuditLogs)
                                     .Include(e => e.ComplaintResolutions)
                                     .Include(e => e.Complaints)
                                     .Include(e => e.Configurations)
                                     .Include(e => e.LoanApplications)
                                     .Include(e => e.LoanRepaymentLogs)
                                     .Include(e => e.TransactionLogs)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching employee by ID");
                throw new Exception("An error occurred while fetching the employee by ID.", ex);
            }
        }

        // Get all Employees
        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            try
            {
                return await _context.Employees
                                     .Include(e => e.AuditLogs)
                                     .Include(e => e.ComplaintResolutions)
                                     .Include(e => e.Complaints)
                                     .Include(e => e.Configurations)
                                     .Include(e => e.LoanApplications)
                                     .Include(e => e.LoanRepaymentLogs)
                                     .Include(e => e.TransactionLogs)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all employees");
                throw new Exception("An error occurred while fetching all employees.", ex);
            }
        }

        // Update an existing Employee
        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    throw new ArgumentNullException(nameof(employee), "Employee cannot be null");
                }

                var existingEmployee = await _context.Employees.FindAsync(employee.EmployeeId);
                if (existingEmployee == null)
                {
                    throw new KeyNotFoundException("Employee not found");
                }

                _context.Entry(existingEmployee).CurrentValues.SetValues(employee);
                await _context.SaveChangesAsync();
                return existingEmployee;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error updating employee");
                throw new Exception("An error occurred while updating the employee.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw new Exception("An unexpected error occurred.", ex);
            }
        }
    }
}
