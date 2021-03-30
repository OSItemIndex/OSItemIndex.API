﻿/**
  * @author     Lampjaw
  * @date       6-30-2019
  * @github     https://github.com/voidwell/Voidwell.DaybreakGames/blob/master/src/Voidwell.DaybreakGames.Data/DbContextHelper.cs
*/

/** Original author: https://github.com/Lampjaw
 * Modified by https://github.com/Twinki14 for OSItemIndex
 * DbContextHelper: Helper class we can use with DI to inject a factory (DbContextFactory) of our dbContext into our modules/services
 * DbContextFactory: Contains a scopeFactory instance and creates a new DbContext instance from the scopeFactory
 * General notes:
 *      Using a factory pattern instead of sharing a DbContext between our services injected by DI gives us the benefit of concurrency/thread-safety,
 *          better performance, and memory management assuming we use it properly. We inject DbContextHelper into each service that requires access to our DbContext
 *          with DI, use GetFactory to access the factory and create a new DbContext instance using the scope factory, 
 *          and GetDbContext to access that context. When we no longer need to use our DbContext, we dispose of our DbContextFactory instance created 
 *          by the injected DbContextHelper instance, which also disposes the underlying DbContext.
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace OSItemIndex.API
{
    public class DbContextHelper : IDbContextHelper
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DbContextHelper(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        /// <summary>
        ///     Creates a new instance of DbContextFactory and with it creates a new DbContext instance.
        /// </summary>
        public DbContextFactory GetFactory()
        {
            return new DbContextFactory(_scopeFactory);
        }

        public class DbContextFactory : IDisposable
        {
            private readonly IServiceScope _scope;
            private readonly OSItemIndexDbContext _dbContext;

            public DbContextFactory(IServiceScopeFactory scopeFactory)
            {
                _scope = scopeFactory.CreateScope();
                _dbContext = _scope.ServiceProvider.GetRequiredService<OSItemIndexDbContext>();

                // Disable tracking on our underlying entities in DbContext
                // https://docs.microsoft.com/en-us/ef/core/querying/tracking
                _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }

            public OSItemIndexDbContext GetDbContext()
            {
                return _dbContext;
            }

            /// <summary>
            ///     Disposes our dbContext and scopeFactory
            /// </summary>
            public void Dispose()
            {
                _dbContext.Dispose();
                _scope.Dispose();
            }
        }
    }
}
