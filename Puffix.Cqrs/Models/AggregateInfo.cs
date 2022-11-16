using System;

namespace Puffix.Cqrs.Models
{
    /// <summary>
    /// Aggregate information.
    /// </summary>
    public class AggregateInfo
    {
        /// <summary>
        /// Aggregate type.
        /// </summary>
        public Type AggregateType { get; }

        /// <summary>
        /// Index type.
        /// </summary>
        public Type IndexType { get; }

        /// <summary>
        /// Aggregate implementation type.
        /// </summary>
        public Type ImplementationType { get; }

        /// <summary>
        /// Collection name.
        /// </summary>
        public string CollectionName { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="aggregateType">Aggregate type.</param>
        /// <param name="indexType">Index type.</param>
        /// <param name="implementationType">Aggregate implementation type.</param>
        /// <param name="collectionName">Collection name.</param>
        public AggregateInfo(Type aggregateType, Type indexType, Type implementationType, string collectionName)
        {
            AggregateType = aggregateType;
            IndexType = indexType;
            ImplementationType = implementationType;
            CollectionName = collectionName;
        }
    }
}
