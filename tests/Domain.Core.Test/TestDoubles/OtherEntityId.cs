using Domain.Core;

namespace Domain.Core.Test.TestDoubles;

public sealed record OtherEntityId(Guid Value) : Id(Value);
