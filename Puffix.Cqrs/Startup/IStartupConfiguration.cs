using Puffix.Cqrs.Commands;
using Puffix.Cqrs.Context;
using Puffix.Cqrs.Queries;

namespace Puffix.Cqrs.Startup;

/// <summary>
/// Contrat pour une configuration de démarrage d'une application.
/// </summary>
public interface IStartupConfiguration
{
    /// <summary>
    /// Contexte de l'application.
    /// </summary>
    IApplicationContext ApplicationContext { get; }

    /// <summary>
    /// Contexte d'exécution.
    /// </summary>
    IExecutionContext ExecutionContext { get; }

    /// <summary>
    /// Service de gestion des commandes.
    /// </summary>
    ICommandService CommandService { get; }

    /// <summary>
    /// Service de gestion des commandes.
    /// </summary>
    IQueryService QueryService { get; }
}
