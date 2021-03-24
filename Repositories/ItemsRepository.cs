using Microsoft.EntityFrameworkCore;
using OSItemIndex.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OSItemIndex.API.Repositories
{
    public class ItemsRepository : IItemsRepository
    {
        private OSItemIndexDbContext dbContext;

        public ItemsRepository(OSItemIndexDbContext context)
        {
            dbContext = context;
        }

        public async Task<OSRSBoxItem> GetItemAsync(object id)
        {
            return await dbContext.Items.FindAsync(id);
        }

        public async Task<IEnumerable<OSRSBoxItem>> GetItemsAsync()
        {
            return await dbContext.Items.ToListAsync();
        }

        public async Task<IEnumerable<OSRSBoxItem>> GetItemsAsync(
            Expression<Func<OSRSBoxItem, bool>> filter = null, 
            Func<IQueryable<OSRSBoxItem>, IOrderedQueryable<OSRSBoxItem>> orderBy = null, 
            string includeProperties = "")
        {
            IQueryable<OSRSBoxItem> query = dbContext.Items;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }


            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            } else
            {
                return await query.ToListAsync();
            }
        }

        public void Update(OSRSBoxItem item)
        {
            dbContext.Update(item);
        }

        public void Update(IEnumerable<OSRSBoxItem> items)
        {
            dbContext.UpdateRange(items);
        }
    }
}
