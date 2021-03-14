using System.Collections.Generic;
using System.Globalization;

namespace Puffix.Cqrs.Context
{
    /// <summary>
    /// Utilisateur d'application.
    /// </summary>
    /// <remarks>Implémentation par défaut.</remarks>
    public class ApplicationUser : IApplicationUser
    {
        /// <summary>
        /// Identifiant de l'utilisateur.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Nom de l'utilisateur.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Culture de l'utilisateur.
        /// </summary>
        public CultureInfo Culture { get; private set; }

        /// <summary>
        /// Liste des droits (indexés).
        /// </summary>
        private readonly IDictionary<string, IClaim> claims;

        /// <summary>
        /// Liste des droits.
        /// </summary>
        public IEnumerable<IClaim> Claims => claims.Values;

        /// <summary>
        /// Constructeur.
        /// </summary>
        public ApplicationUser()
        {
            claims = new Dictionary<string, IClaim>();
        }

        /// <summary>
        /// Spécification de l'identité de l'utilisateur.
        /// </summary>
        /// <param name="id">Identifiant de l'utilisateur.</param>
        /// <param name="name">Nom de l'utilisateur.</param>
        /// <param name="culture">Culture de l'utilisateur.</param>
        public void SetIdentity(string id, string name, CultureInfo culture)
        {
            Id = id;
            Name = name;
            Culture = culture;
        }

        /// <summary>
        /// Ajout d'un droit.
        /// </summary>
        /// <param name="claim">Droit.</param>
        public void AddClaim(IClaim claim)
        {
            if (!claims.ContainsKey(claim.Id))
                claims[claim.Id] = claim;
        }

        /// <summary>
        /// Ajout d'une liste de droits.
        /// </summary>
        /// <param name="claims">Droits</param>
        public void AddClaims(IEnumerable<IClaim> claims)
        {
            foreach (IClaim claim in claims)
            {
                AddClaim(claim);
            }
        }

        /// <summary>
        /// Suppression d'un droit à l'utilisateur.
        /// </summary>
        /// <param name="claim">Droit.</param>
        public void RemoveClaim(IClaim claim)
        {
            if (claims.ContainsKey(claim.Id))
                claims.Remove(claim.Id);
        }

        /// <summary>
        /// Suppression d'un droit à l'utilisateur.
        /// </summary>
        /// <param name="claims">Liste de droits.</param>
        public void RemoveClaims(IEnumerable<IClaim> claims)
        {
            foreach (IClaim claim in claims)
            {
                RemoveClaim(claim);
            }
        }

        /// <summary>
        /// Indique si l'utilisateur a un droit particulier.
        /// </summary>
        /// <param name="claim">Droit.</param>
        /// <returns>Indique si l'utilisateur a le droit particulier ou non.</returns>
        public bool Claim(IClaim claim)
        {
            return claims.ContainsKey(claim.Id);
        }
    }
}
