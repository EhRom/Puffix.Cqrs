using Puffix.Cqrs.Configurations;
using Puffix.Cqrs.Events;
using Puffix.Cqrs.Repositories;

namespace Puffix.Cqrs.Context;

/// <summary>
/// Execution context.
/// </summary>
public class ExecutionContext : IExecutionContext
{
    private readonly IEventService eventService;
    private readonly IRepositoryService repositoryService;
    private readonly IApplicationConfigurationService configurationService;

    /// <summary>
    /// Event management service.
    /// </summary>
    public IEventService EventService => eventService;

    /// <summary>
    /// Repository management service.
    /// </summary>
    public IRepositoryService RepositoryService => repositoryService;

    /// <summary>
    /// Application configuration management service.
    /// </summary>
    public IApplicationConfigurationService ConfigurationService => configurationService;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="eventService">Event management service.</param>
    /// <param name="repositoryService">Repository management service.</param>
    /// <param name="configurationService">Application configuration management service.</param>
    public ExecutionContext(IEventService eventService, IRepositoryService repositoryService, IApplicationConfigurationService configurationService)
    {
        this.eventService = eventService;
        this.repositoryService = repositoryService;
        this.configurationService = configurationService;
    }
}
