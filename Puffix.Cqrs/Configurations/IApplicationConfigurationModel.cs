using System.Collections.Generic;

namespace Puffix.Cqrs.Configurations;

/// <summary>
/// Configuration item model contract.
/// </summary>
/// <typeparam name="ConfigurationElementT">Type of the element of the configuration.</typeparam>
public interface IApplicationConfigurationModel<ConfigurationElementT>
    where ConfigurationElementT : IConfigurationElement
{
    /// <summary>
    /// List of paramters.
    /// </summary>
    IEnumerable<ConfigurationElementT> Parameters { get; set; }
}
