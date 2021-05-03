using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OsItemIndex.Data;

namespace OSItemIndex.API.Repositories
{
    public abstract class EntityRepository<T> : IEntityRepository<T> where T : ItemEntity
    {
        public EntityRepository()
        {

        }

        public async Task<T> GetAsync(int id)
        {
            /*using (var factory = _dbContextHelper.GetFactory())
            {
                var dbContext = factory.GetDbContext();

                return await dbContext.Set<T>().FindAsync(id).AsTask();
            }*/
            return null;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            /*using (var factory = _dbContextHelper.GetFactory())
            {
                var dbContext = factory.GetDbContext();

                return await dbContext.Set<T>().ToListAsync();
            }*/
            return null;
        }

        public async Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            /*using (var factory = _dbContextHelper.GetFactory())
            {
                var dbContext = factory.GetDbContext();

                IQueryable<T> query = dbContext.Set<T>();

                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                if (includeProperties != null)
                {
                    foreach (var includeProperty in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProperty);
                    }
                }

                return orderBy != null ? await orderBy(query).ToListAsync() : await query.ToListAsync();
            }*/
            return null;
        }
    }
}
