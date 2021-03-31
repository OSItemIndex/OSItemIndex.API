using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OSItemIndex.API.Models;

namespace OSItemIndex.API.Repositories
{
    public interface IEntityRepository<T> where T : ItemEntity
    {
        Task<T> GetAsync(object id);

        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");

        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);

        Task<T> UpsertAsync(T entity);
        Task<IEnumerable<T>> UpsertRangeAsync(IEnumerable<T> entities);

        Task<int> CommitAsync();
    }
}
