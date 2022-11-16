using Puffix.Cqrs.Models;
using System;

namespace Puffix.Cqrs.Events
{
    /// <summary>
    /// Event contract.
    /// </summary>
    /// <typeparam name="AggregateImplementationT">Aggregate implementation type.</typeparam>
    /// <typeparam name="AggregateT">Aggregate type.</typeparam>
    /// <typeparam name="IndexT">Index type.</typeparam>
    public interface IEvent<AggregateImplementationT, AggregateT, IndexT> : IEvent
        where AggregateImplementationT : class, AggregateT
        where AggregateT : IAggregate<IndexT>
        where IndexT : IComparable, IComparable<IndexT>, IEquatable<IndexT>
    {
        /// <summary>
        /// Aggregate id.
        /// </summary>
        IndexT AggregateId { get; set; }

        /// <summary>
        /// Execute event.
        /// </summary>
        /// <param name="aggregate">Aggregate.</param>
        void Apply(AggregateT aggregate);
    }

    /// <summary>
    /// Base event contract.
    /// </summary>
    public interface IEvent
    { }
}
