using System;

namespace Puffix.Cqrs.Models
{
    /// <summary>
    /// Attribut pour détecter les contrats de modèles de données (utilisé pour la création automatique des repertoires de données).
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public class AggregateAttribute : Attribute
    { }
}
