using Core;

namespace Core.Test.TestDoubles;

public sealed record TestEntityId(Guid Value) : Id(Value);
