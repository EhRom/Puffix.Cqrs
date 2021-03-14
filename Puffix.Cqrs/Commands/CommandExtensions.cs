using Puffix.Cqrs.Executors;
using System.Threading.Tasks;

namespace Puffix.Cqrs.Commands
{
    /// <summary>
    /// Méthodes d'extension pour les commandes.
    /// </summary>
    public static class CommandExtensions
    {
        /// <summary>
        /// Exécution d'une commande.
        /// </summary>
        /// <param name="command">Commande.</param>
        /// <param name="commandService">Service de gestion des commandes.</param>
        /// <returns>Résultat d'exécution de la commande.</returns>
        public static async Task<IResult> ExecuteAsync(this ICommand command, CommandService commandService)
        {
            return await commandService.ProcessAsync(command);
        }

        /// <summary>
        /// Exécution d'une commande.
        /// </summary>
        /// <typeparam name="TResult">Type de résultat de la commande.</typeparam>
        /// <param name="command">Commande.</param>
        /// <param name="commandService">Service de gestion des commandes.</param>
        /// <returns>Résultat d'exécution de la commande et résultat de la commande.</returns>
        public static async Task<IResult<TResult>> ExecuteAsync<TResult>(this ICommand<TResult> command, CommandService commandService)
        {
            return await commandService.ProcessAsync(command);
        }
    }
}
