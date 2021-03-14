using System;

namespace Puffix.Cqrs.Configurations
{
    /// <summary>
    /// Définition des éléments de configuration.
    /// </summary>
    public class ConfigurationElement : IConfigurationElement
    {
        /// <summary>
        /// Nom du paramètre de configuration.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Valeur du paramètre de configuration.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Type du paramètre.
        /// </summary>
        public string ElementType { get; set; }

        /// <summary>
        /// Création d'un nouvel élément de configuration.
        /// </summary>
        /// <param name="name">Nom.</param>
        /// <param name="elementValue">Valeur.</param>
        /// <param name="elementType">Type.</param>
        /// <returns></returns>
        public static ConfigurationElement CreateNew(string name, object elementValue, Type elementType)
        {
            return new ConfigurationElement
            {
                Name = name,
                Value = elementValue,
                ElementType = elementType.Name
            };
        }

        /// <summary>
        /// Type du paramètre.
        /// </summary>
        /// <returns>Type du paramètre</returns>
        public Type GetElementType()
        {
            return Type.GetType(ElementType);
        }
    }
}
