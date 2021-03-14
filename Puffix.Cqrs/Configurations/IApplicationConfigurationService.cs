using System;
using System.IO;

namespace Puffix.Cqrs.Configurations
{
    /// <summary>
    /// Contrat pour le service de gestion des configurations.
    /// </summary>
    public interface IApplicationConfigurationService
    {
        /// <summary>
        /// Nom de la configuration par défaut.
        /// </summary>
        internal const string DEFAULT_CONFIGURATION_NAME = "default";

        /// <summary>
        /// Recherche d'un paramètre de configuration.
        /// </summary>
        /// <typeparam name="ValueT">Type de la valeur du paramètre.</typeparam>
        /// <param name="key">Nom du paramètre.</param>
        /// <param name="configurationName">Nom de la configuration.</param>
        /// <returns>Valeur du paramètre.</returns>
        ValueT GetParameterValue<ValueT>(string key, string configurationName = DEFAULT_CONFIGURATION_NAME);

        /// <summary>
        /// Enregistrement d'une nouvelle configuration.
        /// </summary>
        /// <typeparam name="ApplicationConfigurationT">Type de la configuration.</typeparam>
        /// <param name="configurationFilePath">Chemin du fichier de configuration.</param>
        /// <param name="configurationFileLoader">Fichier de configuration à charger.</param>
        /// <param name="configurationName">Nom de la configuration.</param>
        void Register<ApplicationConfigurationT>(string configurationFilePath, Func<string, Stream> configurationFileLoader = null, string configurationName = DEFAULT_CONFIGURATION_NAME)
            where ApplicationConfigurationT : IApplicationConfiguration;
    }
}
