using OSItemIndex.API.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq.Expressions;
using System.Linq;

namespace OSItemIndex.API.Repositories
{
    public interface IPricesRepository<T> where T : PriceIdentity
    {
        Task<T> GetAsync(object id);

        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");

        Task<T> UpsertAsync(T item);
        Task<IEnumerable<T>> UpsertAllAsync(IEnumerable<T> items);

        Task<int> CommitAsync();
    }
}
