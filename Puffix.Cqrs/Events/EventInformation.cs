using Puffix.Cqrs.Context;
using System;

namespace Puffix.Cqrs.Events
{
    /// <summary>
    /// Event information.
    /// </summary>
    public class EventInformation : IEventInformation
    {
        /// <summary>
        /// Event id.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Event date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// User.
        /// </summary>
        public IApplicationUser User { get; set; }

        /// <summary>
        /// Event type.
        /// </summary>
        public string EventType { get; set; }

        /// <summary>
        /// Event.
        /// </summary>
        public IEvent Event { get; set; }

        /// <summary>
        /// Function to generate the next id for a new aggregate.
        /// </summary>
        public Func<int, int> GenerateNextId => lastId => ++lastId;
    }
}
