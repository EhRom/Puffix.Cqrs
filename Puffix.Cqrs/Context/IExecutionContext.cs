using Puffix.Cqrs.Configurations;
using Puffix.Cqrs.Events;
using Puffix.Cqrs.Repositories;

namespace Puffix.Cqrs.Context
{
    /// <summary>
    /// Contexte d'exécution de la commande.
    /// </summary>
    public interface IExecutionContext
    {
        /// <summary>
        /// Service de gestion des évènements.
        /// </summary>
        IEventService EventService { get; }

        /// <summary>
        /// Service pour l'accès aux répertoires de données.
        /// </summary>
        IRepositoryService RepositoryService { get; }

        /// <summary>
        /// Service de gestion de la configuration de l'application.
        /// </summary>
        IApplicationConfigurationService ConfigurationService { get; }
    }
}
