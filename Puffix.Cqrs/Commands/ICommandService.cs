using Puffix.Cqrs.Executors;
using System.Threading.Tasks;

namespace Puffix.Cqrs.Commands
{
    /// <summary>
    /// Contrat pour le service de traitement des commandes.
    /// </summary>
    public interface ICommandService
    {
        /// <summary>
        /// Traitement d'une commande.
        /// </summary>
        /// <param name="command">Commande.</param>
        /// <returns>Résultat de traitement de la commande.</returns>
        Task<IResult> ProcessAsync(ICommand command);

        /// <summary>
        /// Traitement d'une commande.
        /// </summary>
        /// <typeparam name="ResultT">Type pour le résultat de la commande.</typeparam>
        /// <param name="command">Commande.</param>
        /// <returns>Résultat de traitement de la commande.</returns>
        Task<IResult<ResultT>> ProcessAsync<ResultT>(ICommand<ResultT> command);
    }
}
