using System;
using System.Collections.Generic;

namespace Puffix.Cqrs.EventHandlers
{
    /// <summary>
    /// Contrat du service de gestion des intercepteurs d'évènements.
    /// </summary>
    public interface IEventHandlerService
    {
        /// <summary>
        /// Recherche des intercepteurs d'évènements associés à l'évènement.
        /// </summary>
        /// <param name="eventType">Type de l'évènement.</param>
        /// <returns>Liste des intercepteurs d'évènements associés à l'évènement.</returns>
        IEnumerable<EventHandlerInfo> GetEventHandlers(Type eventType);
    }
}
