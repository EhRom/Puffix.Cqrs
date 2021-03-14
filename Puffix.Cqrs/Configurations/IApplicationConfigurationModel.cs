using System.Collections.Generic;

namespace Puffix.Cqrs.Configurations
{
    /// <summary>
    /// Contrat de modèle pour le stockage des éléments de configuration.
    /// </summary>
    /// <typeparam name="ConfigurationElementT">Type des éléments de configuration.</typeparam>
    public interface IApplicationConfigurationModel<ConfigurationElementT>
        where ConfigurationElementT : IConfigurationElement
    {
        /// <summary>
        /// Liste des paramètres de configuration.
        /// </summary>
        IEnumerable<ConfigurationElementT> Parameters { get; set; }
    }
}
