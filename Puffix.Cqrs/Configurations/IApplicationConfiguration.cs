using System;
using System.Collections.Generic;
using System.IO;

namespace Puffix.Cqrs.Configurations
{
    /// <summary>
    /// Configuration container contract.
    /// </summary>
    public interface IApplicationConfiguration
    {
        /// <summary>
        /// Configuration items.
        /// </summary>
        IReadOnlyDictionary<string, IConfigurationElement> Parameters { get; }

        /// <summary>
        /// Initialize configuration.
        /// </summary>
        /// <param name="configurationFile">Configuration file path.</param>
        /// <param name="configurationFileLoader">Function to load configuration file.</param>
        void Initialize(string configurationFile, Func<string, Stream> configurationFileLoader);

        /// <summary>
        /// Ensure that configuration is loaded.
        /// </summary>
        void EnsureIsLoaded();
    }
}
