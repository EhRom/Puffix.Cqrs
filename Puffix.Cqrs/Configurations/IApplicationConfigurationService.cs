using System;
using System.IO;

namespace Puffix.Cqrs.Configurations;

/// <summary>
/// Service contract to manage configuration.
/// </summary>
public interface IApplicationConfigurationService
{
    /// <summary>
    /// Default configuration name.
    /// </summary>
    internal const string DEFAULT_CONFIGURATION_NAME = "default";

    /// <summary>
    /// Get configuration parameter.
    /// </summary>
    /// <typeparam name="ValueT">Parameter value type.</typeparam>
    /// <param name="key">Parameter name.</param>
    /// <param name="configurationName">Configuration name.</param>
    /// <returns>Typed parameter value.</returns>
    ValueT GetParameterValue<ValueT>(string key, string configurationName = DEFAULT_CONFIGURATION_NAME);

    /// <summary>
    /// Store new configuration set.
    /// </summary>
    /// <typeparam name="ApplicationConfigurationT">Configuration type.</typeparam>
    /// <param name="configurationFilePath">Configuration file path.</param>
    /// <param name="configurationFileLoader">Function to load configuration file.</param>
    /// <param name="configurationName">Configuration name.</param>
    void Register<ApplicationConfigurationT>(string configurationFilePath, Func<string, Stream> configurationFileLoader = null, string configurationName = DEFAULT_CONFIGURATION_NAME)
        where ApplicationConfigurationT : IApplicationConfiguration;
}
