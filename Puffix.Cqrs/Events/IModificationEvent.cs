using Puffix.Cqrs.Models;
using System;

namespace Puffix.Cqrs.Events
{
    /// <summary>
    /// Modification event contract.
    /// </summary>
    /// <typeparam name="AggregateImplementationT">Aggregate implementation type.</typeparam>
    /// <typeparam name="AggregateT">Aggregate type.</typeparam>
    /// <typeparam name="IndexT">Index type.</typeparam>
    public interface IModificationEvent<AggregateImplementationT, AggregateT, IndexT> : IEvent<AggregateImplementationT, AggregateT, IndexT>
        where AggregateImplementationT : class, AggregateT
        where AggregateT : IAggregate<IndexT>
        where IndexT : IComparable, IComparable<IndexT>, IEquatable<IndexT>
    { }
}
