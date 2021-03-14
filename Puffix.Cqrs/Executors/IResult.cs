using System;
using System.Collections.Generic;

namespace Puffix.Cqrs.Executors
{
    /// <summary>
    /// Contrat pour la definition des résultats.
    /// </summary>
    public interface IResult
    {
        /// <summary>
        /// Indique si l'exécution a réussi ou non.
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// Indique si le contexte d'execution est valide.
        /// </summary>
        bool ValidContext { get; }

        /// <summary>
        /// Indique si les paramètres sont valides.
        /// </summary>
        bool ValidParameters { get; }

        /// <summary>
        /// Liste des erreurs.
        /// </summary>
        IEnumerable<Exception> Errors { get; }
    }

    /// <summary>
    /// Contrat pour la definition des résultats.
    /// </summary>
    public interface IResult<ResultT> : IResult
    {
        /// <summary>
        /// Résultat.
        /// </summary>
        ResultT Result { get; }
    }
}
