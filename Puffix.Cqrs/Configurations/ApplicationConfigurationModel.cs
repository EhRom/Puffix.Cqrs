using System.Collections.Generic;

namespace Puffix.Cqrs.Configurations;

/// <summary>
/// Configuration item model.
/// </summary>
public class ApplicationConfigurationModel : IApplicationConfigurationModel<ConfigurationElement>
{
    /// <summary>
    /// Configuration items.
    /// </summary>
    public IEnumerable<ConfigurationElement> Parameters { get; set; }

    /// <summary>
    /// Create model to store configuration items.
    /// </summary>
    /// <returns></returns>
    public static ApplicationConfigurationModel CreateNew()
    {
        return new ApplicationConfigurationModel
        {
            Parameters = new List<ConfigurationElement>()
        };
    }
}
