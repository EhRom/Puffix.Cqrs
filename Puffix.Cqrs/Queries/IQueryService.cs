using Puffix.Cqrs.Executors;
using System.Threading.Tasks;

namespace Puffix.Cqrs.Queries;

/// <summary>
/// Query process service contract.
/// </summary>
public interface IQueryService
{
    /// <summary>
    /// Process query.
    /// </summary>
    /// <param name="query">Query.</param>
    /// <returns>Query result.</returns>
    Task<IResult> ProcessAsync(IQuery query);

    /// <summary>
    /// Process query.
    /// </summary>
    /// <typeparam name="ResultT">Query result type.</typeparam>
    /// <param name="query">Query.</param>
    /// <returns>Query result.</returns>
    Task<IResult<ResultT>> ProcessAsync<ResultT>(IQuery<ResultT> query);
}
