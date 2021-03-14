using Puffix.Cqrs.Context;
using Puffix.Cqrs.EventHandlers;
using Puffix.Cqrs.Models;
using Puffix.Cqrs.Repositories;
using Puffix.Cqrs.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Puffix.Cqrs.Events
{
    /// <summary>
    /// Service pour la gestion des évènements.
    /// </summary>
    public class EventService : IEventService
    {
        /// <summary>
        /// Service de gestion des sources de données.
        /// </summary>
        private readonly IRepositoryService repositoryService;

        /// <summary>
        /// Service de gestion des intercepteurs d'évènements.
        /// </summary>
        private readonly IEventHandlerService eventHandlerService;

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="repositoryService">Service de gestion des sources de données.</param>
        /// <param name="eventHandlerService">Service de gestion des intercepteurs d'évènements.</param>
        public EventService(IRepositoryService repositoryService, IEventHandlerService eventHandlerService)
        {
            this.repositoryService = repositoryService;
            this.eventHandlerService = eventHandlerService;
        }

        /// <summary>
        /// Levée d'un évènement.
        /// </summary>
        /// <typeparam name="AggregateT">Type de l'agrégat.</typeparam>
        /// <typeparam name="IndexT">Type de l'index.</typeparam>
        /// <param name="raisedEvent">Evènement.</param>
        /// <param name="applicationContext">Contexte de l'application.</param>
        /// <returns>Agrégat.</returns>
        public async Task<AggregateT> Raise<AggregateT, IndexT>(IEvent<AggregateT, IndexT> raisedEvent, IApplicationContext applicationContext)
            where AggregateT : IAggregate<IndexT>
            where IndexT : IComparable, IComparable<IndexT>, IEquatable<IndexT>
        {
            // Construction des informations sur l'évènement et sauvegarde.
            IEventInformation eventInformation = BuildEventInformation(raisedEvent, applicationContext.CurrentUser);
            // TODO : stokcage des évènements.

            // Recherche de la source de données pour le type d'agrégat.
            IRepository<AggregateT, IndexT> repository = repositoryService.GetRepository<AggregateT, IndexT>();

            // Recherche de l'agrégat et création s'il n'existe pas.
            AggregateT aggregate = await repository.GetByIdOrDefaultAsync(raisedEvent.AggregateId);
            if (aggregate == null)
            {
                // Construction de l'agrégat.
                aggregate = BuildAggregate<AggregateT, IndexT>();

                // Création d'un identifiant pour l'évènement.
                raisedEvent.AggregateId = await repository.GetNextAggregatetIdAsync(aggregate.GenerateNextId);
            }

            // Application des modifications.
            raisedEvent.Apply(aggregate);

            // Lancement en asynchrone de l'évènement pour prise en compte dans EventHandler.
            await ActivateEventHanlders(raisedEvent, aggregate);

            return aggregate;
        }

        /// <summary>
        /// Construction des informations sur l'évènement.
        /// </summary>
        /// <param name="raisedEvent">Evènement.</param>
        /// <param name="currentUser">Utilisateur courant.</param>
        /// <returns>Informations sur l'évènement.</returns>
        private IEventInformation BuildEventInformation(IEvent raisedEvent, IApplicationUser currentUser)
        {
            return new EventInformation
            {
                Date = DateTime.UtcNow,
                User = currentUser,
                Event = raisedEvent,
                EventType = raisedEvent.GetType().FullName,
            };
        }

        /// <summary>
        /// Génération d'un agrégat.
        /// </summary>
        /// <typeparam name="AggregateT">Type de l'agrégat.</typeparam>
        /// <typeparam name="IndexT">Type de l'index.</typeparam>
        /// <returns>Agrégat.</returns>
        private AggregateT BuildAggregate<AggregateT, IndexT>()
            where AggregateT : IAggregate<IndexT>
            where IndexT : IComparable, IComparable<IndexT>, IEquatable<IndexT>
        {
            // Recherche des informations de l'agrégat pour en extraire l'implémentation
            AggregateInfo aggregateInfo = repositoryService.GetInfo<AggregateT>();
            Type implementationType = aggregateInfo.ImplementationType;

            // Instanciation d'un agrégat.
            return (AggregateT)Activator.CreateInstance(implementationType);
        }

        /// <summary>
        /// Activation des gestionnaires d'évènements.
        /// </summary>
        /// <param name="raisedEvent">Evènement.</param>
        /// <param name="aggregate">Agrégat.</param>
        /// <returns>Tache aysynchrone./returns>
        private async Task ActivateEventHanlders<AggregateT, IndexT>(IEvent<AggregateT, IndexT> raisedEvent, AggregateT aggregate)
            where AggregateT : IAggregate<IndexT>
            where IndexT : IComparable, IComparable<IndexT>, IEquatable<IndexT>
        {
            // Recherche des ancêtres et interfaces de l'évènement.
            IEnumerable<Type> eventTypeAncestors = TypeTools.GetAllAncestorsAndInterfacesForType(raisedEvent.GetType());

            // Filtre sur les EventHandlers, et récupération des actions de prise en charge.
            IEnumerable<Func<Task>> handlers = eventTypeAncestors
                .SelectMany(ancestorType => eventHandlerService.GetEventHandlers(ancestorType))
                .Select(handler => handler.GetHandleAction(aggregate, raisedEvent));

            // Attente de la fin d'exécution des traitements de prise en charge des évènements.
            await Task.WhenAll(handlers.Select(handlingActions => handlingActions()));
        }
    }
}
