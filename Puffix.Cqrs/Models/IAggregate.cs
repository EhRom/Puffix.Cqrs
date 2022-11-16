using System;

namespace Puffix.Cqrs.Models
{
    /// <summary>
    /// Aggregate contract.
    /// </summary>
    /// <typeparam name="IndexT">Index type.</typeparam>
    public interface IAggregate<IndexT> : IIndexable<IndexT>, IAggregate
        where IndexT : IComparable, IComparable<IndexT>, IEquatable<IndexT>
    { }

    /// <summary>
    /// Base aggregate contract.
    /// </summary>
    public interface IAggregate
    { }
}
