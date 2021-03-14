using System;
using System.Collections.Generic;
using System.IO;

namespace Puffix.Cqrs.Configurations
{
    /// <summary>
    /// Conteneur de configuration de base (ne charge pas les données).
    /// </summary>
    public abstract class ApplicationConfiguration : IApplicationConfiguration
    {
        /// <summary>
        /// Emplacement du fichier de configuration.
        /// </summary>
        protected string configurationFile;

        /// <summary>
        /// Fonction pour le chargement du fichier de configuration.
        /// </summary>
        protected Func<string, Stream> configurationFileLoader;

        /// <summary>
        /// Conteneur de la configuration.
        /// </summary>
        protected IReadOnlyDictionary<string, IConfigurationElement> parameters;

        /// <summary>
        /// Liste des éléments de configuration.
        /// </summary>
        public IReadOnlyDictionary<string, IConfigurationElement> Parameters => parameters;

        /// <summary>
        /// Initialisation de la configuration pour le chargement.
        /// </summary>
        /// <param name="configurationFile">Fichier de configuration à charger.</param>
        /// <param name="configurationFileLoader">Fichier de configuration à charger.</param>
        /// <returns>Tâche asynchrone.</returns>
        public void Initialize(string configurationFile, Func<string, Stream> configurationFileLoader)
        {
            this.configurationFile = configurationFile;
            this.configurationFileLoader = configurationFileLoader;
        }

        /// <summary>
        /// Contrôle du chargement de la configuration.
        /// </summary>
        public abstract void EnsureIsLoaded();
    }
}
