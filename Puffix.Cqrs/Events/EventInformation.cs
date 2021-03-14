using Puffix.Cqrs.Context;
using System;

namespace Puffix.Cqrs.Events
{
    /// <summary>
    /// Information sur un évènement.
    /// </summary>
    public class EventInformation : IEventInformation
    {
        /// <summary>
        /// Identifiant de l'évènement.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Date de l'évènement.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Utilisateur.
        /// </summary>
        public IApplicationUser User { get; set; }

        /// <summary>
        /// Type de l'évènement.
        /// </summary>
        public string EventType { get; set; }

        /// <summary>
        /// Evènement.
        /// </summary>
        public IEvent Event { get; set; }

        /// <summary>
        /// Fonction pour la génération du prochain identifiant de l'agrégat.
        /// </summary>
        public Func<int, int> GenerateNextId => lastId => ++lastId;
    }
}
