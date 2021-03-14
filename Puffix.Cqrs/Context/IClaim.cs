namespace Puffix.Cqrs.Context
{
    /// <summary>
    /// Contrat pour définir un droit dans l'application.
    /// </summary>
    public interface IClaim
    {
        /// <summary>
        /// Identifiant.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Nom.
        /// </summary>
        string Name { get; }
    }
}
