using BankApplicationAPI.Models;
using BankApplicationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
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

        // GET: api/Employee/5
        [HttpGet("{id}")]
        [Authorize(Roles = "admin, support")] // Admins and support can view a specific employee
        public async Task<ActionResult<Employee>> GetEmployee(string id)
        {
            try
            {
                var employees = await _employeeService.GetEmployeeByEmployeeIdAsync(id);
                if (employees == null || !employees.Any())
                    return NotFound("Employee not found.");

                return Ok(employees.First());
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
                if (employees == null || !employees.Any())
                    return NotFound("Employee not found.");

                var result = await _employeeService.DeleteEmployeeAsync(employees.First());
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
