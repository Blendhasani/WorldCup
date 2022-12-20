using System.Linq.Expressions;

namespace WorldCup.Data.Base
{
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        Task<IEnumerable<T>> GetAllAsync(int? page);

        Task<T> GetByIdAsync(int id);

        Task AddAsync(T entity);

        Task UpdateAsync(int id, T entity);

        Task DeleteAsync(int id);

        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);


    }
}
