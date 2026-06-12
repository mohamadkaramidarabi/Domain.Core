
namespace Domain.Core
{
    public interface IEntity<T>
        where T : notnull, Id
    {
        public T Id { get; }
    }
}
