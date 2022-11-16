using Puffix.Cqrs.Models;
using Puffix.Cqrs.Repositories;
using System;

namespace Puffix.Cqrs.Basic.Repositories
{
    /// <summary>
    /// InMemory repository configuration.
    /// </summary>
    public class InMemoryRepositoriesConfiguration : IRepositoriesConfiguration
    {
        /// <summary>
        /// Get the repository provider.
        /// </summary>
        /// <typeparam name="AggregateImplementationT">Aggregate implementation type.</typeparam>
        /// <typeparam name="AggregateT">Aggregate type.</typeparam>
        /// <typeparam name="IndexT">Index type</typeparam>
        /// <param name="aggregateInfo">Aggregate info.</param>
        /// <returns>Repository provider.</returns>
        public IRepositoryProvider<AggregateImplementationT, AggregateT, IndexT> GetRepositoryProvider<AggregateImplementationT, AggregateT, IndexT>(AggregateInfo aggregateInfo)
            where AggregateImplementationT : class, AggregateT
            where AggregateT : IAggregate<IndexT>
            where IndexT : IComparable, IComparable<IndexT>, IEquatable<IndexT>
        {
            return new InMemoryRepositoryProvider<AggregateImplementationT, AggregateT, IndexT>();
        }
    }
}