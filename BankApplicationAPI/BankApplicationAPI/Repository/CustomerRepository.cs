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

                if (await _context.Customers.AnyAsync(c => c.EmailAddress == customer.EmailAddress) || (await _context.Customers.AnyAsync(c => c.CellPhone == customer.CellPhone)))
                {
                    return false;
                }

                if (!string.IsNullOrEmpty(customer.PasswordHash))
                {
                    customer.PasswordHash = BCrypt.Net.BCrypt.HashPassword(customer.PasswordHash);
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

                return await query.Include(c => c.Accounts!).ThenInclude(c => c.AccountStatusType)
                                  .Include(c => c.Accounts!).ThenInclude(c => c.InterestSavingsRate)
                                  .Include(c => c.ComplaintFeedbacks!).ThenInclude(c => c.Complaint)
                                  .Include(c => c.Complaints!).ThenInclude(c => c.ComplaintResolutions)
                                  .Include(c => c.LoanApplications!).ThenInclude(c => c.LoanPaymentSchedules)
                                  .Include(c => c.LoanApplications!).ThenInclude(c => c.LoanRepaymentLogs)
                                  .Include(c => c.TransactionLogs!).ThenInclude(c => c.TransactionType)
                                  .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching customer");
                throw new Exception("An error occurred while fetching the customer.", ex);
            }
        }

        // Get Customer by CustomerId
        public async Task<Customer> GetCustomerByCustomerIdAsync(string customerId)
        {
            try
            {
                if (string.IsNullOrEmpty(customerId))
                {
                    throw new ArgumentException("CustomerId cannot be null or empty", nameof(customerId));
                }

                return await _context.Customers                                     
                                     .Include(c => c.Accounts!).ThenInclude(c => c.AccountStatusType)
                                     .Include(c => c.Accounts!).ThenInclude(c => c.InterestSavingsRate)
                                     .Include(c => c.ComplaintFeedbacks!)
                                     .Include(c => c.Complaints!).ThenInclude(c => c.ComplaintResolutions)
                                     .Include(c => c.LoanApplications!).ThenInclude(c => c.LoanPaymentSchedules)
                                     .Include(c => c.LoanApplications!).ThenInclude(c => c.LoanType)
                                     .Include(c => c.LoanApplications!).ThenInclude(c => c.LoanRepaymentLogs)
                                     .Include(c => c.TransactionLogs!).ThenInclude(c => c.TransactionType)
                                     .FirstOrDefaultAsync(c=>c.CustomerId!.Equals(customerId)) ?? throw new NullReferenceException("id is null");
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
                                      .Include(c => c.Accounts!).ThenInclude(c => c.AccountStatusType)
                                      //.Include(c => c.Accounts!).ThenInclude(c => c.InterestSavingsRate)
                                      //.Include(c => c.ComplaintFeedbacks!).ThenInclude(c => c.Complaint)
                                      //.Include(c => c.Complaints!).ThenInclude(c => c.ComplaintResolutions)
                                      //.Include(c => c.LoanApplications!).ThenInclude(c => c.LoanPaymentSchedules)
                                      //.Include(c => c.LoanApplications!).ThenInclude(c => c.LoanRepaymentLogs)
                                      //.Include(c => c.TransactionLogs!).ThenInclude(c => c.TransactionType)
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

                var existingCustomer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == customer.CustomerId);
                if (existingCustomer == null)
                {
                    throw new KeyNotFoundException("Customer not found");
                }

                // If the customer is attempting to update the password
                if (!string.IsNullOrEmpty(customer.PasswordHash))
                {
                    // Check if the provided password matches the existing password hash
                    if (!BCrypt.Net.BCrypt.Verify(customer.PasswordHash, existingCustomer.PasswordHash))
                    {
                        existingCustomer.PasswordHash = BCrypt.Net.BCrypt.HashPassword(customer.PasswordHash);
                    }
                }
                else
                {
                    // If password is null or empty, keep the existing password hash
                    existingCustomer.PasswordHash = existingCustomer.PasswordHash;
                }

                // Update other customer fields
                existingCustomer.CustomerFirstName = customer.CustomerFirstName;
                existingCustomer.CustomerLastName = customer.CustomerLastName;
                existingCustomer.CustomerAddress1 = customer.CustomerAddress1;
                existingCustomer.CustomerAddress2 = customer.CustomerAddress2;
                existingCustomer.CellPhone = customer.CellPhone;
                existingCustomer.WorkPhone = customer.WorkPhone;
                existingCustomer.City = customer.City;
                existingCustomer.EmailAddress = customer.EmailAddress;
                existingCustomer.State = customer.State;
                existingCustomer.ZipCode = customer.ZipCode;
                existingCustomer.HomePhone = customer.HomePhone;

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
