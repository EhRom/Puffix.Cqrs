using System.Collections.Generic;
using System.Globalization;

namespace Puffix.Cqrs.Context
{

    /// <summary>
    /// Application user contract.
    /// </summary>
    public interface IApplicationUser
    {
        /// <summary>
        /// User id.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// User id.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// User culture.
        /// </summary>
        CultureInfo Culture { get; }

        /// <summary>
        /// Claims collection.
        /// </summary>
        IEnumerable<IClaim> Claims { get; }

        /// <summary>
        /// Set user identity.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <param name="name">User name.</param>
        /// <param name="culture">User culture.</param>
        void SetIdentity(string id, string name, CultureInfo culture);

        /// <summary>
        /// Add claim.
        /// </summary>
        /// <param name="claim">Claim.</param>
        void AddClaim(IClaim claim);

        /// <summary>
        /// Add claim collection.
        /// </summary>
        /// <param name="claims">Claims</param>
        void AddClaims(IEnumerable<IClaim> claims);

        /// <summary>
        /// Remove claim.
        /// </summary>
        /// <param name="claim">Claim.</param>

        void RemoveClaim(IClaim claim);

        /// <summary>
        /// Remove several claims.
        /// </summary>
        /// <param name="claims">Claims</param>
        void RemoveClaims(IEnumerable<IClaim> claims);

        /// <summary>
        /// Check if user has a claim.
        /// </summary>
        /// <param name="claim">Claim.</param>
        /// <returns>Indicates whether the user has the specified claim.</returns>
        bool Claim(IClaim claim);
    }
}
