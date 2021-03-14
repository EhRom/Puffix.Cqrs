using Puffix.Cqrs.Events;
using Puffix.Cqrs.Models;
using System;
using System.Threading.Tasks;

namespace Puffix.Cqrs.EventHandlers
{
    /// <summary>
    /// Contrat pour un intercepteur d'évènements associé à un agrégat.
    /// </summary>
    /// <typeparam name="EventT">Type de l'évènement à intercepter.</typeparam>
    /// <typeparam name="AggregateT">Type de l'agrégat.</typeparam>
    /// <typeparam name="IndexT">Type de l'index.</typeparam>
    public interface IEventHandler<EventT, AggregateT, IndexT> : IEventHandler
        where EventT : IEvent<AggregateT, IndexT>
        where AggregateT : IAggregate<IndexT>
        where IndexT : IComparable, IComparable<IndexT>, IEquatable<IndexT>
    {
        /// <summary>
        /// Interception de l'évènement.
        /// </summary>
        /// <param name="aggregate">Agrégat.</param>
        /// <param name="handledEvent">Evènement intercepté.</param>
        /// <returns>Tâche pour l'exécution asynchrone.</returns>
        Task HandleAsync(AggregateT aggregate, EventT handledEvent);
    }

    /// <summary>
    /// Contrat de base pour un intercepteur d'évènements simple.
    /// </summary>
    /// <typeparam name="EventT">Type de l'évènement à intercepter.</typeparam>
    public interface IEventHandler<EventT> : IEventHandler
        where EventT : IEvent
    {
        /// <summary>
        /// Interception de l'évènement.
        /// </summary>
        /// <param name="handledEvent">Evènement intercepté.</param>
        /// <returns>Tâche pour l'exécution asynchrone.</returns>
        Task HandleAsync(EventT handledEvent);
    }

    /// <summary>
    /// Contrat de base pour un intercepteur d'évènements.
    /// </summary>
    public interface IEventHandler
    { }
}
