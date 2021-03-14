using System;

namespace Puffix.Cqrs.Executors
{
    /// <summary>
    /// Contrat pour la definition des résultats d'exécution (en mode écriture).
    /// </summary>
    public interface IWrittableResult : IResult
    {
        /// <summary>
        /// Spécifie si l'exécution a réussi ou non.
        /// </summary>
        /// <param name="success">Indique si l'exécution a réussi ou non.</param>
        void SetSucces(bool success);

        /// <summary>
        /// Spécifie si le contexte d'execution est valide.
        /// </summary>
        /// <param name="vadidContext">Indique si le contexte d'execution est valide.</param>
        void SetVadidContext(bool vadidContext);

        /// <summary>
        /// Spécifie si les paramètres sont valides.
        /// </summary>
        /// <param name="validParameters">Indique si les paramètres sont valides.</param>
        void SetValidParameters(bool validParameters);

        /// <summary>
        /// Ajoute une erreur dans le contexte d'exécution de l'exécution.
        /// </summary>
        /// <param name="error">Error.</param>
        void AddError(Exception error);
    }

    /// <summary>
    /// Contrat pour la definition des résultats de l'exécution (en mode écriture).
    /// </summary>
    public interface IWrittableResult<ResultT> : IWrittableResult, IResult<ResultT>
    {
        /// <summary>
        /// Spécifie le résultat de l'exécution.
        /// </summary>
        /// <param name="Result">Résultat de l'exécution.</param>
        void SetResult(ResultT result);
    }
}
