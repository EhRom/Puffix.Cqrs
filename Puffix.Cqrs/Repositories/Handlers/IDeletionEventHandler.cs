using Puffix.Cqrs.EventHandlers;
using Puffix.Cqrs.Events;
using Puffix.Cqrs.Models;
using System;

namespace Puffix.Cqrs.Repositories.Handlers;

/// <summary>
/// Contrat poue un intercepteur d'évènement générique pour la suppression d'une donnée.
/// </summary>
/// <typeparam name="AggregateT">Type de l'agrégat.</typeparam>
/// <typeparam name="IndexT">Type de l'index.</typeparam>
public interface IDeletionEventHandler<AggregateImplementationT, AggregateT, IndexT> : IEventHandler<IDeletionEvent<AggregateImplementationT, AggregateT, IndexT>, AggregateImplementationT, AggregateT, IndexT>
    where AggregateImplementationT : class, AggregateT
    where AggregateT : IAggregate<IndexT>
    where IndexT : IComparable, IComparable<IndexT>, IEquatable<IndexT>
{ }
