using BankApplicationAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApplicationAPI.Repository
{
    public class ConfigurationRepository : Interfaces.IConfiguration
    {
        private readonly SunBankContext _context;
        private readonly ILogger<ConfigurationRepository> _logger;

        public ConfigurationRepository(SunBankContext context, ILogger<ConfigurationRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Create a new Configuration
        public async Task<bool> CreateConfigurationAsync(Configuration configuration)
        {
            try
            {
                await _context.Configurations.AddAsync(configuration);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Error creating configuration: {ex.Message}", ex);
                throw new Exception("An error occurred while creating the configuration.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred: {ex.Message}", ex);
                throw new Exception("An unexpected error occurred.");
            }
        }

        // Delete a Configuration
        public async Task<bool> DeleteConfigurationAsync(Configuration configuration)
        {
            try
            {
                _context.Configurations.Remove(configuration);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Error deleting configuration: {ex.Message}", ex);
                throw new Exception("An error occurred while deleting the configuration.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred: {ex.Message}", ex);
                throw new Exception("An unexpected error occurred.");
            }
        }

        // Get Configuration by optional parameters
        public async Task<Configuration> GetConfigurationAsync(int? configurationId = null, string? configKey = null, string? configValue = null, DateTime? lastUpdated = null, string? updatedBy = null)
        {
            try
            {
                var query = _context.Configurations.AsQueryable();

                if (configurationId.HasValue)
                    query = query.Where(c => c.ConfigurationId == configurationId.Value);

                if (!string.IsNullOrEmpty(configKey))
                    query = query.Where(c => c.ConfigKey == configKey);

                if (!string.IsNullOrEmpty(configValue))
                    query = query.Where(c => c.ConfigValue == configValue);

                if (lastUpdated.HasValue)
                    query = query.Where(c => c.LastUpdated == lastUpdated.Value);

                if (!string.IsNullOrEmpty(updatedBy))
                    query = query.Where(c => c.UpdatedBy == updatedBy);

                return await query.Include(c => c.UpdatedByNavigation)
                                  .FirstOrDefaultAsync() ?? throw new NullReferenceException();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching configuration: {ex.Message}", ex);
                throw new Exception("An error occurred while fetching the configuration.");
            }
        }

        // Get Configuration by ConfigurationId
        public async Task<IEnumerable<Configuration>> GetConfigurationByConfigurationIdAsync(int configurationId)
        {
            try
            {
                return await _context.Configurations
                                     .Where(c => c.ConfigurationId == configurationId)
                                     .Include(c => c.UpdatedByNavigation)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching configuration by ID: {ex.Message}", ex);
                throw new Exception("An error occurred while fetching the configuration by ID.");
            }
        }

        // Get all Configurations
        public async Task<IEnumerable<Configuration>> GetConfigurationsAsync()
        {
            try
            {
                return await _context.Configurations
                                     .Include(c => c.UpdatedByNavigation)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching all configurations: {ex.Message}", ex);
                throw new Exception("An error occurred while fetching all configurations.");
            }
        }

        // Update an existing Configuration
        public async Task<Configuration> UpdateConfigurationAsync(Configuration configuration)
        {
            try
            {
                _context.Configurations.Update(configuration);
                await _context.SaveChangesAsync();
                return configuration;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Error updating configuration: {ex.Message}", ex);
                throw new Exception("An error occurred while updating the configuration.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred: {ex.Message}", ex);
                throw new Exception("An unexpected error occurred.");
            }
        }
    }
}
