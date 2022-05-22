using Puffix.Cqrs.Context;
using Puffix.Cqrs.Executors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Puffix.Cqrs.Queries
{
    /// <summary>
    /// Service pour le traitement des requêtes.
    /// </summary>
    public class QueryService : IQueryService
    {
        /// <summary>
        /// Contexte de l'applcation.
        /// </summary>
        private readonly IApplicationContext applicationContext;

        /// <summary>
        /// Contexte d'exécution.
        /// </summary>
        private readonly IExecutionContext executionContext;

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="context">Contexte de l'application.</param>
        public QueryService(IApplicationContext applicationContext, IExecutionContext executionContext)
        {
            this.applicationContext = applicationContext;
            this.executionContext = executionContext;
        }

        /// <summary>
        /// Traitement d'une requête.
        /// </summary>
        /// <param name="query">Requête.</param>
        /// <returns>Résultat de traitement de la requête.</returns>
        public async Task<IResult> ProcessAsync(IQuery query)
        {
            // Execution de la requête.
            return await ProcessInternal(query, _ => string.Empty);
        }

        /// <summary>
        /// Traitement d'une requête.
        /// </summary>
        /// <typeparam name="ResultT">Type pour le résultat de la requête.</typeparam>
        /// <param name="query">Requête.</param>
        /// <returns>Résultat de traitement de la requête.</returns>
        public async Task<IResult<ResultT>> ProcessAsync<ResultT>(IQuery<ResultT> query)
        {
            // Execution de la requête.
            return await ProcessInternal(query as IQuery, _ => ((IQuery<ResultT>)_).Result);
        }

        /// <summary>
        /// Traitement de la commande.
        /// </summary>
        /// <typeparam name="QueryT">Type de la commande.</typeparam>
        /// <typeparam name="ResultT">Type du résultat.</typeparam>
        /// <param name="query">Commande.</param>
        /// <param name="resultAccessor"></param>
        /// <returns>Résultat d'exécution de la commande.</returns>
        private async Task<IWrittableResult<ResultT>> ProcessInternal<QueryT, ResultT>(QueryT query, Func<QueryT, ResultT> resultAccessor)
            where QueryT : IQuery
        {
            // Initialisation du résultat.
            IWrittableResult<ResultT> result = new ExecutionResult<ResultT>();

            // Contrôles des paramètres et du contexte.
            IChecker contextChecker = new ContextChecker(result);
            IChecker parametersChecker = new ParametersChecker(result);
            query.CheckContext(applicationContext, contextChecker);
            query.CheckParameters(parametersChecker);

            // Contrôle de la possiblité d'exécuter la commande. Si non, on indique que l'exécution a échoué.
            if (result.ValidContext && result.ValidParameters)
            {
                try
                {
                    // Exécution de la commande.
                    await query.ExecuteAsync(executionContext, applicationContext);
                    result.SetSucces(true);
                }
                catch (Exception error)
                {
                    result.AddError(error);
                    result.SetSucces(false);
                }

                // Si la commande a reussi, affectation du résultat.
                if (result.Success && query is IQuery<ResultT>)
                    result.SetResult(resultAccessor(query));
            }
            else
                result.SetSucces(false);

            return result;
        }
    }
}
