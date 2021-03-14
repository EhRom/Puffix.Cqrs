using Puffix.Cqrs.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Puffix.Cqrs.Repositories
{
    /// <summary>
    /// Contrat de définition d'un répertoire de données.
    /// </summary>
    /// <typeparam name="AggregateT">Type de l'agrégat.</typeparam>
    /// <typeparam name="IndexT">Type de l'index.</typeparam>
    public interface IRepository<AggregateT, IndexT> : IQueryable<AggregateT>
        where AggregateT : IAggregate<IndexT>
        where IndexT : IComparable, IComparable<IndexT>, IEquatable<IndexT>
    {
        /// <summary>
        /// Test de l'existence d'un agrégat.
        /// </summary>
        /// <param name="aggregate">Agrégat.</param>
        /// <returns>Indique si l'agrégat existe ou non.</returns>
        Task<bool> ExistsAsync(AggregateT aggregate);

        /// <summary>
        /// Test de l'existence d'un agrégat.
        /// </summary>
        /// <param name="id">Identifiant de l'agrégat.</param>
        /// <returns>Indique si l'agrégat existe ou non.</returns>
        Task<bool> ExistsAsync(IndexT id);

        /// <summary>
        /// Recherche d'un agrégat.
        /// </summary>
        /// <param name="id">Identifiant de l'agrégat.</param>
        /// <returns>Agrégat.</returns>
        Task<AggregateT> GetByIdAsync(IndexT id);

        /// <summary>
        /// Recherche d'un agrégat.
        /// </summary>
        /// <param name="id">Identifiant de l'agrégat.</param>
        /// <returns>Agrégat ou valeur nulle à défaut.</returns>
        Task<AggregateT> GetByIdOrDefaultAsync(IndexT id);

        /// <summary>
        /// Génération de l'identifiant de l'agrégat.
        /// </summary>
        /// <param name="generateNextId">Fonction de génération du prochain identifiant.</param>
        /// <returns>Identifiant de l'agrégat.</returns>
        Task<IndexT> GetNextAggregatetIdAsync(Func<IndexT, IndexT> generateNextId);

        /// <summary>
        /// Création d'un aagrégat.
        /// </summary>
        /// <param name="aggregate">Agrégat.</param>
        Task CreateAsync(AggregateT aggregate);

        /// <summary>
        /// Mise à jour d'un aagrégat.
        /// </summary>
        /// <param name="aggregate">Agrégat.</param>
        Task UpdateAsync(AggregateT aggregate);

        /// <summary>
        /// Suppression d'un aagrégat.
        /// </summary>
        /// <param name="aggregate">Agrégat.</param>
        Task DeleteAsync(AggregateT aggregate);

        /// <summary>
        /// Sauvegarde du dépôt de données.
        /// </summary>
        Task SaveAsync();
    }
}
