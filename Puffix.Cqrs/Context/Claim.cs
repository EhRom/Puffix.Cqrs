namespace Puffix.Cqrs.Context;

/// <summary>
/// Claim.
/// </summary>
public class Claim : IClaim
{
    /// <summary>
    /// Claim id.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Claim name.
    /// </summary>
    public string Name { get; set; }
}
