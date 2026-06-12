using Domain.Core.Test.TestDoubles;
using Shouldly;

namespace Domain.Core.Test;

public class AggregateTests
{
    private static TestAggregate CreateAggregate() =>
        new(new TestEntityId(Guid.NewGuid()));

    [Fact]
    public void New_aggregate_has_version_zero()
    {
        var aggregate = CreateAggregate();

        aggregate.Version.ShouldBe(0);
    }

    [Fact]
    public void New_aggregate_has_empty_events()
    {
        var aggregate = CreateAggregate();

        aggregate.Events.ShouldBeEmpty();
    }

    [Fact]
    public void Raise_adds_event_to_events()
    {
        var aggregate = CreateAggregate();
        var @event = new TestEvent();

        aggregate.Raise(@event);

        aggregate.Events.Count.ShouldBe(1);
        aggregate.Events[0].ShouldBe(@event);
    }

    [Fact]
    public void Multiple_raise_calls_preserve_order()
    {
        var aggregate = CreateAggregate();
        var first = new TestEvent();
        var second = new NamedTestEvent();
        var third = new TestEvent();

        aggregate.Raise(first);
        aggregate.Raise(second);
        aggregate.Raise(third);

        aggregate.Events.ShouldBe([first, second, third]);
    }

    [Fact]
    public void First_raise_increments_version_to_one()
    {
        var aggregate = CreateAggregate();

        aggregate.Raise(new TestEvent());

        aggregate.Version.ShouldBe(1);
    }

    [Fact]
    public void Second_and_third_raise_do_not_increment_version_again()
    {
        var aggregate = CreateAggregate();

        aggregate.Raise(new TestEvent());
        aggregate.Raise(new TestEvent());
        aggregate.Raise(new TestEvent());

        aggregate.Version.ShouldBe(1);
    }

    [Fact]
    public void BumpVersion_increments_version_when_no_events()
    {
        var aggregate = CreateAggregate();

        aggregate.BumpVersion();

        aggregate.Version.ShouldBe(1);
    }

    [Fact]
    public void BumpVersion_called_twice_only_increments_once()
    {
        var aggregate = CreateAggregate();

        aggregate.BumpVersion();
        aggregate.BumpVersion();

        aggregate.Version.ShouldBe(1);
    }

    [Fact]
    public void BumpVersion_then_raise_does_not_increment_again()
    {
        var aggregate = CreateAggregate();

        aggregate.BumpVersion();
        aggregate.Raise(new TestEvent());

        aggregate.Version.ShouldBe(1);
    }

    [Fact]
    public void Raise_then_BumpVersion_does_not_increment_again()
    {
        var aggregate = CreateAggregate();

        aggregate.Raise(new TestEvent());
        aggregate.BumpVersion();

        aggregate.Version.ShouldBe(1);
    }

    [Fact]
    public void ClearEvents_returns_all_queued_events_in_order()
    {
        var aggregate = CreateAggregate();
        var first = new TestEvent();
        var second = new NamedTestEvent();

        aggregate.Raise(first);
        aggregate.Raise(second);

        var cleared = aggregate.ClearEvents();

        cleared.ShouldBe([first, second]);
    }

    [Fact]
    public void ClearEvents_empties_events()
    {
        var aggregate = CreateAggregate();

        aggregate.Raise(new TestEvent());
        aggregate.ClearEvents();

        aggregate.Events.ShouldBeEmpty();
    }

    [Fact]
    public void ClearEvents_on_empty_aggregate_returns_empty_array()
    {
        var aggregate = CreateAggregate();

        var cleared = aggregate.ClearEvents();

        cleared.ShouldBeEmpty();
        aggregate.Events.ShouldBeEmpty();
    }

    [Fact]
    public void After_ClearEvents_new_raise_still_works_and_version_rules_still_apply()
    {
        var aggregate = CreateAggregate();

        aggregate.Raise(new TestEvent());
        aggregate.ClearEvents();

        aggregate.Version.ShouldBe(1);
        aggregate.Events.ShouldBeEmpty();

        var newEvent = new TestEvent();
        aggregate.Raise(newEvent);

        aggregate.Events.Count.ShouldBe(1);
        aggregate.Events[0].ShouldBe(newEvent);
        aggregate.Version.ShouldBe(1);
    }
}
