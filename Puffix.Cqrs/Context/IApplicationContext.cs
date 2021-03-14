namespace Puffix.Cqrs.Context
{
    /// <summary>
    /// Contrat de définition d'un contexte d'application.
    /// </summary>
    public interface IApplicationContext
    {
        /// <summary>
        /// Utilisateur courant.
        /// </summary>
        IApplicationUser CurrentUser { get; }
    }
}
