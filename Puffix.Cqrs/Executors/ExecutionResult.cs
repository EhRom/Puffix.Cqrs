using System;
using System.Collections.Generic;

namespace Puffix.Cqrs.Executors
{
    /// <summary>
    /// Resultat d'exécution.
    /// </summary>
    public class ExecutionResult : IWrittableResult
    {
        /// <summary>
        /// Indique si la commande à réussi ou non.
        /// </summary>
        public bool Success { get; private set; } = true;

        /// <summary>
        /// Indique si le contexte d'execution est valide.
        /// </summary>
        public bool ValidContext { get; private set; } = true;

        /// <summary>
        /// Indique si les paramètres sont valides.
        /// </summary>
        public bool ValidParameters { get; private set; } = true;

        /// <summary>
        /// Liste des erreurs de la commande.
        /// </summary>
        public IEnumerable<Exception> Errors { get; private set; }

        /// <summary>
        /// Constructeur.
        /// </summary>
        public ExecutionResult()
        {
            Errors = new List<Exception>();
        }

        /// <summary>
        /// Spécifie si la commande a réussi ou non.
        /// </summary>
        /// <param name="success">Indique si la commande a réussi ou non.</param>
        public void SetSucces(bool success)
        {
            Success = success;
        }

        /// <summary>
        /// Spécifie si le contexte d'execution est valide.
        /// </summary>
        /// <param name="vadidContext">Indique si le contexte d'execution est valide.</param>
        public void SetVadidContext(bool vadidContext)
        {
            if (!vadidContext)
                Success = false;

            ValidContext = vadidContext;
        }

        /// <summary>
        /// Spécifie si les paramètres sont valides.
        /// </summary>
        /// <param name="validParameters">Indique si les paramètres sont valides.</param>
        public void SetValidParameters(bool validParameters)
        {
            if (!validParameters)
                Success = false;

            ValidParameters = validParameters;
        }

        /// <summary>
        /// Ajoute une erreur dans le contexte d'exécution de la commande.
        /// </summary>
        /// <param name="error">Error.</param>
        public void AddError(Exception error)
        {
            ((List<Exception>)Errors).Add(error);
        }
    }

    /// <summary>
    /// Resultat d'exécution d'une commande.
    /// </summary>
    /// <typeparam name="ResultT">Type du résultat de la commande.</typeparam>
    public class ExecutionResult<ResultT> : ExecutionResult, IWrittableResult<ResultT>
    {
        /// <summary>
        /// Résultat de la commande.
        /// </summary>
        public ResultT Result { get; private set; }

        /// <summary>
        /// Constructeur.
        /// </summary>
        public ExecutionResult()
            : base()
        { }

        /// <summary>
        /// Spécifie le résultat de la commande.
        /// </summary>
        /// <param name="Result">Résultat de la commande.</param>
        public void SetResult(ResultT result)
        {
            Result = result;
        }
    }
}
