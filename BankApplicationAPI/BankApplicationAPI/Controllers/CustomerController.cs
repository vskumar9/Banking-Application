using BankApplicationAPI.Helpers;
using BankApplicationAPI.Models;
using BankApplicationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;
        private readonly IdHelper _idHelper;

        public CustomerController(CustomerService customerService, IdHelper idHelper)
        {
            _customerService = customerService;
            _idHelper = idHelper;
        }

        // GET: api/Customer
        [HttpGet]
        [Authorize(Roles = "admin, support")] // Admins and support can view all customers
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            try
            {
                var customers = await _customerService.GetCustomersAsync();
                return Ok(customers);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving customers.");
            }
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        [Authorize(Roles = "admin, support")] // Admins and support can view a specific customer
        public async Task<ActionResult<Customer>> GetCustomer(string id)
        {
            try
            {
                var customers = await _customerService.GetCustomerByCustomerIdAsync(id);
                if (customers == null)
                    return NotFound("Customer not found.");

                return Ok(customers);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving customer.");
            }
        }

        // POST: api/Customer
        [HttpPost]
        [Authorize(Roles = "admin")] // Only admins can create new customers
        public async Task<ActionResult> CreateCustomer([FromBody] Customer customer)
        {
            if (customer == null)
                return BadRequest("Invalid customer data.");

            try
            {
                customer.CustomerId = _idHelper.GenerateCustomerUniqueId();
                var result = await _customerService.CreateCustomerAsync(customer);
                if (result)
                    return CreatedAtAction(nameof(GetCustomer), new { id = customer.CustomerId }, customer);

                return BadRequest("Failed to create customer.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating customer.");
            }
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")] // Only admins can update customer details
        public async Task<ActionResult> UpdateCustomer(string id, [FromBody] Customer customer)
        {
            if (customer == null || customer.CustomerId != id)
                return BadRequest("Data mismatch.");

            try
            {
                var updatedCustomer = await _customerService.UpdateCustomerAsync(customer);
                if (updatedCustomer != null)
                    return Ok(updatedCustomer);

                return NotFound("Customer not found.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating customer.");
            }
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")] // Only admins can delete customers
        public async Task<ActionResult> DeleteCustomer(string id)
        {
            try
            {
                var customers = await _customerService.GetCustomerByCustomerIdAsync(id);
                if (customers == null)
                    return NotFound("Customer not found.");

                var result = await _customerService.DeleteCustomerAsync(customers);
                if (result)
                    return NoContent();

                return BadRequest("Failed to delete customer.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting customer.");
            }
        }
    }
}
