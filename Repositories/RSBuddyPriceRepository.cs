using OSItemIndex.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSItemIndex.API.Repositories
{
    public class RSBuddyPriceRepository : IPricesRepository<RSBuddyPrice>
    {
        private OSItemIndexDbContext dbContext;

        public RSBuddyPriceRepository(OSItemIndexDbContext context)
        {
            dbContext = context;
        }

        public Task<RSBuddyPrice> GetPrice(object id)
        {
            throw new NotImplementedException();
        }

        public Task Update(RSBuddyPrice item)
        {
            throw new NotImplementedException();
        }

        public Task Update(IEnumerable<RSBuddyPrice> items)
        {
            throw new NotImplementedException();
        }
    }
}
