using System;
using System.Collections.Generic;
using System.Linq;

namespace Puffix.Cqrs.EventHandlers;

/// <summary>
/// Event handler management service.
/// </summary>
public class EventHandlerService : IEventHandlerService
{
    private readonly IDictionary<Type, List<EventHandlerInfo>> eventHandlerInfoContainer;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="eventHandlerInfoContainer">Event handler container.</param>
    public EventHandlerService(IDictionary<Type, List<EventHandlerInfo>> eventHandlerInfoContainer)
    {
        this.eventHandlerInfoContainer = eventHandlerInfoContainer;
    }

    /// <summary>
    /// Search for event handlers associated with the event
    /// </summary>
    /// <param name="eventType">Event type.</param>
    /// <returns>Event handlers associated with the event.</returns>
    public IEnumerable<EventHandlerInfo> GetEventHandlers(Type eventType)
    {
        return eventHandlerInfoContainer.ContainsKey(eventType) ?
                    eventHandlerInfoContainer[eventType] :
                    Enumerable.Empty<EventHandlerInfo>();
    }
}
