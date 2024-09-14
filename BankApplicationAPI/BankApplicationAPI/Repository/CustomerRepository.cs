using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Repository
{
    public class CustomerRepository : ICustomer
    {
        public Task<bool> CreateCustomerAsync(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCustomerAsync(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetCustomerAsync(string? CustomerId = null, string? CustomerFirstName = null, string? CustomerLastName = null, string? City = null, string? State = null, string? ZipCode = null, string? EmailAddress = null, string? CellPhone = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customer>> GetCustomerByCustomerIdAsync(string CustomerId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
