using System.Linq.Expressions;

namespace Avalanche.Repositories
{
    /// <summary>
    /// Definition des Interface IRepository.
    /// Welches die grundlegenden Funktionen für die Repository Klassen definiert.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T :class
    {
        T GetById(long id);

        IEnumerable<T> GetAll();

        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        void Add(T entity);

        void AddRange(IEnumerable<T> entities);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);
    }
}
