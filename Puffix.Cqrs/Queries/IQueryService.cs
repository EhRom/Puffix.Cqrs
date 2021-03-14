using Puffix.Cqrs.Executors;
using System.Threading.Tasks;

namespace Puffix.Cqrs.Queries
{
    /// <summary>
    /// Contrat pour le service de traitement des requêtes.
    /// </summary>
    public interface IQueryService
    {
        /// <summary>
        /// Traitement d'une requête.
        /// </summary>
        /// <param name="query">Requête.</param>
        /// <returns>Résultat de traitement de la requête.</returns>
        Task<IResult> ProcessAsync(IQuery query);

        /// <summary>
        /// Traitement d'une requête.
        /// </summary>
        /// <typeparam name="ResultT">Type pour le résultat de la requête.</typeparam>
        /// <param name="query">Requête.</param>
        /// <returns>Résultat de traitement de la requête.</returns>
        Task<IResult<ResultT>> ProcessAsync<ResultT>(IQuery<ResultT> query);
    }
}
