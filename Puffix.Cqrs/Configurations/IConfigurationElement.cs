using System;

namespace Puffix.Cqrs.Configurations
{
    /// <summary>
    /// Contrat pour la définition des éléments de configuration.
    /// </summary>
    public interface IConfigurationElement
    {
        /// <summary>
        /// Nom du paramètre de configuration.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Valeur du paramètre de configuration.
        /// </summary>
        object Value { get; }

        /// <summary>
        /// Type du paramètre.
        /// </summary>
        /// <returns>Type du paramètre</returns>
        Type GetElementType();
    }
}
