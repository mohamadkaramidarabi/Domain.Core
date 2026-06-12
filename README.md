# Core

Lightweight [Domain-Driven Design (DDD)](https://martinfowler.com/bliki/DomainDrivenDesign.html) building blocks for .NET — typed identifiers, entity contracts, aggregate roots with domain events, and optimistic concurrency versioning.

Targets **.NET 10** with no external dependencies.

## Installation

```bash
dotnet add package Domain.Primitives
```

## What's included

| Type | Description |
|------|-------------|
| `Id` | Abstract record wrapping a `Guid` for strongly typed entity identifiers |
| `IEntity<T>` | Contract for entities with a typed `Id` |
| `IAggregate<T>` | Extends `IEntity<T>` with a domain event queue and `ClearEvents()` |
| `Aggregate<TId>` | Base aggregate with version tracking and protected `AddEvent()` / `IncrementVersion()` |
| `IEvent` | Domain event interface with default `Id`, `OccurredOn`, and `EventType` |

## Usage

### Typed IDs and aggregates

```csharp
using Core;

public sealed record OrderId(Guid Value) : Id(Value);

public sealed class OrderCreated : IEvent;

public sealed class Order : Aggregate<OrderId>
{
    public override OrderId Id { get; }

    public Order(OrderId id)
    {
        Id = id;
        AddEvent(new OrderCreated());
    }

    public void Ship()
    {
        AddEvent(new OrderShipped());
    }
}

public sealed class OrderShipped : IEvent;
```

### Dispatching domain events

After persisting an aggregate, dequeue and handle its pending events (e.g. for an outbox or in-process dispatcher):

```csharp
Order order = /* load or create */;

// ... apply business logic that calls AddEvent ...

IEvent[] pendingEvents = order.ClearEvents();

foreach (IEvent domainEvent in pendingEvents)
{
    // publish or persist for downstream handlers
}
```

### Versioning

`Aggregate<TId>` tracks an `Version` property for optimistic concurrency. The version increments once when the first domain event is raised or when `IncrementVersion()` is called — whichever happens first in a given change cycle.

## Development

Clone the repository and run tests:

```bash
git clone https://github.com/mohamadkaramidarabi/Domain.Core.git
cd Domain.Core
dotnet test tests/Core.Test/Core.Test.csproj
```

Build a NuGet package locally:

```bash
dotnet pack src/Core/Core.csproj -c Release -o ./artifacts
```

## Releasing

Releases are published to [nuget.org](https://www.nuget.org/packages/Domain.Primitives) via GitHub Actions when a version tag is pushed (e.g. `v1.0.0`). The tag version should match the `<Version>` in `Core.csproj`.

## License

MIT
