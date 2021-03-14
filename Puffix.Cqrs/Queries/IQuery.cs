using Puffix.Cqrs.Context;
using Puffix.Cqrs.Executors;
using System.Threading.Tasks;

namespace Puffix.Cqrs.Queries
{
    /// <summary>
    /// Contrat pour les requêtes.
    /// </summary>
    public interface IQuery
    {
        /// <summary>
        /// Contrôle du contexte d'exécution de la requête.
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
        /// Execution de la requête (exécution interne).
        /// </summary>
        /// <param name="executionContext">Contexte d'exécution.</param>
        /// <param name="applicationContext">Context de l'application.</param>
        Task ExecuteAsync(IExecutionContext executionContext, IApplicationContext applicationContext);
    }

    /// <summary>
    /// Contrat pour les requêtes renvoyant un résultat.
    /// </summary>
    /// <typeparam name="ResultT">Type du résultat.</typeparam>
    public interface IQuery<out ResultT> : IQuery
    {
        /// <summary>
        /// Résultat de la requête.
        /// </summary>
        ResultT Result { get; }
    }
}
