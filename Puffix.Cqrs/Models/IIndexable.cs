using System;

namespace Puffix.Cqrs.Models
{
    /// <summary>
    /// Contrat pour un objet indexable.
    /// </summary>
    /// <typeparam name="IndexT">Type de l'index.</typeparam>
    public interface IIndexable<IndexT>
        where IndexT : IComparable, IComparable<IndexT>, IEquatable<IndexT>
    {
        /// <summary>
        /// Index.
        /// </summary>
        IndexT Id { get; }

        /// <summary>
        /// Fonction pour la génération du prochain identifiant de l'agrégat.
        /// </summary>
        /// <remarks>On utilise une fonction pour mieux isoler la génération de l'instance.</remarks>
        Func<IndexT, IndexT> GenerateNextId { get; }
    }
}
