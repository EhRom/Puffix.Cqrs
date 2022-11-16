using Puffix.Cqrs.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Puffix.Cqrs.Repositories
{
    /// <summary>
    /// Repository provider interface.
    /// </summary>
    /// <typeparam name="AggregateImplementationT">Aggregate implementation type.</typeparam>
    /// <typeparam name="AggregateT">Aggregate type.</typeparam>
    /// <typeparam name="IndexT">Index type</typeparam>
    public interface IRepositoryProvider<AggregateImplementationT, AggregateT, IndexT> : IQueryable<AggregateImplementationT>
        where AggregateImplementationT : class, AggregateT
        where AggregateT : IAggregate<IndexT>
        where IndexT : IComparable, IComparable<IndexT>, IEquatable<IndexT>
    {
        /// <summary>
        /// Test if aggregate exists.
        /// </summary>
        /// <param name="aggregate">Aggregate.</param>
        /// <returns>Indicates whether the aggregate exists or not.</returns>
        Task<bool> ExistsAsync(AggregateT aggregate);

        /// <summary>
        /// Test if aggregate exists.
        /// </summary>
        /// <param name="id">Aggregate id.</param>
        /// <returns>Indicates whether the aggregate exists or not.</returns>
        Task<bool> ExistsAsync(IndexT id);

        /// <summary>
        /// Get aggregate by id.
        /// </summary>
        /// <param name="id">Aggregate id.</param>
        /// <returns>Aggregate.</returns>
        Task<AggregateT> GetByIdAsync(IndexT id);

        /// <summary>
        /// Get aggregate by id.
        /// </summary>
        /// <param name="id">Aggregate id.</param>
        /// <returns>Aggregate or null value.</returns>
#if NET6_0_OR_GREATER
        Task<AggregateT?> GetByIdOrDefaultAsync(IndexT id);
#else
        Task<AggregateT> GetByIdOrDefaultAsync(IndexT id);
#endif

        /// <summary>
        /// Generate the next id.
        /// </summary>
        /// <param name="generateNextId">Function to generate the next id.</param>
        /// <returns>Aggregate id.</returns>
#if NET6_0_OR_GREATER
        Task<IndexT> GetNextAggregatetIdAsync(Func<IndexT?, IndexT> generateNextId);
#else
        Task<IndexT> GetNextAggregatetIdAsync(Func<IndexT, IndexT> generateNextId);
#endif

        /// <summary>
        /// Create aggregate.
        /// </summary>
        /// <param name="aggregate">Aggregate.</param>
        Task CreateAsync(AggregateT aggregate);

        /// <summary>
        /// Update aggregate.
        /// </summary>
        /// <param name="aggregate">Aggregate.</param>
        Task UpdateAsync(AggregateT aggregate);

        /// <summary>
        /// Delete aggregate.
        /// </summary>
        /// <param name="aggregate">Aggregate.</param>
        Task DeleteAsync(AggregateT aggregate);

        /// <summary>
        /// Save changes.
        /// </summary>
        Task SaveAsync();
    }
}
