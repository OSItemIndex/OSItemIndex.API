using Microsoft.EntityFrameworkCore;
using OSItemIndex.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OSItemIndex.API.Repositories
{
    public class RealtimePriceRepository : IPricesRepository<RealtimePrice>
    {
        private readonly OSItemIndexDbContext _context;

        public RealtimePriceRepository(OSItemIndexDbContext context)
        {
            _context = context;
        }

        public Task<RealtimePrice> GetAsync(object id)
        {
            return _context.PricesRealtime.FindAsync(id).AsTask();
        }

        public async Task<IEnumerable<RealtimePrice>> GetAllAsync()
        {
            return await _context.PricesRealtime.ToListAsync();
        }

        public async Task<IEnumerable<RealtimePrice>> GetAllAsync(Expression<Func<RealtimePrice, bool>> filter = null, Func<IQueryable<RealtimePrice>, IOrderedQueryable<RealtimePrice>> orderBy = null, string includeProperties = "")
        {
            IQueryable<RealtimePrice> query = _context.PricesRealtime;

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

        public Task<RealtimePrice> UpsertAsync(RealtimePrice item)
        {
            return _context.PricesRealtime.UpsertAsync(item, matchPredicate: m => m.Id == item.Id);
        }

        public Task<IEnumerable<RealtimePrice>> UpsertAllAsync(IEnumerable<RealtimePrice> items)
        {
            return _context.PricesRealtime.UpsertRangeAsync(items, matchPredicate: (a, b) => a.Id == b.Id);
        }

        public Task<int> CommitAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
