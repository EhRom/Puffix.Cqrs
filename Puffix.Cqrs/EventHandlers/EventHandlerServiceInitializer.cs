using Puffix.Cqrs.Events;
using Puffix.Cqrs.Models;
using Puffix.Cqrs.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Puffix.Cqrs.EventHandlers;

/// <summary>
/// Event handler initialization service.
/// </summary>
public class EventHandlerServiceInitializer : IEventHandlerServiceInitializer
{
    private const string HANDLE_ASYNC_METHOD_NAME = "HandleAsync";

    private readonly Func<Type, object> resolver;
    private readonly IDictionary<Type, List<EventHandlerInfo>> eventHandlerInfoContainer;

    /// <summary>
    /// Event hanlder information container.
    /// </summary>
    public IDictionary<Type, List<EventHandlerInfo>> EventHandlerInfoContainer => eventHandlerInfoContainer;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="resolver">Function to resolve objects (IoC).</param>
    public EventHandlerServiceInitializer(Func<Type, object> resolver)
    {
        this.resolver = resolver;

        eventHandlerInfoContainer = new Dictionary<Type, List<EventHandlerInfo>>();
    }

    /// <summary>
    /// Register events handlers.
    /// </summary>
    /// <param name="assemblies">Assemblies to process.</param>
    public void RegisterEventHandlers(params Assembly[] assemblies)
    {
        // Register data repositories
        TypeTools.ProcessTypesMatchingAttribute<EventHandlerAttribute>((currentContract, currentImplementation) => RegisterEventHandler(currentContract, currentImplementation), assemblies);
    }

    /// <summary>
    /// Register event handler.
    /// </summary>
    /// <param name="contractType">Contract type.</param>
    /// <param name="implementationType">Implementation type.</param>
    public void RegisterEventHandler(Type contractType, Type implementationType)
    {
        // Defines the search predicates for event handlers contracts.
        Predicate<Type>[] predicates = new Predicate<Type>[]
        {
            type => type.IsConstructedGenericType && type.GetGenericTypeDefinition() == typeof(IEventHandler<>),
            type => type.IsConstructedGenericType && type.GetGenericTypeDefinition() == typeof(IEventHandler<,,,>)
        };

        // Extract event handlers contracts (interfaces) matching the filter predicates for event handlers contracts.
        IEnumerable<Type> matchingEventHandlerContracts = TypeTools.GetAllInterfacesForType(contractType)
                // Select interfaces mathcing predicates
                .SelectMany(interfaceType => predicates
                        .Select(p => new { InterfaceType = interfaceType, Predicate = p })
                        .Where(p => p.Predicate(p.InterfaceType)))
                        .Select(p => p.InterfaceType).ToList();

        // Browse the interfaces corresponding to the predicates, and extract the information.
        foreach (Type matchingContract in matchingEventHandlerContracts)
        {
            Type aggregateImplementationType, agregateType, indexType;
            Func<IAggregate, IEvent, Task> handlingFunction;
            Type[] contractGenericArguments = matchingContract.GetGenericArguments();
            Type eventType = contractGenericArguments.ElementAt(0);

            // Create functions for the interception of events, depending on the number of generic arguments.
            if (contractGenericArguments.Count() == 1)
            {
                aggregateImplementationType = null;
                agregateType = null;
                indexType = null;

                // Extract the event management method in the implementation of the interceptor.
                MethodInfo method = implementationType.GetRuntimeMethod(HANDLE_ASYNC_METHOD_NAME, new[] { eventType });

                if (method == null)
                    throw new ArgumentNullException($"The method {HANDLE_ASYNC_METHOD_NAME} is not found in the {eventType} class.");

                // Define the function to call the event handler method.
                handlingFunction = (aggregate, handledEvent) =>
                {
                    return (Task)method.Invoke(resolver(contractType), new object[] { handledEvent });
                };
            }
            else
            {
                aggregateImplementationType = contractGenericArguments.ElementAt(1);
                agregateType = contractGenericArguments.ElementAt(2);
                indexType = contractGenericArguments.ElementAt(3);

                // Extract the event management method in the implementation of the interceptor.
                MethodInfo method = implementationType.GetRuntimeMethod(HANDLE_ASYNC_METHOD_NAME, new[] { agregateType, eventType });

                if (method == null)
                    throw new ArgumentNullException($"The method {HANDLE_ASYNC_METHOD_NAME} is not found in the {eventType} class.");

                // Define the function to call the event handler method.
                handlingFunction = (aggregate, handledEvent) =>
                {
                    return (Task)method.Invoke(resolver(contractType), new object[] { aggregate, handledEvent });
                };
            }

            // Register the interceptor's information, classified by type of event.
            if (!eventHandlerInfoContainer.ContainsKey(eventType))
                eventHandlerInfoContainer.Add(eventType, new List<EventHandlerInfo>());
            eventHandlerInfoContainer[eventType].Add(new EventHandlerInfo(contractType, eventType, agregateType, indexType, handlingFunction));
        }
    }
}
