using Puffix.Cqrs.Context;
using Puffix.Cqrs.Executors;
using System.Threading.Tasks;

namespace Puffix.Cqrs.Commands;

/// <summary>
/// Command contract.
/// </summary>
public interface ICommand
{
    /// <summary>
    /// Check context for command execution.
    /// </summary>
    /// <param name="applicationContext">Application context.</param>
    /// <param name="contextChecker">Context checker.</param>
    /// <returns>Check result.</returns>
    void CheckContext(IApplicationContext applicationContext, IChecker contextChecker);

    /// <summary>
    /// Check parameters for command execution.
    /// </summary>
    /// <param name="parametersChecker">Parameters checker.</param>
    /// <returns>Check result.</returns>
    void CheckParameters(IChecker parametersChecker);

    /// <summary>
    /// Execution command (internal execution).
    /// </summary>
    /// <param name="executionContext">Execution context.</param>
    /// <param name="applicationContext">Application context.</param>
    Task ExecuteAsync(IExecutionContext executionContext, IApplicationContext applicationContext);
}

/// <summary>
/// Command contract for command that returns typed result.
/// </summary>
/// <typeparam name="ResultT">Result type.</typeparam>
public interface ICommand<out ResultT>
{
    /// <summary>
    /// Command result.
    /// </summary>
    ResultT Result { get; }
}
