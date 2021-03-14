using System.Collections.Generic;

namespace Puffix.Cqrs.Configurations
{
    /// <summary>
    /// Modèle pour le stockage des éléments de configuration.
    /// </summary>
    /// <typeparam name="ConfigurationElementT">Type des éléments de configuration.</typeparam>
    public class ApplicationConfigurationModel : IApplicationConfigurationModel<ConfigurationElement>
    {
        /// <summary>
        /// Liste des paramètres de configuration.
        /// </summary>
        public IEnumerable<ConfigurationElement> Parameters { get; set; }

        /// <summary>
        /// Création d'un nouveau modèle pour la stockage des éléments de configuration.
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
}
