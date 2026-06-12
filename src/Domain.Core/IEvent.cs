
namespace Domain.Core
{

    public interface IEvent
    {
        Guid Id => Guid.NewGuid();
        public DateTime OccurredOn => DateTime.Now;
        public string EventType => GetType().AssemblyQualifiedName ?? GetType().Name;
    }
}
