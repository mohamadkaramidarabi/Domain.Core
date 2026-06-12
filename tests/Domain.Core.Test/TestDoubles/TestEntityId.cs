using Domain.Core;

namespace Domain.Core.Test.TestDoubles;

public sealed record TestEntityId(Guid Value) : Id(Value);
