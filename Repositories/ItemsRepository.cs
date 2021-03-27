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
        private readonly OSItemIndexDbContext _context;

        public ItemsRepository(OSItemIndexDbContext context)
        {
            _context = context;
        }

        public Task<OSRSBoxItem> GetAsync(object id)
        {
            return _context.Items.FindAsync(id).AsTask();
        }

        public async Task<IEnumerable<OSRSBoxItem>> GetAllAsync()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task<IEnumerable<OSRSBoxItem>> GetAllAsync(
            Expression<Func<OSRSBoxItem, bool>> filter = null,
            Func<IQueryable<OSRSBoxItem>, IOrderedQueryable<OSRSBoxItem>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<OSRSBoxItem> query = _context.Items;

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
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public Task<int> CountAsync()
        {
            return _context.Items.CountAsync();
        }

        public Task<OSRSBoxItem> UpsertAsync(OSRSBoxItem item)
        {
            return _context.Items.UpsertAsync(item, matchPredicate: m => m.Id == item.Id);
        }

        public Task<IEnumerable<OSRSBoxItem>> UpsertAllAsync(IEnumerable<OSRSBoxItem> items)
        {
            return _context.Items.UpsertRangeAsync(items, matchPredicate: (a, b) => a.Id == b.Id);
        }

        public Task<int> CommitAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
