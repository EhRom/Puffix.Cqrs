using System.Collections.Generic;
using System.Globalization;

namespace Puffix.Cqrs.Context
{

    /// <summary>
    /// Contrat pour la définition d'un utilisateur.
    /// </summary>
    public interface IApplicationUser
    {
        /// <summary>
        /// Identifiant de l'utilisateur.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Nom de l'utilisateur.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Culture de l'utilisateur.
        /// </summary>
        CultureInfo Culture { get; }

        /// <summary>
        /// Liste des droits.
        /// </summary>
        IEnumerable<IClaim> Claims { get; }

        /// <summary>
        /// Spécification de l'identité de l'utilisateur.
        /// </summary>
        /// <param name="id">Identifiant de l'utilisateur.</param>
        /// <param name="name">Nom de l'utilisateur.</param>
        /// <param name="culture">Culture de l'utilisateur.</param>
        void SetIdentity(string id, string name, CultureInfo culture);

        /// <summary>
        /// Ajout d'un droit à l'utilisateur.
        /// </summary>
        /// <param name="claim">Droit.</param>
        void AddClaim(IClaim claim);

        /// <summary>
        /// Ajout de droits à l'utilisateur.
        /// </summary>
        /// <param name="claims">Liste de droits.</param>
        void AddClaims(IEnumerable<IClaim> claims);

        /// <summary>
        /// Suppression d'un droit à l'utilisateur.
        /// </summary>
        /// <param name="claim">Droit.</param>

        void RemoveClaim(IClaim claim);

        /// <summary>
        /// Suppression d'un droit à l'utilisateur.
        /// </summary>
        /// <param name="claims">Liste de droits.</param>
        void RemoveClaims(IEnumerable<IClaim> claims);

        /// <summary>
        /// Indique si l'utilisateur a un droit particulier.
        /// </summary>
        /// <param name="claim">Droit.</param>
        /// <returns>Indique si l'utilisateur a le droit particulier ou non.</returns>
        bool Claim(IClaim claim);
    }
}
