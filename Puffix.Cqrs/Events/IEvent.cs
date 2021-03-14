using Puffix.Cqrs.Models;
using System;

namespace Puffix.Cqrs.Events
{
    /// <summary>
    /// Contrat pour la définition des évènements.
    /// </summary>
    /// <typeparam name="AggregateT">Type de l'agrégat.</typeparam>
    /// <typeparam name="IndexT">Type de l'index.</typeparam>
    public interface IEvent<AggregateT, IndexT> : IEvent
        where AggregateT : IAggregate<IndexT>
        where IndexT : IComparable, IComparable<IndexT>, IEquatable<IndexT>
    {
        /// <summary>
        /// Identifiant de l'agrégat.
        /// </summary>
        IndexT AggregateId { get; set; }

        /// <summary>
        /// Execution de l'évènement.
        /// </summary>
        /// <param name="aggregate">Agrégat.</param>
        void Apply(AggregateT aggregate);
    }

    /// <summary>
    /// Contrat de base pour la définition des évènements.
    /// </summary>
    public interface IEvent
    { }
}
