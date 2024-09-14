using BankApplicationAPI.Models;

namespace BankApplicationAPI.Repository
{
    public class ConfigurationRepository : Interfaces.IConfiguration
    {
        public Task<bool> CreateConfigurationAsync(Configuration configuration)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteConfigurationAsync(Configuration configuration)
        {
            throw new NotImplementedException();
        }

        public Task<Configuration> GetConfigurationAsync(int? ConfigurationId = null, string? ConfigKey = null, string? ConfigValue = null, DateTime? LastUpdated = null, string? UpdatedBy = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Configuration>> GetConfigurationByConfigurationIdAsync(int ConfigurationId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Configuration>> GetConfigurationsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Configuration> UpdateConfigurationAsync(Configuration configuration)
        {
            throw new NotImplementedException();
        }
    }
}
