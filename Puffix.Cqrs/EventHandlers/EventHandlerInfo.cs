using Puffix.Cqrs.Events;
using Puffix.Cqrs.Models;
using System;
using System.Threading.Tasks;

namespace Puffix.Cqrs.EventHandlers
{
    /// <summary>
    /// Event handler information.
    /// </summary>
    public class EventHandlerInfo
    {
        /// <summary>
        /// Function to handle event.
        /// </summary>
        private readonly Func<IAggregate, IEvent, Task> handlingFunction;

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Event hanlder type.
        /// </summary>
        public Type EventHandlerType { get; }

        /// <summary>
        /// Event type.
        /// </summary>
        public Type EventType { get; }

        /// <summary>
        /// Aggregate type.
        /// </summary>
        public Type AggregateType { get; }

        /// <summary>
        /// Index type.
        /// </summary>
        public Type IndexType { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="eventHandlerType">Event hanlder type.</param>
        /// <param name="eventType">Event type.</param>
        /// <param name="agregateType">Aggregate type.</param>
        /// <param name="indexType">Index type.</param>
        /// <param name="handlingFunction">Function to handle event.</param>
        public EventHandlerInfo(Type eventHandlerType, Type eventType, Type agregateType, Type indexType, Func<IAggregate, IEvent, Task> handlingFunction)
        {
            EventHandlerType = eventHandlerType;
            EventType = eventType;
            Name = eventHandlerType.Name;
            AggregateType = agregateType;
            IndexType = indexType;

            this.handlingFunction = handlingFunction;
        }

        /// <summary>
        /// Get event action.
        /// </summary>
        /// <param name="aggregate">Aggregate.</param>
        /// <param name="handledEvent">Handled event.</param>
        /// <returns>Async function.</returns
        public Func<Task> GetHandleAction(IAggregate aggregate, IEvent handledEvent)
        {
            return () => handlingFunction(aggregate, handledEvent);
        }
    }
}
