namespace BankApplicationAPI.Models;

public partial class Configuration
{
    public int ConfigurationId { get; set; }

    public string ConfigKey { get; set; } = null!;

    public string ConfigValue { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime LastUpdated { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public virtual Employee UpdatedByNavigation { get; set; } = null!;
}
