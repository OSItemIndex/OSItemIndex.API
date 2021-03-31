﻿/***
  * @author     Lampjaw
  * @date       6-28-2019
  * @github     https://github.com/voidwell/Voidwell.DaybreakGames/blob/master/src/Voidwell.DaybreakGames.Data/DbSetExtensions.cs
*/

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OSItemIndex.API
{
    public static class DbSetExtensions
    {
        public static async Task<TEntity> UpsertAsync<TEntity>(this DbSet<TEntity> dbSet, TEntity entity, Expression<Func<TEntity, bool>> matchPredicate, bool ignoreNullProperties = false) where TEntity : class
        {
            var storeEntity = await (matchPredicate != null ? dbSet.Where(matchPredicate) : dbSet).AsNoTracking().FirstOrDefaultAsync();

            if (storeEntity == null)
            {
                await dbSet.AddAsync(entity);
            }
            else
            {
                PrepareEntityUpdate(dbSet, storeEntity, entity, ignoreNullProperties);
            }

            return entity;
        }

        public static Task<IEnumerable<TEntity>> UpsertRangeAsync<TEntity>(this DbSet<TEntity> dbSet, IEnumerable<TEntity> entities, Func<TEntity, TEntity, bool> matchPredicate, bool ignoreNullProperties = false) where TEntity : class
        {
            return dbSet.UpsertRangeAsync(entities, null, matchPredicate, ignoreNullProperties);
        }

        public static async Task<IEnumerable<TEntity>> UpsertRangeAsync<TEntity>(this DbSet<TEntity> dbSet, IEnumerable<TEntity> entities, Expression<Func<TEntity, bool>> searchPredicate, Func<TEntity, TEntity, bool> matchPredicate, bool ignoreNullProperties = false) where TEntity : class
        {          
            var result = new List<TEntity>();
            var createdEntities = new List<TEntity>();

            var storedEntities = await (searchPredicate != null ? dbSet.Where(searchPredicate) : dbSet).AsNoTracking().ToListAsync();

            foreach (var entity in entities)
            {
                var storeEntity = storedEntities.FirstOrDefault(storedEntity => matchPredicate(storedEntity, entity));
                if (storeEntity == null)
                {
                    createdEntities.Add(entity);
                }
                else
                {
                    var preparedEntity = PrepareEntityUpdate(dbSet, storeEntity, entity, ignoreNullProperties);
                    result.Add(preparedEntity);
                }
            }

            if (createdEntities.Any())
            {
                await dbSet.AddRangeAsync(createdEntities);
                result.AddRange(createdEntities);
            }

            return result;
        }

        private static T PrepareEntityUpdate<T>(DbSet<T> dbSet, T target, T source, bool ignoreNullProperties) where T : class
        {
            if (ignoreNullProperties)
            {
                AssignNonNullProperties(ref target, source);
            }
            else
            {
                target = source;
            }

            dbSet.Update(target);
            return target;
        }

        // Assigns non-null property values
        private static void AssignNonNullProperties<T>(ref T target, T source) where T : class
        {
            foreach (var fromProp in typeof(T).GetProperties())
            {
                var toProp = typeof(T).GetProperty(fromProp.Name); // retrieve the actual prop
                var toValue = toProp.GetValue(source, null); // retrieve default(?) value from entity prop
                if (toValue != null)
                {
                    fromProp.SetValue(target, toValue, null); // sets prop value to the default(?) prop value
                }
            }
        }
    }
}
