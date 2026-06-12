using Domain.Core;
using Domain.Core.Test.TestDoubles;
using Shouldly;

namespace Domain.Core.Test;

public class ContractTests
{
    [Fact]
    public void TestAggregate_implements_IAggregate()
    {
        var id = new TestEntityId(Guid.NewGuid());
        IAggregate<TestEntityId> aggregate = new TestAggregate(id);

        aggregate.ShouldNotBeNull();
        aggregate.Id.ShouldBe(id);
    }

    [Fact]
    public void TestAggregate_implements_IEntity()
    {
        var id = new TestEntityId(Guid.NewGuid());
        IEntity<TestEntityId> entity = new TestAggregate(id);

        entity.ShouldNotBeNull();
        entity.Id.ShouldBe(id);
    }

    [Fact]
    public void Id_property_returns_constructor_value()
    {
        var id = new TestEntityId(Guid.NewGuid());
        var aggregate = new TestAggregate(id);

        aggregate.Id.ShouldBe(id);
    }
}
