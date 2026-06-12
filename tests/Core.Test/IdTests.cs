using Core.Test.TestDoubles;
using Shouldly;

namespace Core.Test;

public class IdTests
{
    [Fact]
    public void Same_Guid_values_are_equal()
    {
        var guid = Guid.NewGuid();
        var id1 = new TestEntityId(guid);
        var id2 = new TestEntityId(guid);

        id1.ShouldBe(id2);
    }

    [Fact]
    public void Different_Guid_values_are_not_equal()
    {
        var id1 = new TestEntityId(Guid.NewGuid());
        var id2 = new TestEntityId(Guid.NewGuid());

        id1.ShouldNotBe(id2);
    }

    [Fact]
    public void Value_returns_constructor_argument()
    {
        var guid = Guid.NewGuid();
        var id = new TestEntityId(guid);

        id.Value.ShouldBe(guid);
    }

    [Fact]
    public void Distinct_derived_ID_types_with_same_Guid_are_not_equal()
    {
        var guid = Guid.NewGuid();
        var testId = new TestEntityId(guid);
        var otherId = new OtherEntityId(guid);

        testId.Equals(otherId).ShouldBeFalse();
        ((object)testId).Equals(otherId).ShouldBeFalse();
    }
}
