using Puffix.Cqrs.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Puffix.Cqrs.Repositories;
using System.Threading.Tasks;

namespace Puffix.Cqrs.Basic.Repositories
{
    /// <summary>
    /// Fournisseur INMemory de répetoire de données.
    /// </summary>
    /// <typeparam name="AggregateT">Type de l'agrégat.</typeparam>
    /// <typeparam name="IndexT">Type de l'index.</typeparam>
    public class InMemoryRepositoryProvider<AggregateT, IndexT> : IRepositoryProvider<AggregateT, IndexT>
        where AggregateT : IAggregate<IndexT>
        where IndexT : IComparable, IComparable<IndexT>, IEquatable<IndexT>
    {
        /// <summary>
        /// Dictionnaire de données.
        /// </summary>
        private readonly IDictionary<IndexT, AggregateT> inMemoryData = new Dictionary<IndexT, AggregateT>();

        /// <summary>
        /// Type des éléments stockés.
        /// </summary>
        public Type ElementType => typeof(AggregateT);

        /// <summary>
        /// Expression.
        /// </summary>
        public Expression Expression => inMemoryData.Values.AsQueryable().Expression;

        /// <summary>
        /// Constructeur de requête.
        /// </summary>
        public IQueryProvider Provider => inMemoryData.Values.AsQueryable().Provider;

        /// <summary>
        /// Test de l'existence d'un agrégat.
        /// </summary>
        /// <param name="aggregate">Agrégat.</param>
        /// <returns>Indique si l'agrégat existe ou non.</returns>
        public Task<bool> ExistsAsync(AggregateT aggregate)
        {
            return Task.FromResult(inMemoryData.ContainsKey(aggregate.Id));
        }

        /// <summary>
        /// Test de l'existence d'un agrégat.
        /// </summary>
        /// <param name="id">Identifiant de l'agrégat.</param>
        /// <returns>Indique si l'agrégat existe ou non.</returns>
        public Task<bool> ExistsAsync(IndexT id)
        {
            return Task.FromResult(inMemoryData.ContainsKey(id));
        }

        /// <summary>
        /// Recherche d'un agrégat.
        /// </summary>
        /// <param name="id">Identifiant de l'agrégat.</param>
        /// <returns>Agrégat.</returns>
        public Task<AggregateT> GetByIdAsync(IndexT id)
        {
            if (inMemoryData.ContainsKey(id))
                return Task.FromResult(inMemoryData[id]);
            else
                throw new Exception($"Element with id {id} of type {typeof(AggregateT).FullName} is not found.");
        }
        /// <summary>
        /// Génération de l'identifiant de l'agrégat.
        /// </summary>
        /// <param name="generateNextId">Fonction de génération du prochain identifiant.</param>
        /// <returns>Identifiant de l'agrégat.</returns>
        public Task<IndexT> GetNextAggregatetIdAsync(Func<IndexT, IndexT> generateNextId)
        {
            // Recherche du dernier index utilisé.
            IndexT lastId;
            if (inMemoryData.Count == 0)
                lastId = default;
            else
                lastId = inMemoryData.Keys.Max();

            // Spécification de l'index de l'agrégat.
            IndexT nextId = generateNextId(lastId);
            return Task.FromResult(nextId);
        }

        /// <summary>
        /// Recherche d'un agrégat.
        /// </summary>
        /// <param name="id">Identifiant de l'agrégat.</param>
        /// <returns>Agrégat ou valeur nulle à défaut.</returns>
        public Task<AggregateT> GetByIdOrDefaultAsync(IndexT id)
        {
            AggregateT result;
            if (inMemoryData.ContainsKey(id))
                result = inMemoryData[id];
            else
                result = default;

            return Task.FromResult(result);
        }

        /// <summary>
        /// Récupération d'un énumérateur.
        /// </summary>
        /// <returns>Enumérateur.</returns>
        public IEnumerator<AggregateT> GetEnumerator()
        {
            return inMemoryData.Values.GetEnumerator();
        }

        /// <summary>
        /// Récupération d'un énumérateur.
        /// </summary>
        /// <returns>Enumérateur.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return inMemoryData.Values.GetEnumerator();
        }

        /// <summary>
        /// Création d'un aagrégat.
        /// </summary>
        /// <param name="aggregate">Agrégat.</param>
        public Task CreateAsync(AggregateT aggregate)
        {
            if (inMemoryData.ContainsKey(aggregate.Id))
                throw new Exception($"Element with id {aggregate.Id} of type {typeof(AggregateT).FullName} already exists.");

            inMemoryData[aggregate.Id] = aggregate;

            return Task.FromResult(Type.Missing);
        }

        /// <summary>
        /// Mise à jour d'un aagrégat.
        /// </summary>
        /// <param name="aggregate">Agrégat.</param>
        public Task UpdateAsync(AggregateT aggregate)
        {
            if (!inMemoryData.ContainsKey(aggregate.Id))
                throw new Exception($"Element with id {aggregate.Id} of type {typeof(AggregateT).FullName} does not exist.");

            inMemoryData[aggregate.Id] = aggregate;

            return Task.FromResult(Type.Missing);
        }

        /// <summary>
        /// Suppression d'un aagrégat.
        /// </summary>
        /// <param name="aggregate">Agrégat.</param>
        public Task DeleteAsync(AggregateT aggregate)
        {
            if (!inMemoryData.ContainsKey(aggregate.Id))
                throw new Exception($"Element with id {aggregate.Id} of type {typeof(AggregateT).FullName} does not exist.");

            inMemoryData.Remove(aggregate.Id);

            return Task.FromResult(Type.Missing);
        }

        /// <summary>
        /// Sauvegarde du dépôt de données.
        /// </summary>
        public Task SaveAsync()
        {
            return Task.FromResult(Type.Missing);
        }
    }
}
