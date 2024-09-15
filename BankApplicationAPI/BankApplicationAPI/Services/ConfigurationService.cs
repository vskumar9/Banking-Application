using BankApplicationAPI.Models;

namespace BankApplicationAPI.Services
{
    public class ConfigurationService
    {
        private readonly Interfaces.IConfiguration _configuration;

        public ConfigurationService(Interfaces.IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public async Task<bool> CreateConfigurationAsync(Configuration configuration)
        {
            try
            {
                return await _configuration.CreateConfigurationAsync(configuration);
            }
            catch { throw; }
        }

        public async Task<bool> DeleteConfigurationAsync(Configuration configuration)
        {
            try
            {
                return await _configuration.DeleteConfigurationAsync(configuration);
            }
            catch { throw; }
        }

        public async Task<Configuration> GetConfigurationAsync(int? configurationId = null, string? configKey = null, string? configValue = null, DateTime? lastUpdated = null, string? updatedBy = null)
        {
            try
            {
                return await _configuration.GetConfigurationAsync(configurationId, configKey, configValue, lastUpdated, updatedBy);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<Configuration>> GetConfigurationByConfigurationIdAsync(int configurationId)
        {
            try
            {
                return await _configuration.GetConfigurationByConfigurationIdAsync(configurationId);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<Configuration>> GetConfigurationsAsync()
        {
            try
            {
                return await _configuration.GetConfigurationsAsync();
            }
            catch { throw; }
        }

        public async Task<Configuration> UpdateConfigurationAsync(Configuration configuration)
        {
            try
            {
                return await _configuration.UpdateConfigurationAsync(configuration);
            }
            catch { throw; }
        }

    }
}
