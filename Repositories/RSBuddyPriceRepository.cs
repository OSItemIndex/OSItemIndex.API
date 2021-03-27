using OSItemIndex.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OSItemIndex.API.Repositories
{
    public class RSBuddyPriceRepository : IPricesRepository<RSBuddyPrice>
    {
        private OSItemIndexDbContext _context;

        public RSBuddyPriceRepository(OSItemIndexDbContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<RSBuddyPrice>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RSBuddyPrice>> GetAllAsync(Expression<Func<RSBuddyPrice, bool>> filter = null, Func<IQueryable<RSBuddyPrice>, IOrderedQueryable<RSBuddyPrice>> orderBy = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public Task<RSBuddyPrice> GetAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RSBuddyPrice>> UpsertAllAsync(IEnumerable<RSBuddyPrice> items)
        {
            throw new NotImplementedException();
        }

        public Task<RSBuddyPrice> UpsertAsync(RSBuddyPrice item)
        {
            throw new NotImplementedException();
        }        
        
        public Task<int> CommitAsync()
        {
            throw new NotImplementedException();
        }
    }
}
