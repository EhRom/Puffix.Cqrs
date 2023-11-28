using System.Collections.Generic;
using System.Globalization;

namespace Puffix.Cqrs.Context;

/// <summary>
/// Application user.
/// </summary>
/// <remarks>Default implementation.</remarks>
public class ApplicationUser : IApplicationUser
{
    private readonly IDictionary<string, IClaim> claims;

    /// <summary>
    /// User id.
    /// </summary>
    public string Id { get; private set; }

    /// <summary>
    /// User id.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// User culture.
    /// </summary>
    public CultureInfo Culture { get; private set; }

    /// <summary>
    /// Claims collection.
    /// </summary>
    public IEnumerable<IClaim> Claims => claims.Values;

    /// <summary>
    /// Constructor.
    /// </summary>
    public ApplicationUser()
    {
        claims = new Dictionary<string, IClaim>();
    }

    /// <summary>
    /// Set user identity.
    /// </summary>
    /// <param name="id">User id.</param>
    /// <param name="name">User name.</param>
    /// <param name="culture">User culture.</param>
    public void SetIdentity(string id, string name, CultureInfo culture)
    {
        Id = id;
        Name = name;
        Culture = culture;
    }

    /// <summary>
    /// Add claim.
    /// </summary>
    /// <param name="claim">Claim.</param>
    public void AddClaim(IClaim claim)
    {
        if (!claims.ContainsKey(claim.Id))
            claims[claim.Id] = claim;
    }

    /// <summary>
    /// Add claim collection.
    /// </summary>
    /// <param name="claims">Claims</param>
    public void AddClaims(IEnumerable<IClaim> claims)
    {
        foreach (IClaim claim in claims)
        {
            AddClaim(claim);
        }
    }

    /// <summary>
    /// Remove claim.
    /// </summary>
    /// <param name="claim">Claim.</param>
    public void RemoveClaim(IClaim claim)
    {
        if (claims.ContainsKey(claim.Id))
            claims.Remove(claim.Id);
    }

    /// <summary>
    /// Remove several claims.
    /// </summary>
    /// <param name="claims">Claims</param>
    public void RemoveClaims(IEnumerable<IClaim> claims)
    {
        foreach (IClaim claim in claims)
        {
            RemoveClaim(claim);
        }
    }

    /// <summary>
    /// Check if user has a claim.
    /// </summary>
    /// <param name="claim">Claim.</param>
    /// <returns>Indicates whether the user has the specified claim.</returns>
    public bool Claim(IClaim claim)
    {
        return claims.ContainsKey(claim.Id);
    }
}
