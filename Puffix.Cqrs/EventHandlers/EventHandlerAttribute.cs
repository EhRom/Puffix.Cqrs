using System;

namespace Puffix.Cqrs.EventHandlers
{
    /// <summary>
    /// Attribute to detect events hendler (used for automatic creation of events handler).
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]

    public class EventHandlerAttribute : Attribute
    { }
}
