using Avalanche.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Avalanche.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbSet<T> _entities;

        public Repository(DbContext Context)
        {
            _entities = Context.Set<T>();
        }
        public void Add(T entity)
        {
            _entities.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _entities.AddRange(entities);
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _entities.Where(predicate);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _entities.ToList();
        }

        public virtual T GetById(long id)
        {
            return _entities.Find(id);
        }

        public void Remove(T entity)
        {
            _entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _entities.RemoveRange(entities);
        }
    }
}
