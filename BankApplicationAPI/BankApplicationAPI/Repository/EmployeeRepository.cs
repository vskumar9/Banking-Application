using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Repository
{
    public class EmployeeRepository : IEmployee
    {
        public Task<bool> CreateEmployeeAsync(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteEmployeeAsync(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetEmployeeAsync(string? EmployeeId = null, string? EmailAddress = null, string? EmployeeFirstName = null, string? EmployeeLastName = null, string? RoleName = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Employee>> GetEmployeeByEmployeeIdAsync(string EmployeeId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
