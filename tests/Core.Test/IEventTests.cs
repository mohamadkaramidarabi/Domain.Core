using Core;
using Core.Test.TestDoubles;
using Shouldly;

namespace Core.Test;

public class IEventTests
{
    [Fact]
    public void Id_is_not_empty()
    {
        IEvent @event = new TestEvent();

        @event.Id.ShouldNotBe(Guid.Empty);
    }

    [Fact]
    public void OccurredOn_is_within_a_few_seconds_of_now()
    {
        var before = DateTime.Now;
        IEvent @event = new TestEvent();
        var after = DateTime.Now;

        @event.OccurredOn.ShouldBeInRange(before.AddSeconds(-1), after.AddSeconds(1));
    }

    [Fact]
    public void EventType_contains_type_name()
    {
        IEvent @event = new NamedTestEvent();

        @event.EventType.ShouldContain(nameof(NamedTestEvent));
    }

    [Fact]
    public void Each_Id_access_can_produce_a_new_value()
    {
        IEvent @event = new TestEvent();

        var firstId = @event.Id;
        var secondId = @event.Id;

        firstId.ShouldNotBe(Guid.Empty);
        secondId.ShouldNotBe(Guid.Empty);
        (firstId != secondId).ShouldBeTrue();
    }
}
