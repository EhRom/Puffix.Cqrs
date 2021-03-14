using Puffix.Cqrs.Context;
using Puffix.Cqrs.Models;
using System;

namespace Puffix.Cqrs.Events
{
    /// <summary>
    /// Information sur un évènement.
    /// </summary>
    public interface IEventInformation : IAggregate<int>
    {
        /// <summary>
        /// Date de l'évènement.
        /// </summary>
        DateTime Date { get; }

        /// <summary>
        /// Utilisateur.
        /// </summary>
        IApplicationUser User { get; }

        /// <summary>
        /// Type de l'évènement.
        /// </summary>
        string EventType { get; }

        /// <summary>
        /// Evènement.
        /// </summary>
        IEvent Event { get; }
    }
}
