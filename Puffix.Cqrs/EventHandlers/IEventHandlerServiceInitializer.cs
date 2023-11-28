using System;
using System.Collections.Generic;
using System.Reflection;

namespace Puffix.Cqrs.EventHandlers;

/// <summary>
/// Event handler initialization service contract.
/// </summary>
public interface IEventHandlerServiceInitializer
{
    /// <summary>
    /// Event hanlder information container.
    /// </summary>
    IDictionary<Type, List<EventHandlerInfo>> EventHandlerInfoContainer { get; }

    /// <summary>
    /// Register events handlers.
    /// </summary>
    /// <param name="assemblies">Assemblies to process.</param>
    void RegisterEventHandlers(params Assembly[] assemblies);

    /// <summary>
    /// Register event handler.
    /// </summary>
    /// <param name="contractType">Contract type.</param>
    /// <param name="implementationType">Implementation type.</param>
    void RegisterEventHandler(Type contractType, Type implementationType);
}
