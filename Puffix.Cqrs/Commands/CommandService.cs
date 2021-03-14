using Puffix.Cqrs.Context;
using Puffix.Cqrs.Executors;
using System;
using System.Threading.Tasks;

namespace Puffix.Cqrs.Commands
{
    /// <summary>
    /// Service pour le traitement des commandes.
    /// </summary>
    public class CommandService : ICommandService
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
        public CommandService(IApplicationContext applicationContext, IExecutionContext executionContext)
        {
            this.applicationContext = applicationContext;
            this.executionContext = executionContext;
        }

        /// <summary>
        /// Traitement d'une commande.
        /// </summary>
        /// <param name="command">Commande.</param>
        /// <returns>Résultat de traitement de la commande.</returns>
        public async Task<IResult> ProcessAsync(ICommand command)
        {
            // Execution de la commande.
            return await ProcessInternalAsync(command, _ => string.Empty);
        }

        /// <summary>
        /// Traitement d'une commande.
        /// </summary>
        /// <typeparam name="ResultT">Type pour le résultat de la commande.</typeparam>
        /// <param name="command">Commande.</param>
        /// <returns>Résultat de traitement de la commande.</returns>
        public async Task<IResult<ResultT>> ProcessAsync<ResultT>(ICommand<ResultT> command)
        {
            // Execution de la commande.
            return await ProcessInternalAsync(command as ICommand, _ => (_ as ICommand<ResultT>).Result);
        }

        /// <summary>
        /// Traitement de la commande.
        /// </summary>
        /// <typeparam name="CommandT">Type de la commande.</typeparam>
        /// <typeparam name="ResultT">Type du résultat.</typeparam>
        /// <param name="command">Commande.</param>
        /// <param name="resultAccessor"></param>
        /// <returns>Résultat d'exécution de la commande.</returns>
        private async Task<IWrittableResult<ResultT>> ProcessInternalAsync<CommandT, ResultT>(CommandT command, Func<CommandT, ResultT> resultAccessor)
            where CommandT : ICommand
        {
            // Initialisation du résultat.
            IWrittableResult<ResultT> result = new ExecutionResult<ResultT>();

            // Contrôles des paramètres et du contexte.
            IChecker contextChecker = new ContextChecker(result);
            IChecker parametersChecker = new ParametersChecker(result);
            command.CheckContext(applicationContext, contextChecker);
            command.CheckParameters(parametersChecker);

            // Contrôle de la possiblité d'exécuter la commande. Si non, on indique que l'exécution a échoué.
            if (result.ValidContext && result.ValidParameters)
            {
                try
                {
                    // Exécution de la commande.
                    await command.ExecuteAsync(executionContext, applicationContext);
                    result.SetSucces(true);
                }
                catch (Exception error)
                {
                    result.AddError(error);
                    result.SetSucces(false);
                }

                // Si la commande a reussi, affectation du résultat.
                if (result.Success && command is ICommand<ResultT>)
                    result.SetResult(resultAccessor(command));
            }
            else
                result.SetSucces(false);

            return result;
        }
    }
}
