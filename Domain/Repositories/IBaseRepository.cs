using System.Collections.Generic;
using System.Linq.Expressions;

namespace Domain.Repositories
{
    public interface IBaseRepository<TEntity, TId> where TEntity : class
    {

        Task<TEntity> GetAsync(TId id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

    }
}
