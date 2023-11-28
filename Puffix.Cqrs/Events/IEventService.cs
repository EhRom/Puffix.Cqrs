using Puffix.Cqrs.Context;
using Puffix.Cqrs.Models;
using System;
using System.Threading.Tasks;

namespace Puffix.Cqrs.Events;

/// <summary>
/// Contract for the services to manage events.
/// /// </summary>
public interface IEventService
{
    /// <summary>
    /// Raise event.
    /// </summary>
    /// <typeparam name="AggregateImplementationT">Aggregate implementation type.</typeparam>
    /// <typeparam name="AggregateT">Aggregate type.</typeparam>
    /// <typeparam name="IndexT">Index type.</typeparam>
    /// <param name="raisedEvent">Event to raise.</param>
    /// <param name="applicationContext">Application context.</param>
    /// <returns>Aggregate.</returns>
    Task<AggregateT> Raise<AggregateImplementationT, AggregateT, IndexT>(IEvent<AggregateImplementationT, AggregateT, IndexT> raisedEvent, IApplicationContext applicationContext)
        where AggregateImplementationT : class, AggregateT
        where AggregateT : IAggregate<IndexT>
        where IndexT : IComparable, IComparable<IndexT>, IEquatable<IndexT>;
}
