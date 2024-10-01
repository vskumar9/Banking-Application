using BankApplicationAPI.Models;

namespace BankApplicationAPI.Interfaces
{
    public interface ICustomer
    {
        //Customer Details
        Task<IEnumerable<Customer>> GetCustomersAsync();
        //Customer Details By Customer ID
        Task<Customer> GetCustomerByCustomerIdAsync(string CustomerId);
        //Customer Details Update
        Task<Customer> UpdateCustomerAsync(Customer customer);
        //Customer Delete By Customer ID
        Task<Boolean> DeleteCustomerAsync(Customer customer);
        //Create New Customer 
        Task<Boolean> CreateCustomerAsync(Customer customer);
        //Search Customer By different categories
        Task<IEnumerable<Customer>> GetCustomerAsync(string? CustomerId = null, string? CustomerFirstName = null, string? CustomerLastName = null, string? City = null, string? State = null, 
                                                     string? ZipCode = null, string? EmailAddress = null, string? CellPhone = null);

        //Customer Account Details
        Task<IEnumerable<Customer>> GetAccountsByCustomerId(string CustomerId);
        //Customer Complaints
        Task<IEnumerable<Customer>> GetComplaintsByCustomerId(string CustomerId);
        //Customer Loan Applications
        Task<IEnumerable<Customer>> GetLoansByCustomerId(string CustomerId);
        //Customer TransactionLogs
        Task<IEnumerable<Customer>> GetTransactionLogsByCustomerId(string CustomerId);
    }
}
