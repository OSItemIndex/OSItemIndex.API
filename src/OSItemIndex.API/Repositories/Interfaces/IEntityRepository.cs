using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OSItemIndex.Data;

namespace OSItemIndex.API.Repositories
{
    public interface IEntityRepository<T> where T : ItemEntity
    {
        Task<int> CountAsync();
        Task<T> GetAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, T>> select);
        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Expression<Func<T, T>> select = null);

        Task BulkCopyAsync(IEnumerable<T> entities);
    }
}
