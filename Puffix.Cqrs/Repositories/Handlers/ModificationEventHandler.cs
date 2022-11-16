using Puffix.Cqrs.Events;
using Puffix.Cqrs.Models;
using System;
using System.Threading.Tasks;

namespace Puffix.Cqrs.Repositories.Handlers
{
    /// <summary>
    /// Intercepteur d'évènement générique pour la modification d'une donnée.
    /// </summary>
    /// <typeparam name="AggregateT">Type de l'agrégat.</typeparam>
    /// <typeparam name="IndexT">Type de l'index.</typeparam>
    public class ModificationEventHandler<AggregateImplementationT, AggregateT, IndexT> : IModificationEventHandler<AggregateImplementationT, AggregateT, IndexT>
        where AggregateImplementationT : class, AggregateT
        where AggregateT : IAggregate<IndexT>
        where IndexT : IComparable, IComparable<IndexT>, IEquatable<IndexT>
    {
        /// <summary>
        /// Répertoire de données.
        /// </summary>
        private readonly IRepository<AggregateImplementationT, AggregateT, IndexT> repository;

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="repository">Répertoire de données.</param>
        public ModificationEventHandler(IRepository<AggregateImplementationT, AggregateT, IndexT> repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Interception de l'évènement.
        /// </summary>
        /// <param name="aggregate">Agrégat.</param>
        /// <param name="handledEvent">Evènement intercepté.</param>
        /// <returns>Tâche pour l'exécution asynchrone.</returns>
        public async Task HandleAsync(AggregateT aggregate, IModificationEvent<AggregateImplementationT, AggregateT, IndexT> handledEvent)
        {
            if (await repository.ExistsAsync(aggregate.Id))
                await repository.UpdateAsync(aggregate);
            else
                await repository.CreateAsync(aggregate);
        }
    }
}
