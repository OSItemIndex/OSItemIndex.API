using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using OSItemIndex.API.Models;

namespace OSItemIndex.API.Repositories
{
    interface IItemsRepository : IRepository<OSRSBoxItem>
    {
        Task<OSRSBoxItem> GetItemAsync(object id);

        Task<IEnumerable<OSRSBoxItem>> GetItemsAsync();
        Task<IEnumerable<OSRSBoxItem>> GetItemsAsync(
            Expression<Func<OSRSBoxItem, bool>> filter = null,
            Func<IQueryable<OSRSBoxItem>, IOrderedQueryable<OSRSBoxItem>> orderBy = null,
            string includeProperties = "");

        void Update(OSRSBoxItem item);
        void Update(IEnumerable<OSRSBoxItem> items);
    }
}
