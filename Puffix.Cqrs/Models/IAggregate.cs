using System;

namespace Puffix.Cqrs.Models
{
    /// <summary>
    /// Contrat de définition d'un agrégat.
    /// </summary>
    /// <typeparam name="IndexT">Type de l'index.</typeparam>
    public interface IAggregate<IndexT> : IIndexable<IndexT>, IAggregate
        where IndexT : IComparable, IComparable<IndexT>, IEquatable<IndexT>
    { }

    /// <summary>
    /// Contrat de base de définition d'un  agrégat.
    /// </summary>
    public interface IAggregate
    { }
}
