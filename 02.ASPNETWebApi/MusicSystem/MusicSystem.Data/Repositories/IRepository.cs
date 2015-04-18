using System;
using System.Linq;
using System.Linq.Expressions;

namespace MusicSystem.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> All();

        IQueryable<T> Find(Expression<Func<T, bool>> expression);

        T GetById(object id);

        T Add(T entity);

        T Update(T entity);

        void Delete(T entity);

        void Detach(T entity);
    }
}
