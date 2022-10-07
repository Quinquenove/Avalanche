using System.Linq.Expressions;

namespace Avalanche.Repositories
{
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
