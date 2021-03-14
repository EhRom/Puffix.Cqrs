using Puffix.Cqrs.Models;
using System;

namespace Puffix.Cqrs.Repositories
{
    /// <summary>
    /// Contrat pour la configuratrion des répertoires de données.
    /// </summary>
    public interface IRepositoriesConfiguration
    {
        /// <summary>
        /// Recherche de l'implémentation du fournisseur de donées du répertoire.
        /// </summary>
        /// <typeparam name="AggregateT">Type d'agrégat.</typeparam>
        /// <typeparam name="IndexT">Type de l'idex.</typeparam>
        /// <param name="aggregateInfo">Informations sur l'agrégat.</param>
        /// <returns>Fournisseur de données du répertoire.</returns>
        IRepositoryProvider<AggregateT, IndexT> GetRepositoryProvider<AggregateT, IndexT>(AggregateInfo aggregateInfo)
            where AggregateT : IAggregate<IndexT>
            where IndexT : IComparable, IComparable<IndexT>, IEquatable<IndexT>;
    }
}
