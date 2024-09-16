using Microsoft.EntityFrameworkCore;
using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Repository
{
    public class CustomerRepository : ICustomer
    {
        private readonly SunBankContext _context;
        private readonly ILogger<CustomerRepository> _logger;

        public CustomerRepository(SunBankContext context, ILogger<CustomerRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Create a new Customer
        public async Task<bool> CreateCustomerAsync(Customer customer)
        {
            try
            {
                if (customer == null)
                {
                    throw new ArgumentNullException(nameof(customer), "Customer cannot be null");
                }

                await _context.Customers.AddAsync(customer);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error creating customer");
                throw new Exception("An error occurred while creating the customer.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        // Delete a Customer
        public async Task<bool> DeleteCustomerAsync(Customer customer)
        {
            try
            {
                if (customer == null)
                {
                    throw new ArgumentNullException(nameof(customer), "Customer cannot be null");
                }

                var existingCustomer = await _context.Customers.FindAsync(customer.CustomerId);
                if (existingCustomer == null)
                {
                    throw new KeyNotFoundException("Customer not found");
                }

                _context.Customers.Remove(existingCustomer);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error deleting customer");
                throw new Exception("An error occurred while deleting the customer.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        // Get Customer by optional parameters
        public async Task<IEnumerable<Customer>> GetCustomerAsync(string? CustomerId = null, string? CustomerFirstName = null, string? CustomerLastName = null, string? City = null, string? State = null, string? ZipCode = null, string? EmailAddress = null, string? CellPhone = null)
        {
            try
            {
                var query = _context.Customers.AsQueryable();

                if (!string.IsNullOrEmpty(CustomerId))
                    query = query.Where(c => c.CustomerId == CustomerId);

                if (!string.IsNullOrEmpty(CustomerFirstName))
                    query = query.Where(c => c.CustomerFirstName == CustomerFirstName);

                if (!string.IsNullOrEmpty(CustomerLastName))
                    query = query.Where(c => c.CustomerLastName == CustomerLastName);

                if (!string.IsNullOrEmpty(City))
                    query = query.Where(c => c.City == City);

                if (!string.IsNullOrEmpty(State))
                    query = query.Where(c => c.State == State);

                if (!string.IsNullOrEmpty(ZipCode))
                    query = query.Where(c => c.ZipCode == ZipCode);

                if (!string.IsNullOrEmpty(EmailAddress))
                    query = query.Where(c => c.EmailAddress == EmailAddress);

                if (!string.IsNullOrEmpty(CellPhone))
                    query = query.Where(c => c.CellPhone == CellPhone);

                return await query.Include(c => c.Accounts)
                                  .Include(c => c.ComplaintFeedbacks)
                                  .Include(c => c.Complaints)
                                  .Include(c => c.LoanApplications)
                                  .Include(c => c.TransactionLogs)
                                  .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching customer");
                throw new Exception("An error occurred while fetching the customer.", ex);
            }
        }

        // Get Customer by CustomerId
        public async Task<Customer> GetCustomerByCustomerIdAsync(string CustomerId)
        {
            try
            {
                if (string.IsNullOrEmpty(CustomerId))
                {
                    throw new ArgumentException("CustomerId cannot be null or empty", nameof(CustomerId));
                }

                return await _context.Customers
                                     .Where(c => c.CustomerId == CustomerId)
                                     .Include(c => c.Accounts)
                                     .Include(c => c.ComplaintFeedbacks)
                                     .Include(c => c.Complaints)
                                     .Include(c => c.LoanApplications)
                                     .Include(c => c.TransactionLogs)
                                     .FirstOrDefaultAsync() ?? throw new NullReferenceException();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching customer by ID");
                throw new Exception("An error occurred while fetching the customer by ID.", ex);
            }
        }

        // Get all Customers
        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            try
            {
                return await _context.Customers
                                     .Include(c => c.Accounts)
                                     .Include(c => c.ComplaintFeedbacks)
                                     .Include(c => c.Complaints)
                                     .Include(c => c.LoanApplications)
                                     .Include(c => c.TransactionLogs)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all customers");
                throw new Exception("An error occurred while fetching all customers.", ex);
            }
        }

        // Update an existing Customer
        public async Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            try
            {
                if (customer == null)
                {
                    throw new ArgumentNullException(nameof(customer), "Customer cannot be null");
                }

                var existingCustomer = await _context.Customers.FindAsync(customer.CustomerId);
                if (existingCustomer == null)
                {
                    throw new KeyNotFoundException("Customer not found");
                }

                _context.Entry(existingCustomer).CurrentValues.SetValues(customer);
                await _context.SaveChangesAsync();
                return existingCustomer;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error updating customer");
                throw new Exception("An error occurred while updating the customer.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw new Exception("An unexpected error occurred.", ex);
            }
        }
    }
}
