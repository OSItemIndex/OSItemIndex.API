using Microsoft.EntityFrameworkCore;
using OSItemIndex.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OSItemIndex.API.Repositories
{
    public class WikiRealtimePriceRepository : IPricesRepository<WikiRealtimePrice>
    {
        private readonly OSItemIndexDbContext _context;

        public WikiRealtimePriceRepository(OSItemIndexDbContext context)
        {
            _context = context;
        }

        public Task<WikiRealtimePrice> GetAsync(object id)
        {
            return _context.WikiRealtimePrices.FindAsync(id).AsTask();
        }

        public async Task<IEnumerable<WikiRealtimePrice>> GetAllAsync()
        {
            return await _context.WikiRealtimePrices.ToListAsync();
        }

        public async Task<IEnumerable<WikiRealtimePrice>> GetAllAsync(Expression<Func<WikiRealtimePrice, bool>> filter = null, Func<IQueryable<WikiRealtimePrice>, IOrderedQueryable<WikiRealtimePrice>> orderBy = null, string includeProperties = "")
        {
            IQueryable<WikiRealtimePrice> query = _context.WikiRealtimePrices;

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

        public Task<WikiRealtimePrice> UpsertAsync(WikiRealtimePrice item)
        {
            return _context.WikiRealtimePrices.UpsertAsync(item, matchPredicate: m => m.Id == item.Id);
        }

        public Task<IEnumerable<WikiRealtimePrice>> UpsertAllAsync(IEnumerable<WikiRealtimePrice> items)
        {
            return _context.WikiRealtimePrices.UpsertRangeAsync(items, matchPredicate: (a, b) => a.Id == b.Id);
        }

        public Task<int> CommitAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
