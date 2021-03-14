using System;

namespace Puffix.Cqrs.Executors
{
    /// <summary>
    /// Contrat pour les composants de contrôles.
    /// </summary>
    public interface IChecker
    {
        /// <summary>
        /// Contrôle d'un élément.
        /// </summary>
        /// <param name="expression">Expression de contrôle (résultat).</param>
        /// <param name="failCheckMessage">Message d'erreur en cas d'échec.</param>
        /// <returns>Résultat du contrôle.</returns>
        void Check(bool expression, string failCheckMessage);

        /// <summary>
        /// Contrôle d'un élément.
        /// </summary>
        /// <param name="expression">Expression de contrôle (résultat).</param>
        /// <param name="failCheckErrorFunction">Fonction pour la construction de l'erreur en cas d'échec.</param>
        /// <returns>Résultat du contrôle.</returns>
        void Check(bool expression, Func<Exception> failCheckErrorFunction);
    }
}
