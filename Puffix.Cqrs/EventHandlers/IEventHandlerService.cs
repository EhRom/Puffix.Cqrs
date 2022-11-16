using System;
using System.Collections.Generic;

namespace Puffix.Cqrs.EventHandlers
{
    /// <summary>
    /// Event handler management service contract.
    /// </summary>
    public interface IEventHandlerService
    {
        /// <summary>
        /// Search for event handlers associated with the event
        /// </summary>
        /// <param name="eventType">Event type.</param>
        /// <returns>Event handlers associated with the event.</returns>
        IEnumerable<EventHandlerInfo> GetEventHandlers(Type eventType);
    }
}
