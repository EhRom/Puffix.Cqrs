using System;
using System.Collections.Generic;
using System.IO;

namespace Puffix.Cqrs.Configurations
{
    /// <summary>
    /// Contrat pour les conteneurs de configuration.
    /// </summary>
    public interface IApplicationConfiguration
    {
        /// <summary>
        /// Liste des éléments de configuration.
        /// </summary>
        IReadOnlyDictionary<string, IConfigurationElement> Parameters { get; }

        /// <summary>
        /// Initialisation de la configuration pour le chargement.
        /// </summary>
        /// <param name="configurationFile">Fichier de configuration à charger.</param>
        /// <param name="configurationFileLoader">Fichier de configuration à charger.</param>
        /// <returns>Tâche asynchrone.</returns>
        void Initialize(string configurationFile, Func<string, Stream> configurationFileLoader);

        /// <summary>
        /// Contrôle du chargement de la configuration.
        /// </summary>
        void EnsureIsLoaded();
    }
}
