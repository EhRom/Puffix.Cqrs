namespace Puffix.Cqrs.Executors
{
    /// <summary>
    /// Contrôleur des paramètres pour l'exécution.
    /// </summary>
    public class ParametersChecker : Checker
    {
        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="result">Résultat de l'exécution.</param>
        public ParametersChecker(IWrittableResult result)
            : base(result)
        { }

        /// <summary>
        /// Spécifie si l'élément est valide ou non.
        /// </summary>
        /// <param name="result">Résultat de l'exécution.</param>
        /// <param name="isValid">Indique si l'élément est valide ou non.</param>
        protected override void SetValid(IWrittableResult result, bool isValid)
        {
            result.SetValidParameters(isValid);
        }
    }
}
