using System;

namespace Puffix.Cqrs.Models;

/// <summary>
/// Indexable object contract.
/// </summary>
/// <typeparam name="IndexT">Index type.</typeparam>
public interface IIndexable<IndexT>
    where IndexT : IComparable, IComparable<IndexT>, IEquatable<IndexT>
{
    /// <summary>
    /// Index.
    /// </summary>
    IndexT Id { get; }

    /// <summary>
    /// Function for generating the next identifier of the aggregate.
    /// </summary>
    Func<IndexT, IndexT> GenerateNextId { get; }
}
