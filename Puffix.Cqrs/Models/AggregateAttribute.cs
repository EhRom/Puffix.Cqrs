using System;

namespace Puffix.Cqrs.Models
{
    /// <summary>
    /// Attribute to detect data model contracts (used for automatic creation of data directories).
    /// </summary>
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public class AggregateAttribute : Attribute
    { }
}
