using Microsoft.EntityFrameworkCore;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Services
{
    public class TokenExpiryService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public TokenExpiryService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<SunBankContext>();
                    await ExpireInactiveUsers(context);
                    await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
                }
            }
        }

        private async Task ExpireInactiveUsers(SunBankContext context)
        {
            var now = DateTime.UtcNow;
            var expiredCustomers = await context.Customers
                .Where(c => c.IsActive && c.LastLoginDate < now.AddDays(-1)) 
                .ToListAsync();

            var expiredEmployees = await context.Employees
                .Where(e => e.IsActive && e.LastLoginDate < now.AddDays(-1)) 
                .ToListAsync();

            foreach (var customer in expiredCustomers)
            {
                customer.IsActive = false; 
            }

            foreach (var employee in expiredEmployees)
            {
                employee.IsActive = false; 
            }
            await context.SaveChangesAsync();
        }
    }
}
