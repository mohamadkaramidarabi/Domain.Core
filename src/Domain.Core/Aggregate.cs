
namespace Domain.Core
{
    public abstract class Aggregate<TId> : IAggregate<TId>
        where TId : notnull, Id
    {
        private readonly List<IEvent> _events = [];
        private bool _versionIncremented;

        public int Version { get; protected set; }
        public IReadOnlyList<IEvent> Events => _events;

        public abstract TId Id { get; }

        public IEvent[] ClearEvents()
        {
            var dequeuedEvents = _events.ToArray();

            _events.Clear();

            return dequeuedEvents;
        }

        protected void AddEvent(IEvent @event)
        {
            if (_events.Count == 0 && !_versionIncremented)
            {
                Version++;
                _versionIncremented = true;
            }

            _events.Add(@event);
        }

        protected void IncrementVersion()
        {
            if (_versionIncremented) return;

            Version++;
            _versionIncremented = true;

        }
    }
}
