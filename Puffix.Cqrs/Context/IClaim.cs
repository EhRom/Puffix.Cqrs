namespace Puffix.Cqrs.Context
{
    /// <summary>
    /// Claim contract.
    /// </summary>
    public interface IClaim
    {
        /// <summary>
        /// Claim id.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Claim name.
        /// </summary>
        string Name { get; }
    }
}
