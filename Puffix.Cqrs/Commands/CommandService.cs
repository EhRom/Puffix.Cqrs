using Puffix.Cqrs.Context;
using Puffix.Cqrs.Executors;
using System;
using System.Threading.Tasks;

namespace Puffix.Cqrs.Commands;

/// <summary>
/// Service to process commands.
/// </summary>
public class CommandService : ICommandService
{
    /// <summary>
    /// Application context.
    /// </summary>
    private readonly IApplicationContext applicationContext;

    /// <summary>
    /// Execution context.
    /// </summary>
    private readonly IExecutionContext executionContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="applicationContext">Application context.</param>
    /// <param name="executionContext">Execution context.</param>
    public CommandService(IApplicationContext applicationContext, IExecutionContext executionContext)
    {
        this.applicationContext = applicationContext;
        this.executionContext = executionContext;
    }

    /// <summary>
    /// Process a command.
    /// </summary>
    /// <param name="command">Command.</param>
    /// <returns>Command process result.</returns>
    public async Task<IResult> ProcessAsync(ICommand command)
    {
        // Execution de la commande.
        return await ProcessInternalAsync(command, _ => string.Empty);
    }

    /// <summary>
    /// Process a command.
    /// </summary>
    /// <typeparam name="TResult">Command result type.</typeparam>
    /// <param name="command">Command.</param>
    /// <returns>Command process result with typed result.</returns>
    public async Task<IResult<ResultT>> ProcessAsync<ResultT>(ICommand<ResultT> command)
    {
        // Execution de la commande.
        return await ProcessInternalAsync(command as ICommand, _ =>
        {
            if (_ == null)
                throw new ArgumentNullException($"The {nameof(command)} is not set or does not implement ICommand.");

            return ((ICommand<ResultT>)_).Result;
        });
    }

    /// <summary>
    /// Process a command.
    /// </summary>
    /// <typeparam name="CommandT">Command type.</typeparam>
    /// <typeparam name="TResult">Command result type.</typeparam>
    /// <param name="command">Command.</param>
    /// <param name="resultAccessor">Function to access the result.</param>
    /// <returns>Command process result.</returns>
    private async Task<IWrittableResult<ResultT>> ProcessInternalAsync<CommandT, ResultT>(CommandT command, Func<CommandT, ResultT> resultAccessor)
        where CommandT : ICommand
    {
        // Initialize result.
        IWrittableResult<ResultT> result = new ExecutionResult<ResultT>();

        IChecker contextChecker = new ContextChecker(result);
        IChecker parametersChecker = new ParametersChecker(result);
        command.CheckContext(applicationContext, contextChecker);
        command.CheckParameters(parametersChecker);

        if (result.ValidContext && result.ValidParameters)
        {
            try
            {
                await command.ExecuteAsync(executionContext, applicationContext);
                result.SetSucces(true);
            }
            catch (Exception error)
            {
                result.AddError(error);
                result.SetSucces(false);
            }

            if (result.Success && command is ICommand<ResultT>)
                result.SetResult(resultAccessor(command));
        }
        else
            result.SetSucces(false);

        return result;
    }
}
