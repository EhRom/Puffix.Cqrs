using Puffix.Cqrs.Context;
using Puffix.Cqrs.Models;
using System;

namespace Puffix.Cqrs.Events;

/// <summary>
/// Event information contract.
/// </summary>
public interface IEventInformation : IAggregate<int>
{
    /// <summary>
    /// Event date.
    /// </summary>
    DateTime Date { get; }

    /// <summary>
    /// User.
    /// </summary>
    IApplicationUser User { get; }

    /// <summary>
    /// Event type.
    /// </summary>
    string EventType { get; }

    /// <summary>
    /// Event.
    /// </summary>
    IEvent Event { get; }
}
