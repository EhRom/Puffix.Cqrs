using Puffix.Cqrs.EventHandlers;
using Puffix.Cqrs.Models;
using Puffix.Cqrs.Repositories.Handlers;
using Puffix.Cqrs.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Puffix.Cqrs.Repositories;

/// <summary>
/// Service d'initialisation du service pour la gestion des répertoires de données.
/// </summary>
public class RepositoryServiceInitializer : IRepositoryServiceInitializer
{
    /// <summary>
    /// Service de gestion des intercepteurs d'évènements.
    /// </summary>
    private readonly IEventHandlerServiceInitializer eventHandlerServiceInitializer;

    /// <summary>
    /// Action pour l'enregistrement dans le conteneur IoC.
    /// </summary>
    private readonly Action<Type, Type> registerForIoc;

    /// <summary>
    /// Dictionnaire des informations sur les agrégats enregistrés.
    /// </summary>
    private readonly IDictionary<Type, AggregateInfo> aggregateInfoContainer;

    /// <summary>
    /// Dictionnaire des informations sur les agrégats enregistrés.
    /// </summary>
    public IDictionary<Type, AggregateInfo> AggregateInfoContainer => aggregateInfoContainer;

    /// <summary>
    /// Constructeur.
    /// </summary>
    /// <param name="eventHandlerServiceInitializer">Service d'initialisation du service de gestion des intercepteurs d'évènements.</param>
    /// <param name="registerForIoc">Action pour l'enregistrement dans le conteneur IoC.</param>
    public RepositoryServiceInitializer(IEventHandlerServiceInitializer eventHandlerServiceInitializer, Action<Type, Type> registerForIoc)
    {
        // Référencement du service d'initialisation du service de gestion des intercepteurs d'évènements.
        this.eventHandlerServiceInitializer = eventHandlerServiceInitializer;

        // Référencement de l'action pour l'enregistrement dans le conteneur IoC.
        this.registerForIoc = registerForIoc;

        // Instanciation du dictionnaire.
        aggregateInfoContainer = new Dictionary<Type, AggregateInfo>();
    }

    /// <summary>
    /// Enregistrements des répertoires de données.
    /// </summary>
    /// <param name="assemblies">Liste des librairies à scanner.</param>
    public void RegisterRepositories(params Assembly[] assemblies)
    {
        // Enregistrement des répertoires de données.
        TypeTools.ProcessTypesMatchingAttribute<AggregateAttribute>((currentContract, currentImplementation) => Register(currentContract, currentImplementation), assemblies);
    }

    /// <summary>
    /// Enregistrement d'un répertoire de données.
    /// </summary>
    /// <param name="contractType">Type du contrat.</param>
    /// <param name="implementationType">Type de l'implémentation.</param>
    /// <param name="registerForIoc">Action pour l'enregistrmenet du répertoire dans le conteneur IoC.</param>
    private void Register(Type contractType, Type implementationType)
    {
        // Recherche du type d'index de l'agrégat.
        Type indexType = TypeTools.GetAllInterfacesForType(contractType)
                    .Where(t => t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(IAggregate<>)))
                    .Select(t => t.GetGenericArguments().FirstOrDefault()).FirstOrDefault();

        // Contrôle du type de l'index.
        if (indexType == null)
            throw new InvalidOperationException($"The aggregate {contractType} does not implement {typeof(IAggregate<>)}.");

        // Enregistrement des informations sur l'agrégat.
        AggregateInfo aggregatenfo = new AggregateInfo(contractType, indexType, implementationType, implementationType.Name);
        if (aggregateInfoContainer.ContainsKey(contractType))
            throw new ArgumentException($"The aggregate {contractType} is already registered.");
        aggregateInfoContainer[contractType] = aggregatenfo;

        // Création des types génériques.
        Type repositoryContract = typeof(IRepository<,,>).MakeGenericType(implementationType, contractType, indexType);
        Type repositoryImplementation = typeof(Repository<,,>).MakeGenericType(implementationType, contractType, indexType);

        // Enregistrement de la correspondance dans le conteneur IoC.
        registerForIoc(repositoryContract, repositoryImplementation);

        // Enregistrement des intercepteurs d'évènements par défaut.
        Type modificationEventHandlerContract = typeof(IModificationEventHandler<,,>).MakeGenericType(implementationType, contractType, indexType);
        Type modificationEventHandlerImplementation = typeof(ModificationEventHandler<,,>).MakeGenericType(implementationType, contractType, indexType);
        eventHandlerServiceInitializer.RegisterEventHandler(modificationEventHandlerContract, modificationEventHandlerImplementation);
        registerForIoc(modificationEventHandlerContract, modificationEventHandlerImplementation);

        Type deletionEventHandlerContract = typeof(IDeletionEventHandler<,,>).MakeGenericType(implementationType, contractType, indexType);
        Type deletionEventHandlerImplementation = typeof(DeletionEventHandler<,,>).MakeGenericType(implementationType, contractType, indexType);
        eventHandlerServiceInitializer.RegisterEventHandler(deletionEventHandlerContract, deletionEventHandlerImplementation);
        registerForIoc(deletionEventHandlerContract, deletionEventHandlerImplementation);
    }
}
