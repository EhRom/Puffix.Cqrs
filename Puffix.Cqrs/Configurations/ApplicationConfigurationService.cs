using System;
using System.Collections.Generic;
using System.IO;

namespace Puffix.Cqrs.Configurations
{
    /// <summary>
    /// Service to manage configuration.
    /// </summary>
    public class ApplicationConfigurationService : IApplicationConfigurationService
    {
        /// <summary>
        /// Configuration collection container.
        /// </summary>
        private readonly IDictionary<string, IApplicationConfiguration> configurationsContainer;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ApplicationConfigurationService()
        {
            configurationsContainer = new Dictionary<string, IApplicationConfiguration>();
        }

        /// <summary>
        /// Get configuration parameter.
        /// </summary>
        /// <typeparam name="ValueT">Parameter value type.</typeparam>
        /// <param name="key">Parameter name.</param>
        /// <param name="configurationName">Configuration name.</param>
        /// <returns>Typed parameter value.</returns>
        public ValueT GetParameterValue<ValueT>(string key, string configurationName = IApplicationConfigurationService.DEFAULT_CONFIGURATION_NAME)
        {
            if (!configurationsContainer.ContainsKey(configurationName))
                throw new ArgumentException($"The configuration '{configurationName}' is not registered");
            IApplicationConfiguration currentConfiguration = configurationsContainer[configurationName];
            currentConfiguration.EnsureIsLoaded();

            if (!currentConfiguration.Parameters.ContainsKey(key))
                throw new ArgumentException($"The configuration element {key} is not founnd.");
            if (currentConfiguration.Parameters[key].GetElementType() != typeof(ValueT))
                throw new ArgumentException($"Mismatch type between the requested type {typeof(ValueT)} and the configuration type {configurationsContainer[configurationName].Parameters[key].GetElementType()}.");

            return (ValueT)currentConfiguration.Parameters[key].Value;
        }

        /// <summary>
        /// Store new configuration set.
        /// </summary>
        /// <typeparam name="ApplicationConfigurationT">Configuration type.</typeparam>
        /// <param name="configurationFilePath">Configuration file path.</param>
        /// <param name="configurationFileLoader">Function to load configuration file.</param>
        /// <param name="configurationName">Configuration name.</param>
        public void Register<ApplicationConfigurationT>(string configurationFilePath, Func<string, Stream> configurationFileLoader = null, string configurationName = IApplicationConfigurationService.DEFAULT_CONFIGURATION_NAME)
            where ApplicationConfigurationT : IApplicationConfiguration
        {
            if (configurationsContainer.ContainsKey(configurationName))
                throw new ArgumentException($"The configuration '{configurationName}' is already registered");

            configurationsContainer[configurationName] = (ApplicationConfigurationT)Activator.CreateInstance(typeof(ApplicationConfigurationT));
            configurationsContainer[configurationName].Initialize(configurationFilePath, configurationFileLoader);
        }
    }
}
