using Puffix.Cqrs.Context;
using Puffix.Cqrs.EventHandlers;
using Puffix.Cqrs.Models;
using Puffix.Cqrs.Repositories;
using Puffix.Cqrs.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Puffix.Cqrs.Events;

/// <summary>
/// Service to manage events.
/// </summary>
public class EventService : IEventService
{
    private readonly IRepositoryService repositoryService;
    private readonly IEventHandlerService eventHandlerService;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="repositoryService">Repository management service.</param>
    /// <param name="eventHandlerService">Event handler management service.</param>
    public EventService(IRepositoryService repositoryService, IEventHandlerService eventHandlerService)
    {
        this.repositoryService = repositoryService;
        this.eventHandlerService = eventHandlerService;
    }

    /// <summary>
    /// Raise event.
    /// </summary>
    /// <typeparam name="AggregateImplementationT">Aggregate implementation type.</typeparam>
    /// <typeparam name="AggregateT">Aggregate type.</typeparam>
    /// <typeparam name="IndexT">Index type.</typeparam>
    /// <param name="raisedEvent">Event to raise.</param>
    /// <param name="applicationContext">Application context.</param>
    /// <returns>Aggregate.</returns>
    public async Task<AggregateT> Raise<AggregateImplementationT, AggregateT, IndexT>(IEvent<AggregateImplementationT, AggregateT, IndexT> raisedEvent, IApplicationContext applicationContext)
        where AggregateImplementationT : class, AggregateT
        where AggregateT : IAggregate<IndexT>
        where IndexT : IComparable, IComparable<IndexT>, IEquatable<IndexT>
    {
        // Build and save information on the raised event.
        IEventInformation eventInformation = BuildEventInformation(raisedEvent, applicationContext.CurrentUser);
        // TODO : store events.

        // Find the repository associated to the aggregate type.
        IRepository<AggregateImplementationT, AggregateT, IndexT> repository = repositoryService.GetRepository<AggregateImplementationT, AggregateT, IndexT>();

        // Search for the aggregate and create it if it does not exist.
        AggregateT aggregate = await repository.GetByIdOrDefaultAsync(raisedEvent.AggregateId);
        if (aggregate == null)
        {
            // Build aggregate.
            aggregate = BuildAggregate<AggregateT, IndexT>();

            // Create aggregate id.
            raisedEvent.AggregateId = await repository.GetNextAggregatetIdAsync(aggregate.GenerateNextId);
        }

        // Apply modifications.
        raisedEvent.Apply(aggregate);

        // Raise asynchronous event to be handled by the event handler.
        await ActivateEventHanlders(raisedEvent, aggregate);

        return aggregate;
    }

    /// <summary>
    /// Build object to store event information.
    /// </summary>
    /// <param name="raisedEvent">Raised event.</param>
    /// <param name="currentUser">Current user.</param>
    /// <returns>Event information.</returns>
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
    /// Build aggregate.
    /// </summary>
    /// <typeparam name="AggregateT">Aggregate type.</typeparam>
    /// <typeparam name="IndexT">Index type.</typeparam>
    /// <returns>Aggregate.</returns>
    private AggregateT BuildAggregate<AggregateT, IndexT>()
        where AggregateT : IAggregate<IndexT>
        where IndexT : IComparable, IComparable<IndexT>, IEquatable<IndexT>
    {
        // Search for aggregate information to get implementation.
        AggregateInfo aggregateInfo = repositoryService.GetInfo<AggregateT>();
        Type implementationType = aggregateInfo.ImplementationType;

        // Instanciate aggregate.
        return (AggregateT)Activator.CreateInstance(implementationType);
    }

    /// <summary>
    /// Activate event handlers.
    /// </summary>
    /// <param name="raisedEvent">Raised event.</param>
    /// <param name="aggregate">Aggregate.</param>
    /// <returns>Async task./returns>
    private async Task ActivateEventHanlders<AggregateImplementationT, AggregateT, IndexT>(IEvent<AggregateImplementationT, AggregateT, IndexT> raisedEvent, AggregateT aggregate)
        where AggregateImplementationT : class, AggregateT
        where AggregateT : IAggregate<IndexT>
        where IndexT : IComparable, IComparable<IndexT>, IEquatable<IndexT>
    {
        // Search for ancestors and event interfaces.
        IEnumerable<Type> eventTypeAncestors = TypeTools.GetAllAncestorsAndInterfacesForType(raisedEvent.GetType());

        // Filtering on EventHandlers, and retrieve handler actions.
        IEnumerable<Func<Task>> handlers = eventTypeAncestors
                .SelectMany(ancestorType => eventHandlerService.GetEventHandlers(ancestorType))
                .Select(handler => handler.GetHandleAction(aggregate, raisedEvent));

        // Waiting for the end of the event handling process.
        await Task.WhenAll(handlers.Select(handlingActions => handlingActions()));
    }
}
