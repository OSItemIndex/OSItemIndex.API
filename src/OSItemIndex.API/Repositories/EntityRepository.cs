using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Npgsql.Bulk;
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

        public async Task<int> CountAsync()
        {
            await using (var factory = _context.GetFactory())
            {
                var dbContext = factory.GetDbContext();
                return await dbContext.Set<T>().CountAsync();
            }
        }

        public async Task<T> GetAsync(int id)
        {
            await using (var factory = _context.GetFactory())
            {
                var dbContext = factory.GetDbContext();
                return await dbContext.Set<T>().FindAsync(id).AsTask();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            await using (var factory = _context.GetFactory())
            {
                var dbContext = factory.GetDbContext();
                return await dbContext.Set<T>().OrderBy(entity => entity.Id).ToListAsync();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, T>> select)
        {
            await using (var factory = _context.GetFactory())
            {
                var dbContext = factory.GetDbContext();
                IQueryable<T> query = dbContext.Set<T>();

                if (select != null)
                {
                    query = query.Select(select);
                }

                return await query.OrderBy(entity => entity.Id).ToListAsync();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Expression<Func<T, T>> select = null)
        {
            await using (var factory = _context.GetFactory())
            {
                var dbContext = factory.GetDbContext();
                IQueryable<T> query = dbContext.Set<T>();

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (select != null)
                {
                    query = query.Select(select);
                }

                return orderBy != null ? await orderBy(query).ToListAsync() : await query.ToListAsync();
            }
        }

        public async Task BulkCopyAsync(IEnumerable<T> entities)
        {
            await using (var factory = _context.GetFactory())
            {
                var dbContext = factory.GetDbContext();
                var uploader = new NpgsqlBulkUploader(dbContext);

                var entityType = dbContext.Model.FindEntityType(typeof(T));
                var primKeyProp = entityType.FindPrimaryKey().Properties.Select(k => k.PropertyInfo).Single();
                var props = entityType.GetProperties().Select(o => o.PropertyInfo).ToArray();

                await uploader.InsertAsync(entities, InsertConflictAction.UpdateProperty<T>(primKeyProp, props));
            }
        }
    }
}
