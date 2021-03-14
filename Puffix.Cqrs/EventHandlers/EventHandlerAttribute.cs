using System;

namespace Puffix.Cqrs.EventHandlers
{
    /// <summary>
    /// Attribut pour détecter les intercepteurs d'évènements (utilisé pour la création automatique des intercepteurs d'évènements).
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]

    public class EventHandlerAttribute : Attribute
    { }
}
