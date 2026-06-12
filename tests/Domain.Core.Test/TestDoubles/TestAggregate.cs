using Domain.Core;

namespace Domain.Core.Test.TestDoubles;

public sealed class TestAggregate : Aggregate<TestEntityId>
{
    public TestAggregate(TestEntityId id) => Id = id;

    public override TestEntityId Id { get; }

    public void Raise(IEvent @event) => AddEvent(@event);

    public void BumpVersion() => IncrementVersion();
}
