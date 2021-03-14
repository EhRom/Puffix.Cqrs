using Puffix.Cqrs.Configurations;
using Puffix.Cqrs.Events;
using Puffix.Cqrs.Repositories;

namespace Puffix.Cqrs.Context
{
    /// <summary>
    /// Contexte d'exécution.
    /// </summary>
    public class ExecutionContext : IExecutionContext
    {
        /// <summary>
        /// Service de gestion des évènements.
        /// </summary>
        private readonly IEventService eventService;

        /// <summary>
        /// Service pour l'accès aux répertoires de données.
        /// </summary>
        private readonly IRepositoryService repositoryService;

        /// <summary>
        /// Service de gestion de la configuration de l'application.
        /// </summary>
        public IApplicationConfigurationService configurationService;

        /// <summary>
        /// Service de gestion des évènements.
        /// </summary>
        public IEventService EventService => eventService;

        /// <summary>
        /// Service pour l'accès aux répertoires de données.
        /// </summary>
        public IRepositoryService RepositoryService => repositoryService;

        /// <summary>
        /// Service de gestion de la configuration de l'application.
        /// </summary>
        public IApplicationConfigurationService ConfigurationService => configurationService;

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="eventService">Service de gestion des commandes.</param>
        /// <param name="repositoryService">Service pour l'accès aux répertoires de données.</param>
        /// <param name="configurationService">Service de gestion de la configuration de l'application.</param>
        public ExecutionContext(IEventService eventService, IRepositoryService repositoryService, IApplicationConfigurationService configurationService)
        {
            this.eventService = eventService;
            this.repositoryService = repositoryService;
            this.configurationService = configurationService;
        }
    }
}
