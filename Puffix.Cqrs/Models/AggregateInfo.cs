using System;

namespace Puffix.Cqrs.Models
{
    /// <summary>
    /// Informations sur un agrégat.
    /// </summary>
    public class AggregateInfo
    {
        /// <summary>
        /// Type de l'agrégat.
        /// </summary>
        public Type AggregateType { get; }

        /// <summary>
        /// Type de l'index.
        /// </summary>
        public Type IndexType { get; }

        /// <summary>
        /// Type de l'implémentation de l'agrégat.
        /// </summary>
        public Type ImplementationType { get; }

        /// <summary>
        /// Nom de la collection.
        /// </summary>
        public string CollectionName { get; }

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="aggregateType">Type de l'agrégat.</param>
        /// <param name="indexType">Type de l'index.</param>
        /// <param name="implementationType">Type de l'implémentation de l'agrégat.</param>
        /// <param name="collectionName">Nom de la collection.</param>
        public AggregateInfo(Type aggregateType, Type indexType, Type implementationType, string collectionName)
        {
            AggregateType = aggregateType;
            IndexType = indexType;
            ImplementationType = implementationType;
            CollectionName = collectionName;
        }
    }
}
