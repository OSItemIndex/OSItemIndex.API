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
        private readonly IDbContextHelper _dbContextHelper;

        public ItemsRepository(IDbContextHelper dbContextHelper)
        {
            _dbContextHelper = dbContextHelper;
        }

        public async Task<OSRSBoxItem> GetAsync(object id)
        {
            using (var factory = _dbContextHelper.GetFactory())
            {
                var dbContext = factory.GetDbContext();

                return await dbContext.Items.FindAsync(id).AsTask();
            }
        }

        public async Task<IEnumerable<OSRSBoxItem>> GetAllAsync()
        {
            using (var factory = _dbContextHelper.GetFactory())
            {
                var dbContext = factory.GetDbContext();

                return await dbContext.Items.ToListAsync();
            }
        }

        public async Task<IEnumerable<OSRSBoxItem>> GetAllAsync(
            Expression<Func<OSRSBoxItem, bool>> filter,
            Func<IQueryable<OSRSBoxItem>, IOrderedQueryable<OSRSBoxItem>> orderBy = null,
            string includeProperties = "")
        {
            using (var factory = _dbContextHelper.GetFactory())
            {
                var dbContext = factory.GetDbContext();

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
                }
                else
                {
                    return await query.ToListAsync();
                }
            }
        }

        public async Task<int> CountAsync()
        {
            using (var factory = _dbContextHelper.GetFactory())
            {
                var dbContext = factory.GetDbContext();

                return await dbContext.Items.CountAsync();
            }
        }

        public async Task<int> CountAsync(Expression<Func<OSRSBoxItem, bool>> predicate)
        {
            using (var factory = _dbContextHelper.GetFactory())
            {
                var dbContext = factory.GetDbContext();

                return await dbContext.Items.CountAsync(predicate);
            }
        }

        public async Task<OSRSBoxItem> UpsertAsync(OSRSBoxItem item)
        {
            using (var factory = _dbContextHelper.GetFactory())
            {
                var dbContext = factory.GetDbContext();

                return await dbContext.Items.UpsertAsync(item, m => m.Id == item.Id);
            }
        }

        public async Task<IEnumerable<OSRSBoxItem>> UpsertAllAsync(IEnumerable<OSRSBoxItem> items)
        {
            using (var factory = _dbContextHelper.GetFactory())
            {
                var dbContext = factory.GetDbContext();

                return await dbContext.Items.UpsertRangeAsync(items, (a, b) => a.Id == b.Id);
            }
        }

        public async Task<int> CommitAsync()
        {
            using (var factory = _dbContextHelper.GetFactory())
            {
                var dbContext = factory.GetDbContext();

                return await dbContext.SaveChangesAsync();
            }
        }
    }
}
