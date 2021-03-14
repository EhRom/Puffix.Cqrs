using System;
using System.Collections.Generic;
using System.Linq;

namespace Puffix.Cqrs.EventHandlers
{
    /// <summary>
    /// Service de gestion des intercepteurs d'évènements.
    /// </summary>
    public class EventHandlerService : IEventHandlerService
    {
        /// <summary>
        /// Dictionnaire des informations sur les agrégats enregistrés.
        /// </summary>
        private readonly IDictionary<Type, List<EventHandlerInfo>> eventHandlerInfoContainer;

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="eventHandlerInfoContainer">Dictionnaire des informations sur les agrégats enregistrés.</param>
        public EventHandlerService(IDictionary<Type, List<EventHandlerInfo>> eventHandlerInfoContainer)
        {
            // Référencement du dictionnaire.
            this.eventHandlerInfoContainer = eventHandlerInfoContainer;
        }

        /// <summary>
        /// Recherche des intercepteurs d'évènements associés à l'évènement.
        /// </summary>
        /// <param name="eventType">Type de l'évènement.</param>
        /// <returns>Liste des intercepteurs d'évènements associés à l'évènement.</returns>
        public IEnumerable<EventHandlerInfo> GetEventHandlers(Type eventType)
        {
            // Recherche de l'évènement et renvoi de la liste des intercepteurs inscrits, ou une liste vide.
            return eventHandlerInfoContainer.ContainsKey(eventType) ?
                        eventHandlerInfoContainer[eventType] :
                        Enumerable.Empty<EventHandlerInfo>();
        }
    }
}
