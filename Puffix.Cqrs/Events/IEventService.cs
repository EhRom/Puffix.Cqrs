using Puffix.Cqrs.Context;
using Puffix.Cqrs.Models;
using System;
using System.Threading.Tasks;

namespace Puffix.Cqrs.Events
{
    /// <summary>
    /// Contrat pour la définition du service de gestion des évènements.
    /// </summary>
    public interface IEventService
    {
        /// <summary>
        /// Levée d'un évènement.
        /// </summary>
        /// <typeparam name="AggregateT">Type de l'agrégat.</typeparam>
        /// <typeparam name="IndexT">Type de l'index.</typeparam>
        /// <param name="raisedEvent">Evènement.</param>
        /// <param name="applicationContext">Contexte de l'application.</param>
        /// <returns>Agrégat.</returns>
        Task<AggregateT> Raise<AggregateT, IndexT>(IEvent<AggregateT, IndexT> raisedEvent, IApplicationContext applicationContext)
            where AggregateT : IAggregate<IndexT>
            where IndexT : IComparable, IComparable<IndexT>, IEquatable<IndexT>;
    }
}
