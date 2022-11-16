using System;

namespace Puffix.Cqrs.Configurations
{
    /// <summary>
    /// Configuration element contract.
    /// </summary>
    public interface IConfigurationElement
    {
        /// <summary>
        /// Parameter name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Paramter value.
        /// </summary>
        object Value { get; }

        /// <summary>
        /// Get the type pf the parameter.
        /// </summary>
        /// <returns>Parameter type.</returns>
        Type GetElementType();
    }
}
