using Puffix.Cqrs.Events;
using Puffix.Cqrs.Models;
using System;
using System.Threading.Tasks;

namespace Puffix.Cqrs.EventHandlers;

/// <summary>
/// Event handler associated with an aggregate contract.
/// </summary>
/// <typeparam name="EventT">Event type.</typeparam>
/// <typeparam name="AggregateT">Aggregate type.</typeparam>
/// <typeparam name="IndexT">Index type.</typeparam>
public interface IEventHandler<EventT, AggregateImplementationT, AggregateT, IndexT> : IEventHandler
    where EventT : IEvent<AggregateImplementationT, AggregateT, IndexT>
    where AggregateImplementationT : class, AggregateT
    where AggregateT : IAggregate<IndexT>
    where IndexT : IComparable, IComparable<IndexT>, IEquatable<IndexT>
{
    /// <summary>
    /// Handle event.
    /// </summary>
    /// <param name="aggregate">Aggregate.</param>
    /// <param name="handledEvent">Handled event.</param>
    /// <returns>Asynchronous task.</returns>
    Task HandleAsync(AggregateT aggregate, EventT handledEvent);
}

/// <summary>
/// Basic event handler base contract.
/// </summary>
/// <typeparam name="EventT">Event type to handle.</typeparam>
public interface IEventHandler<EventT> : IEventHandler
    where EventT : IEvent
{
    /// <summary>
    /// Handle event.
    /// </summary>
    /// <param name="handledEvent">Handled event.</param>
    /// <returns>Asynchronous task.</returns>
    Task HandleAsync(EventT handledEvent);
}

/// <summary>
/// Event handler base contract.
/// </summary>
public interface IEventHandler
{ }
