using Puffix.Cqrs.Context;
using Puffix.Cqrs.Executors;
using System;
using System.Threading.Tasks;

namespace Puffix.Cqrs.Queries;

/// <summary>
/// Query process service contract.
/// </summary>
public class QueryService : IQueryService
{
    /// <summary>
    /// Applcation context.
    /// </summary>
    private readonly IApplicationContext applicationContext;

    /// <summary>
    /// Execution context.
    /// </summary>
    private readonly IExecutionContext executionContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="applicationContext">Applcation context.</param>
    /// <param name="executionContext">Execution context.</param>
    public QueryService(IApplicationContext applicationContext, IExecutionContext executionContext)
    {
        this.applicationContext = applicationContext;
        this.executionContext = executionContext;
    }

    /// <summary>
    /// Process query.
    /// </summary>
    /// <param name="query">Query.</param>
    /// <returns>Query result.</returns>
    public async Task<IResult> ProcessAsync(IQuery query)
    {
        // Execution de la requête.
        return await ProcessInternal(query, _ => string.Empty);
    }

    /// <summary>
    /// Process query.
    /// </summary>
    /// <typeparam name="ResultT">Query result type.</typeparam>
    /// <param name="query">Query.</param>
    /// <returns>Query result.</returns>
    public async Task<IResult<ResultT>> ProcessAsync<ResultT>(IQuery<ResultT> query)
    {
        // Execution de la requête.
        return await ProcessInternal(query as IQuery, _ => ((IQuery<ResultT>)_).Result);
    }

    /// <summary>
    /// Process query.
    /// </summary>
    /// <typeparam name="QueryT">Query type.</typeparam>
    /// <typeparam name="ResultT">Query result type.</typeparam>
    /// <param name="query">Query.</param>
    /// <param name="resultAccessor">Result accessor. Allow to extract the query result./param>
    /// <returns>Query result.</returns>
    private async Task<IWrittableResult<ResultT>> ProcessInternal<QueryT, ResultT>(QueryT query, Func<QueryT, ResultT> resultAccessor)
        where QueryT : IQuery
    {
        // Initialize the result.
        IWrittableResult<ResultT> result = new ExecutionResult<ResultT>();

        // Control parameters and context.
        IChecker contextChecker = new ContextChecker(result);
        IChecker parametersChecker = new ParametersChecker(result);
        query.CheckContext(applicationContext, contextChecker);
        query.CheckParameters(parametersChecker);

        // Check if the query can be executed.
        if (result.ValidContext && result.ValidParameters)
        {
            try
            {
                // Execute the query.
                await query.ExecuteAsync(executionContext, applicationContext);
                result.SetSucces(true);
            }
            catch (Exception error)
            {
                result.AddError(error);
                result.SetSucces(false);
            }

            // Set result on query success.
            if (result.Success && query is IQuery<ResultT>)
                result.SetResult(resultAccessor(query));
        }
        else
            result.SetSucces(false);

        return result;
    }
}
