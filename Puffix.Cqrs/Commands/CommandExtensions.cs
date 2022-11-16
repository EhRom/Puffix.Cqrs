using Puffix.Cqrs.Executors;
using System.Threading.Tasks;

namespace Puffix.Cqrs.Commands
{
    /// <summary>
    /// Extension methods for commands.
    /// </summary>
    public static class CommandExtensions
    {
        /// <summary>
        /// Execute command.
        /// </summary>
        /// <param name="command">Command.</param>
        /// <param name="commandService">Command service.</param>
        /// <returns>Command execution result.</returns>
        public static async Task<IResult> ExecuteAsync(this ICommand command, CommandService commandService)
        {
            return await commandService.ProcessAsync(command);
        }

        /// <summary>
        /// Execute command (with typed result).
        /// </summary>
        /// <typeparam name="TResult">Command result type.</typeparam>
        /// <param name="command">Command.</param>
        /// <param name="commandService">Command service.</param>
        /// <returns>Command execution result and command result.</returns>
        public static async Task<IResult<TResult>> ExecuteAsync<TResult>(this ICommand<TResult> command, CommandService commandService)
        {
            return await commandService.ProcessAsync(command);
        }
    }
}
