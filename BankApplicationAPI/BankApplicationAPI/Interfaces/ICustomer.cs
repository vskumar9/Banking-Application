using BankApplicationAPI.Models;

namespace BankApplicationAPI.Interfaces
{
    public interface ICustomer
    {
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<IEnumerable<Customer>> GetCustomerByCustomerIdAsync(string CustomerId);
        Task<Customer> UpdateCustomerAsync(Customer customer);
        Task<Boolean> DeleteCustomerAsync(Customer customer);
        Task<Boolean> CreateCustomerAsync(Customer customer);
        Task<Customer> GetCustomerAsync(string? CustomerId = null,
                                      string? CustomerFirstName = null,
                                      string? CustomerLastName = null,
                                      string? City = null,
                                      string? State = null,
                                      string? ZipCode = null,
                                      string? EmailAddress = null,
                                      string? CellPhone = null);
    }
}
