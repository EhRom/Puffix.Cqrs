using Puffix.Cqrs.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Puffix.Cqrs.Repositories
{
    /// <summary>
    /// Répertoire de données.
    /// </summary>
    /// <typeparam name="AggregateT">Type de l'agrégat.</typeparam>
    /// <typeparam name="IndexT">Type de l'index.</typeparam>
    public class Repository<AggregateT, IndexT> : IRepository<AggregateT, IndexT>
         where AggregateT : IAggregate<IndexT>
         where IndexT : IComparable, IComparable<IndexT>, IEquatable<IndexT>
    {
        /// <summary>
        /// Fournisseur de répertoire de données.
        /// </summary>
        private readonly IRepositoryProvider<AggregateT, IndexT> provider;

        /// <summary>
        /// Type de l'agrégat.
        /// </summary>
        public Type ElementType => provider.ElementType;

        /// <summary>
        /// Expression.
        /// </summary>
        public Expression Expression => provider.Expression;

        /// <summary>
        /// Fournisseur de requête.
        /// </summary>
        public IQueryProvider Provider => provider.Provider;

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="repositoriesConfiguration">Configuratrion des répertoires de données.</param>
        /// <param name="repositoryService">Service de gestion des répertoires de données.</param>
        public Repository(IRepositoriesConfiguration repositoriesConfiguration, IRepositoryService repositoryService)
        {
            // Recherche des informations de l'égrégat.
            AggregateInfo aggregateInfo = repositoryService.GetInfo<AggregateT>();

            // Récupération du fournisseur de données pour l'agrégat.
            provider = repositoriesConfiguration.GetRepositoryProvider<AggregateT, IndexT>(aggregateInfo);
        }

        /// <summary>
        /// Test de l'existence d'un agrégat.
        /// </summary>
        /// <param name="aggregate">Agrégat.</param>
        /// <returns>Indique si l'agrégat existe ou non.</returns>
        public async Task<bool> ExistsAsync(AggregateT aggregate)
        {
            return await provider.ExistsAsync(aggregate);
        }

        /// <summary>
        /// Test de l'existence d'un agrégat.
        /// </summary>
        /// <param name="id">Identifiant de l'agrégat.</param>
        /// <returns>Indique si l'agrégat existe ou non.</returns>
        public async Task<bool> ExistsAsync(IndexT id)
        {
            return await provider.ExistsAsync(id);
        }

        /// <summary>
        /// Recherche d'un agrégat.
        /// </summary>
        /// <param name="id">Identifiant de l'agrégat.</param>
        /// <returns>Agrégat.</returns>
        public async Task<AggregateT> GetByIdAsync(IndexT id)
        {
            return await provider.GetByIdAsync(id);
        }

        /// <summary>
        /// Recherche d'un agrégat.
        /// </summary>
        /// <param name="id">Identifiant de l'agrégat.</param>
        /// <returns>Agrégat ou valeur nulle à défaut.</returns>
        public async Task<AggregateT> GetByIdOrDefaultAsync(IndexT id)
        {
            return await provider.GetByIdOrDefaultAsync(id);
        }

        /// <summary>
        /// Récupération d'un énumérateur.
        /// </summary>
        /// <returns>Enumérateur.</returns>
        public IEnumerator<AggregateT> GetEnumerator()
        {
            return provider.GetEnumerator();
        }

        /// <summary>
        /// Récupération d'un énumérateur.
        /// </summary>
        /// <returns>Enumérateur.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return provider.GetEnumerator();
        }

        /// <summary>
        /// Génération de l'identifiant de l'agrégat.
        /// </summary>
        /// <param name="generateNextId">Fonction de génération du prochain identifiant.</param>
        /// <returns>Identifiant de l'agrégat.</returns>
        public async Task<IndexT> GetNextAggregatetIdAsync(Func<IndexT, IndexT> generateNextId)
        {
            return await provider.GetNextAggregatetIdAsync(generateNextId);
        }

        /// <summary>
        /// Création d'un aagrégat.
        /// </summary>
        /// <param name="aggregate">Agrégat.</param>
        public async Task CreateAsync(AggregateT aggregate)
        {
            await provider.CreateAsync(aggregate);
        }

        /// <summary>
        /// Mise à jour d'un aagrégat.
        /// </summary>
        /// <param name="aggregate">Agrégat.</param>
        public async Task UpdateAsync(AggregateT aggregate)
        {
            await provider.UpdateAsync(aggregate);
        }

        /// <summary>
        /// Suppression d'un aagrégat.
        /// </summary>
        /// <param name="aggregate">Agrégat.</param>
        public async Task DeleteAsync(AggregateT aggregate)
        {
            await provider.DeleteAsync(aggregate);
        }

        /// <summary>
        /// Sauvegarde du dépôt de données.
        /// </summary>
        public async Task SaveAsync()
        {
            await provider.SaveAsync();
        }
    }
}
