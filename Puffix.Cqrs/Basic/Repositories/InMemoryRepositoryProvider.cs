using Puffix.Cqrs.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Puffix.Cqrs.Repositories;
using System.Threading.Tasks;

namespace Puffix.Cqrs.Basic.Repositories;

/// <summary>
/// InMemory repository provider.
/// </summary>
/// <typeparam name="AggregateImplementationT">Aggregate implementation type.</typeparam>
/// <typeparam name="AggregateT">Aggregate type.</typeparam>
/// <typeparam name="IndexT">Index type</typeparam>
public class InMemoryRepositoryProvider<AggregateImplementationT, AggregateT, IndexT> : IRepositoryProvider<AggregateImplementationT, AggregateT, IndexT>
    where AggregateImplementationT : class, AggregateT
    where AggregateT : IAggregate<IndexT>
    where IndexT : IComparable, IComparable<IndexT>, IEquatable<IndexT>
{
    /// <summary>
    /// Data dictionary.
    /// </summary>
    private readonly IDictionary<IndexT, AggregateImplementationT> inMemoryData = new Dictionary<IndexT, AggregateImplementationT>();

    /// <summary>
    /// Type of the elements.
    /// </summary>
    public Type ElementType => typeof(AggregateT);

    /// <summary>
    /// Expression.
    /// </summary>
    public Expression Expression => inMemoryData.Values.AsQueryable().Expression;

    /// <summary>
    /// Query provider.
    /// </summary>
    public IQueryProvider Provider => inMemoryData.Values.AsQueryable().Provider;

    /// <summary>
    /// Test if aggregate exists.
    /// </summary>
    /// <param name="aggregate">Aggregate.</param>
    /// <returns>Indicates whether the aggregate exists or not.</returns>
    public Task<bool> ExistsAsync(AggregateT aggregate)
    {
        return Task.FromResult(inMemoryData.ContainsKey(aggregate.Id));
    }

    /// <summary>
    /// Test if aggregate exists.
    /// </summary>
    /// <param name="id">Aggregate id.</param>
    /// <returns>Indicates whether the aggregate exists or not.</returns>
    public Task<bool> ExistsAsync(IndexT id)
    {
        return Task.FromResult(inMemoryData.ContainsKey(id));
    }

    /// <summary>
    /// Get aggregate by id.
    /// </summary>
    /// <param name="id">Aggregate id.</param>
    /// <returns>Aggregate.</returns>
    public Task<AggregateT> GetByIdAsync(IndexT id)
    {
        if (inMemoryData.ContainsKey(id))
            return Task.FromResult((AggregateT)inMemoryData[id]);
        else
            throw new Exception($"Element with id {id} of type {typeof(AggregateT).FullName} is not found.");
    }

    /// <summary>
    /// Get aggregate by id.
    /// </summary>
    /// <param name="id">Aggregate id.</param>
    /// <returns>Aggregate or null value.</returns>
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
    /// Generate the next id.
    /// </summary>
    /// <param name="generateNextId">Function to generate the next id.</param>
    /// <returns>Aggregate id.</returns>
    public  async Task<IndexT> GetNextAggregatetIdAsync(Func<IndexT, IndexT> generateNextId)
    {
        await Task.CompletedTask;

        IndexT lastId;
        if (inMemoryData.Count == 0)
            lastId = default;
        else
            lastId = inMemoryData.Keys.Max();

        IndexT nextId = generateNextId(lastId);
        return nextId;
    }

    /// <summary>
    /// Get enumerator.
    /// </summary>
    /// <returns>Enumerator.</returns>
    public IEnumerator<AggregateImplementationT> GetEnumerator()
    {
        return inMemoryData.Values.GetEnumerator();
    }

    /// <summary>
    /// Get enumerator.
    /// </summary>
    /// <returns>Enumerator.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return inMemoryData.Values.GetEnumerator();
    }

    /// <summary>
    /// Create aggregate.
    /// </summary>
    /// <param name="aggregate">Aggregate.</param>
    public Task CreateAsync(AggregateT aggregate)
    {
        if (inMemoryData.ContainsKey(aggregate.Id))
            throw new Exception($"Element with id {aggregate.Id} of type {typeof(AggregateT).FullName} already exists.");

        inMemoryData[aggregate.Id] = (AggregateImplementationT)aggregate;

        return Task.FromResult(Type.Missing);
    }

    /// <summary>
    /// Update aggregate.
    /// </summary>
    /// <param name="aggregate">Aggregate.</param>
    public Task UpdateAsync(AggregateT aggregate)
    {
        if (!inMemoryData.ContainsKey(aggregate.Id))
            throw new Exception($"Element with id {aggregate.Id} of type {typeof(AggregateT).FullName} does not exist.");

        inMemoryData[aggregate.Id] = (AggregateImplementationT)aggregate;

        return Task.FromResult(Type.Missing);
    }

    /// <summary>
    /// Delete aggregate.
    /// </summary>
    /// <param name="aggregate">Aggregate.</param>
    public Task DeleteAsync(AggregateT aggregate)
    {
        if (!inMemoryData.ContainsKey(aggregate.Id))
            throw new Exception($"Element with id {aggregate.Id} of type {typeof(AggregateT).FullName} does not exist.");

        inMemoryData.Remove(aggregate.Id);

        return Task.FromResult(Type.Missing);
    }

    /// <summary>
    /// Save changes.
    /// </summary>
    public Task SaveAsync()
    {
        return Task.FromResult(Type.Missing);
    }
}
