

namespace Domain.Core
{
    public interface IAggregate<T> : IEntity<T>
        where T : notnull, Id
    {
        IReadOnlyList<IEvent> Events { get; }
        IEvent[] ClearEvents();
    }
}
