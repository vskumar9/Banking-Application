using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Services
{
    public class EmployeeService
    {
        private readonly IEmployee _employee;

        public EmployeeService(IEmployee employee)
        {
            _employee = employee;
        }

        public async Task<bool> CreateEmployeeAsync(Employee employee)
        {
            try
            {
                return await _employee.CreateEmployeeAsync(employee);
            }
            catch { throw; }
        }

        public async Task<bool> DeleteEmployeeAsync(Employee employee)
        {
            try
            {
                return await _employee.DeleteEmployeeAsync(employee);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<Employee>> GetEmployeeAsync(string? EmployeeId = null, string? EmailAddress = null, string? EmployeeFirstName = null, string? EmployeeLastName = null, string? RoleName = null)
        {
            try
            {
                return await _employee.GetEmployeeAsync(EmployeeId, EmailAddress, EmployeeFirstName, EmployeeLastName, RoleName);
            }
            catch { throw; }
        }

        public async Task<Employee> GetEmployeeByEmployeeIdAsync(string EmployeeId)
        {
            try
            {
                return await _employee.GetEmployeeByEmployeeIdAsync(EmployeeId);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            try
            {
                return await _employee.GetEmployeesAsync();
            }
            catch { throw; }
        }

        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            try
            {
                return await _employee.UpdateEmployeeAsync(employee);
            }
            catch { throw; }
        }
    }
}
