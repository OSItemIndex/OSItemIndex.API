using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using OSItemIndex.API.Models;

namespace OSItemIndex.API.Repositories
{
    public interface IItemsRepository
    {      
        Task<OSRSBoxItem> GetAsync(object id);

        Task<IEnumerable<OSRSBoxItem>> GetAllAsync();
        Task<IEnumerable<OSRSBoxItem>> GetAllAsync(
            Expression<Func<OSRSBoxItem, bool>> filter = null,
            Func<IQueryable<OSRSBoxItem>, IOrderedQueryable<OSRSBoxItem>> orderBy = null,
            string includeProperties = "");

        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<OSRSBoxItem, bool>> predicate);

        Task<OSRSBoxItem> UpsertAsync(OSRSBoxItem item);
        Task<IEnumerable<OSRSBoxItem>> UpsertAllAsync(IEnumerable<OSRSBoxItem> items);

        Task<int> CommitAsync();
    }
}
