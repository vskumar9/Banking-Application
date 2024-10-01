using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Services
{
    public class CustomerService
    {
        private readonly ICustomer _customer;

        public CustomerService(ICustomer customer)
        {
            _customer = customer;
        }

        public async Task<bool> CreateCustomerAsync(Customer customer)
        {
            try
            {
                return await _customer.CreateCustomerAsync(customer);
            }
            catch { throw; }
        }

        public async Task<bool> DeleteCustomerAsync(Customer customer)
        {
            try
            {
                return await _customer.DeleteCustomerAsync(customer);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<Customer>> GetCustomerAsync(string? CustomerId = null, string? CustomerFirstName = null, string? CustomerLastName = null, string? City = null, string? State = null, string? ZipCode = null, string? EmailAddress = null, string? CellPhone = null)
        {
            try
            {
                return await _customer.GetCustomerAsync(CustomerId, CustomerFirstName, CustomerLastName, City, State, ZipCode, EmailAddress, CellPhone);
            }
            catch { throw; }
        }

        public async Task<Customer> GetCustomerByCustomerIdAsync(string CustomerId)
        {
            try
            {
                return await _customer.GetCustomerByCustomerIdAsync(CustomerId);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            try
            {
                return await _customer.GetCustomersAsync();
            }
            catch { throw; }
        }

        public async Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            try
            {
                return await _customer.UpdateCustomerAsync(customer);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<Customer>> GetAccountsByCustomerId(string customerId)
        {
            try
            {
                return await _customer.GetAccountsByCustomerId(customerId);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<Customer>> GetComplaintsByCustomerId(string customerId)
        {
            try
            {
                return await _customer.GetComplaintsByCustomerId(customerId);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<Customer>> GetLoansByCustomerId(string customerId)
        {
            try
            {
                return await _customer.GetLoansByCustomerId(customerId);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<Customer>> GetTransactionLogsByCustomerId(string customerId)
        {
            try
            {
                return await _customer.GetTransactionLogsByCustomerId(customerId);
            }
            catch { throw; }
        }



    }
}
