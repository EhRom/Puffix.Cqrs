using Puffix.Cqrs.Models;
using System;

namespace Puffix.Cqrs.Repositories
{
    /// <summary>
    /// Contrat pour le service de gestion des sources de données.
    /// </summary>
    public interface IRepositoryService
    {
        /// <summary>
        /// Recherche des informations de l'agrégat.
        /// </summary>
        /// <typeparam name="AggregateT">Type de l'agrégat.</typeparam>
        /// <returns>Informations de l'agrégat.</returns>
        AggregateInfo GetInfo<AggregateT>();

        /// <summary>
        /// Récupération d'une source de données pour un agrégat.
        /// </summary>
        /// <typeparam name="TAggregate">Type de l'agrégat.</typeparam>
        /// <typeparam name="TIndex">Type de l'index.</typeparam>
        /// <returns>Source de données de l'agrégat.</returns>
        IRepository<AggregateImplementationT, AggregateT, IndexT> GetRepository<AggregateImplementationT, AggregateT, IndexT>()
            where AggregateImplementationT : class, AggregateT
            where AggregateT : IAggregate<IndexT>
            where IndexT : IComparable, IComparable<IndexT>, IEquatable<IndexT>;
    }
}