using Puffix.Cqrs.Events;
using Puffix.Cqrs.Models;
using System;
using System.Threading.Tasks;

namespace Puffix.Cqrs.EventHandlers
{
    /// <summary>
    /// Informations sur l'intercepteur d'évènements
    /// </summary>
    public class EventHandlerInfo
    {
        /// <summary>
        /// Fonction d'interception de l'évènement.
        /// </summary>
        private readonly Func<IAggregate, IEvent, Task> handlingFunction;

        /// <summary>
        /// Nom de l'intercepteur.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Type de l'intercepteur d'évènement.
        /// </summary>
        public Type EventHandlerType { get; }

        /// <summary>
        /// Type de l'évènement intercepté.
        /// </summary>
        public Type EventType { get; }

        /// <summary>
        /// Type de l'agrégat.
        /// </summary>
        public Type AgregateType { get; }

        /// <summary>
        /// Type de l'index.
        /// </summary>
        public Type IndexType { get; }

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="eventHandlerType">Type de l'itercepteur d'évènement.</param>
        /// <param name="eventType">Type de l'évènement intercepté.</param>
        /// <param name="agregateType">Type de l'agrégat.</param>
        /// <param name="handlingFunction">Action pour l'interception.</param>
        public EventHandlerInfo(Type eventHandlerType, Type eventType, Type agregateType, Type indexType, Func<IAggregate, IEvent, Task> handlingFunction)
        {
            EventHandlerType = eventHandlerType;
            EventType = eventType;
            Name = eventHandlerType.Name;
            AgregateType = agregateType;
            IndexType = indexType;

            this.handlingFunction = handlingFunction;
        }

        /// <summary>
        /// Récupération de l'action 
        /// </summary>
        /// <param name="aggregate">Agrégat.</param>
        /// <param name="handledEvent">Evènement intercepté.</param>
        /// <returns>Tâche asynchone.</returns>
        public Func<Task> GetHandleAction(IAggregate aggregate, IEvent handledEvent)
        {
            return () => handlingFunction(aggregate, handledEvent);
        }
    }
}
