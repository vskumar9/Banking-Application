using BankApplicationAPI.Exceptions;
using BankApplicationAPI.Helpers;
using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;
using BankApplicationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BankApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;
        private readonly IdHelper _idHelper;
        private readonly ApplicationUtil _applicationUtil;

        public EmployeeController(EmployeeService employeeService, IdHelper idHelper, ApplicationUtil applicationUtil)
        {
            _employeeService = employeeService;
            _idHelper = idHelper;
            _applicationUtil = applicationUtil;
        }

        // GET: api/Employee
        [HttpGet]
        [Authorize(Roles = "admin")] // Only admins can view all employees
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            try
            {
                var employees = await _employeeService.GetEmployeesAsync();
                return Ok(employees);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving employees.");
            }
        }

        [HttpGet("employee")]
        [Authorize(Roles = "admin, support, cashier, staff")] // Only admins can view all employees
        public async Task<ActionResult<Employee>> GetEmployeeByEmployee()
        {
            try
            {
                var employeeId = User.FindFirstValue(ClaimTypes.PrimarySid);
                if (string.IsNullOrEmpty(employeeId))
                    return Unauthorized("Invalid token.");

                var employee = await _employeeService.GetEmployeeByEmployeeIdAsync(employeeId);
                return Ok(employee);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving employees.");
            }
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        [Authorize(Roles = "admin, support")] // Admins and support can view a specific employee
        public async Task<ActionResult<Employee>> GetEmployee(string id)
        {
            try
            {
                var employees = await _employeeService.GetEmployeeByEmployeeIdAsync(id);
                if (employees == null)
                    return NotFound("Employee not found.");

                return Ok(employees);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving employee.");
            }
        }

        // POST: api/Employee
        [HttpPost]
        [Authorize(Roles = "admin")] // Only admins can create new employees
        public async Task<ActionResult> CreateEmployee([FromBody] Employee employee)
        {
            if (employee == null)
                return BadRequest("Invalid employee data.");

            try
            {
                employee.EmployeeId = _idHelper.GenerateEmployeeUniqueId();
                _applicationUtil.ValidateEmail(employee.EmailAddress!);
                _applicationUtil.ValidatePassword(employee.PasswordHash!);
                var result = await _employeeService.CreateEmployeeAsync(employee);
                if (result)
                    return CreatedAtAction(nameof(GetEmployee), new { id = employee.EmployeeId }, employee);

                return BadRequest("Failed to create employee.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating employee.");
            }
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")] // Only admins can update employee details
        public async Task<ActionResult> UpdateEmployee(string id, [FromBody] Employee employee)
        {
            if (employee == null || employee.EmployeeId != id)
                return BadRequest("Data mismatch.");

            try
            {
                _applicationUtil.ValidateEmail(employee.EmailAddress!);
                _applicationUtil.ValidatePassword(employee.PasswordHash!);
                var updatedEmployee = await _employeeService.UpdateEmployeeAsync(employee);
                if (updatedEmployee != null)
                    return Ok(updatedEmployee);

                return NotFound("Employee not found.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating employee.");
            }
        }
        
        [HttpPut]
        [Authorize(Roles = "staff, admin, support, chashier")] // Only admins can update employee details
        public async Task<ActionResult> UpdateEmployeeByEmployee()
        {
            try
            {
                var employeeId = User.FindFirstValue(ClaimTypes.PrimarySid);
                if (string.IsNullOrEmpty(employeeId))
                    return Unauthorized("Invalid token.");

                var employee = await _employeeService.GetEmployeeByEmployeeIdAsync(employeeId);

                var updatedEmployee = await _employeeService.UpdateEmployeeAsync(employee);
                if (updatedEmployee != null)
                    return Ok(updatedEmployee);

                return NotFound("Employee not found.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating employee.");
            }
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")] // Only admins can delete employees
        public async Task<ActionResult> DeleteEmployee(string id)
        {
            try
            {
                var employees = await _employeeService.GetEmployeeByEmployeeIdAsync(id);
                if (employees == null)
                    return NotFound("Employee not found.");

                var result = await _employeeService.DeleteEmployeeAsync(employees);
                if (result)
                    return NoContent();

                return BadRequest("Failed to delete employee.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting employee.");
            }
        }
    }
}
