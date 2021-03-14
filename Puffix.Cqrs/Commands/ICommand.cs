using Puffix.Cqrs.Context;
using Puffix.Cqrs.Executors;
using System.Threading.Tasks;

namespace Puffix.Cqrs.Commands
{
    /// <summary>
    /// Contrat pour les commandes.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Contrôle du contexte d'exécution de la commande.
        /// </summary>
        /// <param name="applicationContext">Context de l'application.</param>
        /// <param name="contextChecker">Contrôleur de contexte.</param>
        /// <returns>Résultat du contrôle.</returns>
        void CheckContext(IApplicationContext applicationContext, IChecker contextChecker);

        /// <summary>
        /// Contrôle des paramètres.
        /// </summary>
        /// <param name="parametersChecker">Contrôleur de paramètres.</param>
        /// <returns>Résultat du contrôle.</returns>
        void CheckParameters(IChecker parametersChecker);

        /// <summary>
        /// Execution de la commande (exécution interne).
        /// </summary>
        /// <param name="executionContext">Contexte d'exécution.</param>
        /// <param name="applicationContext">Context de l'application.</param>
        Task ExecuteAsync(IExecutionContext executionContext, IApplicationContext applicationContext);
    }

    /// <summary>
    /// Contrat pour les commandes renvoyant un résultat.
    /// </summary>
    /// <typeparam name="ResultT">Type du résultat.</typeparam>
    public interface ICommand<out ResultT>
    {
        /// <summary>
        /// Résultat de la commande.
        /// </summary>
        ResultT Result { get; }
    }
}
