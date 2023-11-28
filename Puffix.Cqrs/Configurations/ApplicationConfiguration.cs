using System;
using System.Collections.Generic;
using System.IO;

namespace Puffix.Cqrs.Configurations;

/// <summary>
/// Base configuration container (does not load data).
/// </summary>
public abstract class ApplicationConfiguration : IApplicationConfiguration
{
    /// <summary>
    /// Configuration file path.
    /// </summary>
    protected string configurationFile;

    /// <summary>
    /// Function to load configuration file.
    /// </summary>
    protected Func<string, Stream> configurationFileLoader;

    /// <summary>
    /// Configuration container.
    /// </summary>
    protected IReadOnlyDictionary<string, IConfigurationElement> parameters;

    /// <summary>
    /// Configuration items.
    /// </summary>
    public IReadOnlyDictionary<string, IConfigurationElement> Parameters => parameters;

    /// <summary>
    /// Initialize configuration.
    /// </summary>
    /// <param name="configurationFile">Configuration file path.</param>
    /// <param name="configurationFileLoader">Function to load configuration file.</param>
    public void Initialize(string configurationFile, Func<string, Stream> configurationFileLoader)
    {
        this.configurationFile = configurationFile;
        this.configurationFileLoader = configurationFileLoader;
    }

    /// <summary>
    /// Ensure that configuration is loaded.
    /// </summary>
    public abstract void EnsureIsLoaded();
}
