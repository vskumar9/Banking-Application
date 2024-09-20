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

                if (await _context.Employees.AnyAsync(c => c.EmailAddress == employee.EmailAddress))
                {
                    return false;
                }


                if (!string.IsNullOrEmpty(employee.PasswordHash))
                {
                    employee.PasswordHash = BCrypt.Net.BCrypt.HashPassword(employee.PasswordHash);
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
        public async Task<IEnumerable<Employee>> GetEmployeeAsync(string? EmployeeId = null, string? EmailAddress = null, string? EmployeeFirstName = null, string? EmployeeLastName = null, string? RoleName = null)
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
                    query = query.Where(e => e.UserRoles!.Any(ur => ur.Role!.RoleName == RoleName));
                }

                return await query.Include(e => e.AuditLogs)
                                  .Include(e => e.ComplaintResolutions!).ThenInclude(e => e.Complaint)
                                  .Include(e => e.Complaints!).ThenInclude(e => e.ComplaintFeedbacks)
                                  .Include(e => e.Complaints!).ThenInclude(e => e.ComplaintStatusHistories)
                                  .Include(e => e.Complaints!).ThenInclude(e => e.ComplaintType)
                                  .Include(e => e.Configurations!).ThenInclude(e => e.UpdatedByNavigation)
                                  .Include(e => e.LoanApplications!).ThenInclude(e => e.LoanPaymentSchedules)
                                  .Include(e => e.LoanApplications!).ThenInclude(e => e.LoanType)
                                  .Include(e => e.LoanRepaymentLogs!).ThenInclude(e => e.Loan)
                                  .Include(e => e.TransactionLogs!).ThenInclude(e => e.TransactionType)
                                  .Include(e=> e.UserRoles!).ThenInclude(e => e.Role)
                                  .Include(e=> e.UserRoles!).ThenInclude(e => e.Role!.RolePermissions)
                                  .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching employee");
                throw new Exception("An error occurred while fetching the employee.", ex);
            }
        }

        // Get Employee by EmployeeId
        public async Task<Employee> GetEmployeeByEmployeeIdAsync(string EmployeeId)
        {
            try
            {
                if (string.IsNullOrEmpty(EmployeeId))
                {
                    throw new ArgumentException("EmployeeId cannot be null or empty", nameof(EmployeeId));
                }

                // Query the employee by ID and include the relevant navigation properties
                var employee = await _context.Employees
                    //// Exact match on EmployeeId
                    .Include(e => e.AuditLogs)
                   .Include(e => e.ComplaintResolutions)!.ThenInclude(cr => cr!.Complaint)
                    //.Include(e => e.Complaints)!.ThenInclude(c => c!.ComplaintFeedbacks)
                    //.Include(e => e.Complaints)!.ThenInclude(c => c!.ComplaintStatusHistories)
                    //.Include(e => e.Complaints)!.ThenInclude(c => c!.ComplaintType)
                    //.Include(e => e.Configurations)!.ThenInclude(c => c!.UpdatedByNavigation)
                    .Include(e => e.LoanApplications)!.ThenInclude(la => la!.LoanPaymentSchedules)
                    //.Include(e => e.LoanApplications)!.ThenInclude(la => la!.LoanType)
                    //.Include(e => e.LoanRepaymentLogs)!.ThenInclude(lr => lr!.Loan)
                    //.Include(e => e.TransactionLogs)!.ThenInclude(tl => tl!.TransactionType)
                    .Include(e => e.UserRoles)!.ThenInclude(ur => ur!.Role)
                    .Include(e => e.UserRoles)!.ThenInclude(ur => ur!.Role!.RolePermissions)
                    .FirstOrDefaultAsync(e=>e.EmployeeId==EmployeeId);

                if (employee == null)
                {
                    throw new NullReferenceException("Employee not found.");
                }

                return employee;
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
                                     //.Include(e => e.AuditLogs)
                                     // .Include(e => e.ComplaintResolutions!).ThenInclude(e => e.Complaint)
                                     // .Include(e => e.Complaints!).ThenInclude(e => e.ComplaintFeedbacks)
                                     // .Include(e => e.Complaints!).ThenInclude(e => e.ComplaintStatusHistories)
                                     // .Include(e => e.Complaints!).ThenInclude(e => e.ComplaintType)
                                     // .Include(e => e.Configurations!).ThenInclude(e => e.UpdatedByNavigation)
                                     // .Include(e => e.LoanApplications!).ThenInclude(e => e.LoanPaymentSchedules)
                                     // .Include(e => e.LoanApplications!).ThenInclude(e => e.LoanType)
                                     // .Include(e => e.LoanRepaymentLogs!).ThenInclude(e => e.Loan)
                                     // .Include(e => e.TransactionLogs!).ThenInclude(e => e.TransactionType)
                                      .Include(e => e.UserRoles!).ThenInclude(e => e.Role)
                                      .Include(e => e.UserRoles!).ThenInclude(e => e.Role!.RolePermissions)
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

                if (!string.IsNullOrEmpty(employee.PasswordHash) && !BCrypt.Net.BCrypt.Verify(employee.PasswordHash, existingEmployee.PasswordHash))
                {
                    employee.PasswordHash = BCrypt.Net.BCrypt.HashPassword(existingEmployee.PasswordHash);
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
