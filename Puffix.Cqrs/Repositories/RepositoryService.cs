using Puffix.Cqrs.Models;
using System;
using System.Collections.Generic;

namespace Puffix.Cqrs.Repositories
{

    /// <summary>
    /// Service pour la gestion des répertoires de données.
    /// </summary>
    public class RepositoryService : IRepositoryService
    {
        /// <summary>
        /// Fonction pour la résolution de classes via le conteneur IoC.
        /// </summary>
        private readonly Func<Type, object> resolver;

        /// <summary>
        /// Dictionnaire des informations sur les agrégats enregistrés.
        /// </summary>
        private readonly IDictionary<Type, AggregateInfo> aggregateInfoContainer;

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="resolver">Fonction pour la résolution de classes via le conteneur IoC.</param>
        /// <param name="aggregateInfoContainer">Dictionnaire des informations sur les agrégats enregistrés.</param>
        public RepositoryService(Func<Type, object> resolver, IDictionary<Type, AggregateInfo> aggregateInfoContainer)
        {
            // Référencement des éléments.
            this.resolver = resolver;
            this.aggregateInfoContainer = aggregateInfoContainer;
        }

        /// <summary>
        /// Recherche des informations de l'agrégat.
        /// </summary>
        /// <typeparam name="AggregateT">Type de l'agrégat.</typeparam>
        /// <returns>Informations de l'agrégat.</returns>
        public AggregateInfo GetInfo<AggregateT>()
        {
            if (!aggregateInfoContainer.ContainsKey(typeof(AggregateT)))
                throw new ArgumentOutOfRangeException($"The aggregate {typeof(AggregateT)} is not registered.");

            return aggregateInfoContainer[typeof(AggregateT)];
        }

        /// <summary>
        /// Récupération d'une source de données pour un agrégat.
        /// </summary>
        /// <typeparam name="AggregateT">Type de l'agrégat.</typeparam>
        /// <typeparam name="IndexT">Type de l'index.</typeparam>
        /// <returns>Source de données de l'agrégat.</returns>
        public IRepository<AggregateImplementationT, AggregateT, IndexT> GetRepository<AggregateImplementationT, AggregateT, IndexT>()
            where AggregateImplementationT : class, AggregateT
            where AggregateT : IAggregate<IndexT>
            where IndexT : IComparable, IComparable<IndexT>, IEquatable<IndexT>
        {
            return resolver(typeof(IRepository<AggregateImplementationT, AggregateT, IndexT>)) as IRepository<AggregateImplementationT, AggregateT, IndexT>;
        }
    }
}
