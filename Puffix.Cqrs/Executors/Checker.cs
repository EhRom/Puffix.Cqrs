using System;

namespace Puffix.Cqrs.Executors
{
    /// <summary>
    /// Contrôleur.
    /// </summary>
    public abstract class Checker : IChecker
    {
        /// <summary>
        /// Résultat de l'exécution.
        /// </summary>
        private readonly IWrittableResult result;

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="result">Résultat de l'exécution</param>
        public Checker(IWrittableResult result)
        {
            this.result = result;
        }

        /// <summary>
        /// Contrôle d'un élément.
        /// </summary>
        /// <param name="expression">Expression de contrôle (résultat).</param>
        /// <param name="failCheckMessage">Message d'erreur en cas d'échec.</param>
        /// <returns>Résultat du contrôle.</returns>
        public void Check(bool expression, string failCheckMessage)
        {
            Check(expression, () => new ArgumentException(failCheckMessage));
        }

        /// <summary>
        /// Contrôle d'un élément.
        /// </summary>
        /// <param name="expression">Expression de contrôle (résultat).</param>
        /// <param name="failCheckErrorFunction">Fonction pour la construction de l'erreur en cas d'échec.</param>
        /// <returns>Résultat du contrôle.</returns>
        public void Check(bool expression, Func<Exception> failCheckErrorFunction)
        {
            if (!expression)
            {
                SetValid(result, false);
                result.AddError(failCheckErrorFunction());
            }
        }

        /// <summary>
        /// Spécifie si l'élément est valide ou non.
        /// </summary>
        /// <param name="result">Résultat de l'exécution</param>
        /// <param name="isValid">Indique si l'élément est valide ou non.</param>
        protected abstract void SetValid(IWrittableResult result, bool isValid);
    }
}
