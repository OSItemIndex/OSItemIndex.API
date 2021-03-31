using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OSItemIndex.API.Data;
using OSItemIndex.API.Extensions;
using OSItemIndex.API.Models;

namespace OSItemIndex.API.Repositories
{
    public abstract class EntityRepository<T> : IEntityRepository<T> where T : ItemEntity
    {
        private readonly IDbContextHelper _dbContextHelper;

        public EntityRepository(IDbContextHelper dbContextHelper)
        {
            _dbContextHelper = dbContextHelper;
        }

        public async Task<T> GetAsync(object id)
        {
            using (var factory = _dbContextHelper.GetFactory())
            {
                var dbContext = factory.GetDbContext();

                return await dbContext.Set<T>().FindAsync(id).AsTask();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using (var factory = _dbContextHelper.GetFactory())
            {
                var dbContext = factory.GetDbContext();

                return await dbContext.Set<T>().ToListAsync();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            using (var factory = _dbContextHelper.GetFactory())
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
            }
        }

        public async Task<int> CountAsync()
        {
            using (var factory = _dbContextHelper.GetFactory())
            {
                var dbContext = factory.GetDbContext();
                return await dbContext.Set<T>().CountAsync();
            }
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            using (var factory = _dbContextHelper.GetFactory())
            {
                var dbContext = factory.GetDbContext();
                return await dbContext.Set<T>().CountAsync(predicate);
            }
        }

        public async Task<T> UpsertAsync(T entity)
        {
            using (var factory = _dbContextHelper.GetFactory())
            {
                var dbContext = factory.GetDbContext();

                return await dbContext.Set<T>().UpsertAsync(entity, m => m.Id == entity.Id);
            }
        }

        public async Task<IEnumerable<T>> UpsertRangeAsync(IEnumerable<T> entities)
        {
            using (var factory = _dbContextHelper.GetFactory())
            {
                var dbContext = factory.GetDbContext();

                return await dbContext.Set<T>().UpsertRangeAsync(entities, (a, b) => a.Id == b.Id);
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
