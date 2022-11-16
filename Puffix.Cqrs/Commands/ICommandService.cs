using Puffix.Cqrs.Executors;
using System.Threading.Tasks;

namespace Puffix.Cqrs.Commands
{
    /// <summary>
    /// Service to process commands contract.
    /// </summary>
    public interface ICommandService
    {
        /// <summary>
        /// Process a command.
        /// </summary>
        /// <param name="command">Command.</param>
        /// <returns>Command process result.</returns>
        Task<IResult> ProcessAsync(ICommand command);

        /// <summary>
        /// Process a command.
        /// </summary>
        /// <typeparam name="TResult">Command result type.</typeparam>
        /// <param name="command">Command.</param>
        /// <returns>Command process result with typed result.</returns>
        Task<IResult<ResultT>> ProcessAsync<ResultT>(ICommand<ResultT> command);
    }
}
