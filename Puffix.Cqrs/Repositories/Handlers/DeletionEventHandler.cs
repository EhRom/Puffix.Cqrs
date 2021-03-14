using Puffix.Cqrs.Events;
using Puffix.Cqrs.Models;
using System;
using System.Threading.Tasks;

namespace Puffix.Cqrs.Repositories.Handlers
{
    /// <summary>
    /// Intercepteur d'évènement générique pour la suppression d'une donnée.
    /// </summary>
    /// <typeparam name="AggregateT">Type de l'agrégat.</typeparam>
    /// <typeparam name="IndexT">Type de l'index.</typeparam>
    public class DeletionEventHandler<AggregateT, IndexT> : IDeletionEventHandler<AggregateT, IndexT>
        where AggregateT : IAggregate<IndexT>
        where IndexT : IComparable, IComparable<IndexT>, IEquatable<IndexT>
    {
        /// <summary>
        /// Répertoire de données.
        /// </summary>
        private readonly IRepository<AggregateT, IndexT> repository;

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="repository">Répertoire de données.</param>
        public DeletionEventHandler(IRepository<AggregateT, IndexT> repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Interception de l'évènement.
        /// </summary>
        /// <param name="aggregate">Agrégat.</param>
        /// <param name="handledEvent">Evènement intercepté.</param>
        /// <returns>Tâche pour l'exécution asynchrone.</returns>
        public async Task HandleAsync(AggregateT aggregate, IDeletionEvent<AggregateT, IndexT> handledEvent)
        {
            if (await repository.ExistsAsync(aggregate.Id))
                await repository.DeleteAsync(aggregate);
        }
    }
}
