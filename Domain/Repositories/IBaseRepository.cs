using System.Collections.Generic;
using System.Linq.Expressions;

namespace Domain.Repositories
{
    public interface IBaseRepository <TEntity, TId> where TEntity : class
    {
        
        TEntity Get(TId id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void Delete(TEntity entity);

    }
}
