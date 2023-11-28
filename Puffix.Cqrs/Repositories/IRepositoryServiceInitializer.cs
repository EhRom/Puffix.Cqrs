using Puffix.Cqrs.Models;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Puffix.Cqrs.Repositories;

/// <summary>
/// Contrat pour le service d'initialisation du service de gestion des sources de données.
/// </summary>
public interface IRepositoryServiceInitializer
{
    /// <summary>
    /// Dictionnaire des informations sur les agrégats enregistrés.
    /// </summary>
    IDictionary<Type, AggregateInfo> AggregateInfoContainer { get; }

    /// <summary>
    /// Enregistrements des répertoires de données.
    /// </summary>
    /// <param name="assemblies">Liste des librairies à scanner.</param>
    void RegisterRepositories(params Assembly[] assemblies);
}
