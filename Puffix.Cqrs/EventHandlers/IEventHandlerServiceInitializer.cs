using System;
using System.Collections.Generic;
using System.Reflection;

namespace Puffix.Cqrs.EventHandlers
{
    /// <summary>
    /// Contrat du service d'initialisation du service de gestion des intercepteurs d'évènements.
    /// </summary>
    public interface IEventHandlerServiceInitializer
    {
        /// <summary>
        /// Dictionnaire des informations sur les agrégats enregistrés.
        /// </summary>
        IDictionary<Type, List<EventHandlerInfo>> EventHandlerInfoContainer { get; }

        /// <summary>
        /// Enregistrements des intercepteurs d'évènements.
        /// </summary>
        /// <param name="assemblies">Liste des librairies à scanner.</param>
        void RegisterEventHandlers(params Assembly[] assemblies);

        /// <summary>
        /// Enregistrement d'un intercepteur d'évènements.
        /// </summary>
        /// <param name="contractType">Type du contrat.</param>
        /// <param name="implementationType">Type de l'implémentation.</param>
        void RegisterEventHandler(Type contractType, Type implementationType);
    }
}
