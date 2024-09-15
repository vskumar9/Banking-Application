using BankApplicationAPI.Models;

namespace BankApplicationAPI.Interfaces
{
    public interface IConfiguration
    {
        Task<IEnumerable<Configuration>> GetConfigurationsAsync();
        Task<IEnumerable<Configuration>> GetConfigurationByConfigurationIdAsync(int ConfigurationId);
        Task<Configuration> UpdateConfigurationAsync(Configuration configuration);
        Task<Boolean> DeleteConfigurationAsync(Configuration configuration);
        Task<Boolean> CreateConfigurationAsync(Configuration configuration);
        Task<Configuration> GetConfigurationAsync(int? ConfigurationId = null,
                                      string? ConfigKey = null,
                                      string? ConfigValue = null,
                                      DateTime? LastUpdated = null,
                                      string? UpdatedBy = null);
    }
}
