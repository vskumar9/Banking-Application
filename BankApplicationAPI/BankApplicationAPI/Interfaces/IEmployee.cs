﻿using BankApplicationAPI.Models;

namespace BankApplicationAPI.Interfaces
{
    public interface IEmployee
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync();
        Task<Employee> GetEmployeeByEmployeeIdAsync(string EmployeeId);
        Task<Employee> UpdateEmployeeAsync(Employee employee);
        Task<Boolean> DeleteEmployeeAsync(Employee employee);
        Task<Boolean> CreateEmployeeAsync(Employee employee);
        Task<IEnumerable<Employee>> GetEmployeeAsync(string? EmployeeId = null,
                                      string? EmailAddress = null,
                                      string? EmployeeFirstName = null,
                                      string? EmployeeLastName = null,
                                      string? RoleName = null);
    }
}
