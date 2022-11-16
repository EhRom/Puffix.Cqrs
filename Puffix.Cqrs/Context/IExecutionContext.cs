using Puffix.Cqrs.Configurations;
using Puffix.Cqrs.Events;
using Puffix.Cqrs.Repositories;

namespace Puffix.Cqrs.Context
{
    /// <summary>
    /// Execution context contract.
    /// </summary>
    public interface IExecutionContext
    {
        /// <summary>
        /// Event management service.
        /// </summary>
        IEventService EventService { get; }

        /// <summary>
        /// Repository management service.
        /// </summary>
        IRepositoryService RepositoryService { get; }

        /// <summary>
        /// Application configuration management service.
        /// </summary>
        IApplicationConfigurationService ConfigurationService { get; }
    }
}
