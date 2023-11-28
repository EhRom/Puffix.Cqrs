using Puffix.Cqrs.Context;
using Puffix.Cqrs.Executors;
using System.Threading.Tasks;

namespace Puffix.Cqrs.Queries;

/// <summary>
/// Query contract.
/// </summary>
public interface IQuery
{
    /// <summary>
    /// Check the execution context of the query.
    /// </summary>
    /// <param name="applicationContext">Application context.</param>
    /// <param name="contextChecker">Context checker.</param>
    /// <returns>Check result.</returns>
    void CheckContext(IApplicationContext applicationContext, IChecker contextChecker);

    /// <summary>
    /// Check the query paramters.
    /// </summary>
    /// <param name="parametersChecker">Parameters checker.</param>
    /// <returns>Check result.</returns>
    void CheckParameters(IChecker parametersChecker);

    /// <summary>
    /// Query execution.
    /// </summary>
    /// <param name="executionContext">Execution context.</param>
    /// <param name="applicationContext">Application context.</param>
    Task ExecuteAsync(IExecutionContext executionContext, IApplicationContext applicationContext);
}

/// <summary>
/// Query with typed result contract.
/// </summary>
/// <typeparam name="ResultT">Type of the result.</typeparam>
public interface IQuery<out ResultT> : IQuery
{
    /// <summary>
    /// Query result.
    /// </summary>
    ResultT Result { get; }
}
