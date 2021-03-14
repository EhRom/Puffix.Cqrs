using Puffix.Cqrs.Models;
using System;

namespace Puffix.Cqrs.Events
{
    /// <summary>
    /// Contrat pour la définition des évènementsde modification de données.
    /// </summary>
    /// <typeparam name="AggregateT">Type de l'agrégat.</typeparam>
    /// <typeparam name="IndexT">Type de l'index.</typeparam>
    public interface IModificationEvent<AggregateT, IndexT> : IEvent<AggregateT, IndexT>
        where AggregateT : IAggregate<IndexT>
        where IndexT : IComparable, IComparable<IndexT>, IEquatable<IndexT>
    { }
}
