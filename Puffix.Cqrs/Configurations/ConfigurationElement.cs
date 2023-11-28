using System;

namespace Puffix.Cqrs.Configurations;

/// <summary>
/// Configuration element.
/// </summary>
public class ConfigurationElement : IConfigurationElement
{
    /// <summary>
    /// Parameter name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Paramter value.
    /// </summary>
    public object Value { get; set; }

    /// <summary>
    /// Paramter type.
    /// </summary>
    public string ElementType { get; set; }

    /// <summary>
    /// Create new parameter.
    /// </summary>
    /// <param name="name">Name.</param>
    /// <param name="elementValue">Value.</param>
    /// <param name="elementType">Type.</param>
    /// <returns>Configuration element.</returns>
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
    /// Get the type pf the parameter.
    /// </summary>
    /// <returns>Parameter type.</returns>
    public Type GetElementType()
    {
        return Type.GetType(ElementType);
    }
}
