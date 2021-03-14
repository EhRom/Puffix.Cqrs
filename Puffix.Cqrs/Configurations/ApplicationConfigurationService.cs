using System;
using System.Collections.Generic;
using System.IO;

namespace Puffix.Cqrs.Configurations
{
    /// <summary>
    /// Service de gestion des configurations.
    /// </summary>
    public class ApplicationConfigurationService : IApplicationConfigurationService
    {
        /// <summary>
        /// Conteneur des différentes configurations.
        /// </summary>
        private readonly IDictionary<string, IApplicationConfiguration> configurationsContainer;

        /// <summary>
        /// Constructeur.
        /// </summary>
        public ApplicationConfigurationService()
        {
            configurationsContainer = new Dictionary<string, IApplicationConfiguration>();
        }

        /// <summary>
        /// Recherche d'un paramètre de configuration.
        /// </summary>
        /// <typeparam name="ValueT">Type de la valeur du paramètre.</typeparam>
        /// <param name="key">Nom du paramètre.</param>
        /// <param name="configurationName">Nom de la configuration.</param>
        /// <returns>Valeur du paramètre.</returns>
        public ValueT GetParameterValue<ValueT>(string key, string configurationName = IApplicationConfigurationService.DEFAULT_CONFIGURATION_NAME)
        {
            // Contrôle de la configuration ciblée.
            if (!configurationsContainer.ContainsKey(configurationName))
                throw new ArgumentException($"The configuration '{configurationName}' is not registered");
            IApplicationConfiguration currentConfiguration = configurationsContainer[configurationName];
            currentConfiguration.EnsureIsLoaded();

            // Contrôle des paramètres.
            if (!currentConfiguration.Parameters.ContainsKey(key))
                throw new ArgumentException($"The configuration element {key} is not founnd.");
            if (currentConfiguration.Parameters[key].GetElementType() != typeof(ValueT))
                throw new ArgumentException($"Mismatch type between the requested type {typeof(ValueT)} and the configuration type {configurationsContainer[configurationName].Parameters[key].GetElementType()}.");

            return (ValueT)currentConfiguration.Parameters[key].Value;
        }

        /// <summary>
        /// Enregistrement d'une nouvelle configuration.
        /// </summary>
        /// <typeparam name="ApplicationConfigurationT">Type de la configuration.</typeparam>
        /// <param name="configurationFilePath">Chemin du fichier de configuration.</param>
        /// <param name="configurationFileLoader">Fichier de configuration à charger.</param>
        /// <param name="configurationName">Nom de la configuration.</param>
        public void Register<ApplicationConfigurationT>(string configurationFilePath, Func<string, Stream> configurationFileLoader = null, string configurationName = IApplicationConfigurationService.DEFAULT_CONFIGURATION_NAME)
            where ApplicationConfigurationT : IApplicationConfiguration
        {
            if (configurationsContainer.ContainsKey(configurationName))
                throw new ArgumentException($"The configuration '{configurationName}' is already registered");

            // Instanciation du conteneur et enregistrement.
            configurationsContainer[configurationName] = (ApplicationConfigurationT)Activator.CreateInstance(typeof(ApplicationConfigurationT));
            configurationsContainer[configurationName].Initialize(configurationFilePath, configurationFileLoader);
        }
    }
}
