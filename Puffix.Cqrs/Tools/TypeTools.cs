using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Puffix.Cqrs.Tools
{
    /// <summary>
    /// Outil pour les types.
    /// </summary>
    public static class TypeTools
    {
        /// <summary>
        /// Recherche de l'ensemble des ancêtres et des interfaces d'un type.
        /// </summary>
        /// <param name="type">Type à explorer.</param>
        /// <returns>Liste de types.</returns>
        public static IEnumerable<Type> GetAllAncestorsAndInterfacesForType(Type type)
        {
            IEnumerable<Type> interfaces = GetAllInterfacesForType(type);
            IEnumerable<Type> ancestors = GetAllAncestorsForType(type);

            return interfaces.Union(ancestors);
        }

        /// <summary>
        /// Recherche de l'ensemble des interfaces d'un type.
        /// </summary>
        /// <param name="type">Type à explorer.</param>
        /// <returns>Liste de types.</returns>
        public static IEnumerable<Type> GetAllInterfacesForType(Type type)
        {
            return type.GetTypeInfo().ImplementedInterfaces;
        }

        /// <summary>
        /// Recherche de l'ensemble des ancêtres d'un type.
        /// </summary>
        /// <param name="type">Type à explorer.</param>
        /// <returns>Liste de types.</returns>
        public static IEnumerable<Type> GetAllAncestorsForType(Type type)
        {
            IList<Type> ancestors;

            if (type.BaseType == null || type.BaseType == typeof(object))
                ancestors = new List<Type> { type };
            else
            {
                ancestors = GetAllAncestorsForType(type.BaseType) as IList<Type>;
                if (ancestors == null)
                    throw new ArgumentNullException("The GetAllAncestorsForType method should not return null values.");

                ancestors.Add(type);
            }

            return ancestors;
        }

        /// <summary>
        /// Traitement des types déclarant un attribut, ainsi que la liste des types implémetants ce type.
        /// </summary>
        /// <typeparam name="AttributeT">Type de l'attribut recherché.</typeparam>
        /// <param name="processType">Traitement à effectuer.</param>
        /// <param name="assemblies">Liste des librairies à explorer.</param>
        public static void ProcessTypesMatchingAttribute<AttributeT>(Action<Type, Type> processType, params Assembly[] assemblies)
           where AttributeT : Attribute
        {
            // Définition du prédicat pour la recherche de l'attribut.
            Predicate<Type> predicate = new Predicate<Type>(type => type.GetTypeInfo().GetCustomAttributes<AttributeT>().Any());

            // Extraction des contrats d'agrégats et des implémentations.
            IDictionary<Type, IEnumerable<Type>> contractsAndImplementations = ExtractTypesMatchingPredicate(predicate, assemblies);

            // Parcours des différents types.
            foreach (Type currentContract in contractsAndImplementations.Keys)
            {
                // Parcours des implémentations.
                foreach (Type currentImplementation in contractsAndImplementations[currentContract])
                {
                    // Traitement des contrats et implémentations.
                    processType(currentContract, currentImplementation);
                }
            }
        }

        /// <summary>
        /// Extraction des types correspondants à un prédicat, ainsi que la liste des types implémetants ce type.
        /// </summary>
        /// <param name="predicate">Predicat de correspondance.</param>
        /// <param name="assemblies">Liste des librairies à explorer.</param>
        /// <returns>Dictionnaire des types et des implementations asociées.</returns>
        public static IDictionary<Type, IEnumerable<Type>> ExtractTypesMatchingPredicate(Predicate<Type> predicate, params Assembly[] assemblies)
        {
            // Extraction des types des assemblies à analyser.
            IEnumerable<Type> allTypes = assemblies.SelectMany(assembly => assembly.GetTypes());

            // Filtrage des types selon le prédicat (Contrat).
            return allTypes.Where(typeInfo => predicate(typeInfo))
                            .SelectMany(aggregateContract => allTypes
                                // Combinaison du contrat et des candidats à l'immpléméntation.
                                .Select(implementationCandidate => new { CandidateType = implementationCandidate, ContractType = aggregateContract })
                                // Filtre sur les types qui héritent du contrat.
                                .Where(implementationCandidate => implementationCandidate.ContractType.IsAssignableFrom(implementationCandidate.CandidateType) && !implementationCandidate.ContractType.Equals(implementationCandidate.CandidateType)))
                            // Regroupement par contrat d'agrégat.
                            .GroupBy(aggregateImplementation => aggregateImplementation.ContractType)
                            .ToDictionary(contractImp => contractImp.Key, contractImp => contractImp.Select(p => p.CandidateType).ToList() as IEnumerable<Type>);
        }
    }
}
