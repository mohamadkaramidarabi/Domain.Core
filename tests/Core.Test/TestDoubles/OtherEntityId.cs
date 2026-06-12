using Core;

namespace Core.Test.TestDoubles;

public sealed record OtherEntityId(Guid Value) : Id(Value);
