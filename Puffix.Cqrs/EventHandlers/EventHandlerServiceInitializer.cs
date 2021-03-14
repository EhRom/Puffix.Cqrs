using Puffix.Cqrs.Events;
using Puffix.Cqrs.Models;
using Puffix.Cqrs.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Puffix.Cqrs.EventHandlers
{
    /// <summary>
    /// Service d'initialisation du service de gestion des intercepteurs d'évènements.
    /// </summary>
    public class EventHandlerServiceInitializer : IEventHandlerServiceInitializer
    {
        /// <summary>
        /// Fonction pour la résolution de classes via le conteneur IoC.
        /// </summary>
        private readonly Func<Type, object> resolver;

        /// <summary>
        /// Dictionnaire des informations sur les agrégats enregistrés.
        /// </summary>
        private readonly IDictionary<Type, List<EventHandlerInfo>> eventHandlerInfoContainer;

        /// <summary>
        /// Dictionnaire des informations sur les agrégats enregistrés.
        /// </summary>
        public IDictionary<Type, List<EventHandlerInfo>> EventHandlerInfoContainer => eventHandlerInfoContainer;

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="resolver">Fonction pour la résolution de classes via le conteneur IoC.</param>
        public EventHandlerServiceInitializer(Func<Type, object> resolver)
        {
            // Enregistrement de l'action de résolution.
            this.resolver = resolver;

            // Instanciation du dictionnaire.
            eventHandlerInfoContainer = new Dictionary<Type, List<EventHandlerInfo>>();
        }

        /// <summary>
        /// Enregistrements des intercepteurs d'évènements.
        /// </summary>
        /// <param name="assemblies">Liste des librairies à scanner.</param>
        public void RegisterEventHandlers(params Assembly[] assemblies)
        {
            // Enregistrement des répertoires de données.
            TypeTools.ProcessTypesMatchingAttribute<EventHandlerAttribute>((currentContract, currentImplementation) => RegisterEventHandler(currentContract, currentImplementation), assemblies);
        }

        /// <summary>
        /// Enregistrement d'un intercepteur d'évènements.
        /// </summary>
        /// <param name="contractType">Type du contrat.</param>
        /// <param name="implementationType">Type de l'implémentation.</param>
        public void RegisterEventHandler(Type contractType, Type implementationType)
        {
            // Définition des prédicats de recherche des contrats d'intercepteurs d'évènements.
            Predicate<Type>[] predicates = new Predicate<Type>[]
            {
                type => type.IsConstructedGenericType && type.GetGenericTypeDefinition() == typeof(IEventHandler<>),
                type => type.IsConstructedGenericType && type.GetGenericTypeDefinition() == typeof(IEventHandler<,,>)
            };

            // Extraction des interfaces pour les contrats d'intercetpeurs d'évènements correspondantes aux prédicats
            // de filtre pour les contrats de base d'intercepteurs d'évènements.
            IEnumerable<Type> matchingEventHandlerContracts = TypeTools.GetAllInterfacesForType(contractType)
                    // Sélection des interfaces correspondantes aux prédicats.
                    .SelectMany(interfaceType => predicates
                            .Select(p => new { InterfaceType = interfaceType, Predicate = p })
                            .Where(p => p.Predicate(p.InterfaceType)))
                            .Select(p => p.InterfaceType).ToList();

            // Parcours des interfaces correspondantes aux prédicats, et extraction des informations.
            foreach (Type matchingContract in matchingEventHandlerContracts)
            {
                Type agregateType, indexType;
                Func<IAggregate, IEvent, Task> handlingFunction;
                Type[] contractGenericArguments = matchingContract.GetGenericArguments();
                Type eventType = contractGenericArguments.ElementAt(0);

                // Création des fonctions pour l'interception des évènements, en fonction du nombre d'arguments génériques.
                if (contractGenericArguments.Count() == 1)
                {
                    agregateType = null;
                    indexType = null;

                    // Extraction de la méthode de gestion de l'évènement dans l'implémentation de l'intercepteur.
                    MethodInfo method = implementationType.GetRuntimeMethod("Handle", new[] { eventType });

                    // Définition de la fonction pour appeler la méthode de gestion de l'évènement.
                    handlingFunction = (aggregate, handledEvent) =>
                    {
                        return (Task)method.Invoke(resolver(contractType), new object[] { handledEvent });
                    };
                }
                else
                {
                    agregateType = contractGenericArguments.ElementAt(1);
                    indexType = contractGenericArguments.ElementAt(2);

                    // Extraction de la méthode de gestion de l'évènement dans l'implémentation de l'intercepteur.
                    MethodInfo method = implementationType.GetRuntimeMethod("Handle", new[] { agregateType, eventType });

                    // Définition de la fonction pour appeler la méthode de gestion de l'évènement.
                    handlingFunction = (aggregate, handledEvent) =>
                    {
                        return (Task)method.Invoke(resolver(contractType), new object[] { aggregate, handledEvent });
                    };
                }

                // Enregistrement des informations de l'intercepteur, classé par type d'évènement.
                if (!eventHandlerInfoContainer.ContainsKey(eventType))
                    eventHandlerInfoContainer.Add(eventType, new List<EventHandlerInfo>());
                eventHandlerInfoContainer[eventType].Add(new EventHandlerInfo(contractType, eventType, agregateType, indexType, handlingFunction));
            }
        }
    }
}
