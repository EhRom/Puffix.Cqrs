namespace Puffix.Cqrs.Context
{
    /// <summary>
    /// Droit dans l'application
    /// </summary>
    public class Claim : IClaim
    {
        /// <summary>
        /// Identifiant.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Nom.
        /// </summary>
        public string Name { get; set; }
    }
}
