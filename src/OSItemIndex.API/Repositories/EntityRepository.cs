using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OSItemIndex.Data;
using OSItemIndex.Data.Database;

namespace OSItemIndex.API.Repositories
{
    public abstract class EntityRepository<T> : IEntityRepository<T> where T : ItemEntity
    {
        private readonly IDbContextHelper _context;

        public EntityRepository(IDbContextHelper context)
        {
            _context = context;
        }

        public async Task<T> GetAsync(int id)
        {
            using (var factory = _context.GetFactory())
            {
                var dbContext = factory.GetDbContext();
                return await dbContext.Set<T>().FindAsync(id).AsTask();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using (var factory = _context.GetFactory())
            {
                var dbContext = factory.GetDbContext();
                return await dbContext.Set<T>().OrderBy(entity => entity.Id).ToListAsync();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            using (var factory = _context.GetFactory())
            {
                var dbContext = factory.GetDbContext();
                IQueryable<T> query = dbContext.Set<T>();

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                return orderBy != null ? await orderBy(query).ToListAsync() : await query.ToListAsync();
            }
        }
    }
}
