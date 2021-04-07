/***
  * @author     Lampjaw
  * @date       6-9-2019
  * @github     https://github.com/voidwell/Voidwell.DaybreakGames/blob/master/src/Voidwell.DaybreakGames.Data/DesignTimeDbContextFactory.cs
*/

/*** Original author: https://github.com/Lampjaw
 * Modified by https://github.com/Twinki14 for OSItemIndex
 * DesignTimeDbContextFactory: Implementation that design-time services will use to create new instances of our db context
 *      when it requires it, such as for migrations, other libraries, or a context factory
 */

using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace OSItemIndex.API.Data
{
    /// <summary>
    ///     IDesignTimeDbContextFactory implemention that's used by design-time services.
    ///     https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.design.idesigntimedbcontextfactory-1?view=efcore-5.0
    /// </summary>
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<OSItemIndexDbContext>
    {
        /// <summary>
        ///     Creates a new instance of a OSItemIndexDbContext.
        /// </summary>
        /// <returns>A new instance of OSItemIndexDbContext.</returns>
        public OSItemIndexDbContext CreateDbContext(string[] args)
        {
            var confBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true);

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                confBuilder.AddJsonFile("appsettings.dev.json", true, true);
            }

            var dbOptions = new DatabaseOptions();
            var configuration = confBuilder.Build();

            configuration.Bind(dbOptions);

            var builder = new DbContextOptionsBuilder<OSItemIndexDbContext>();

            builder.UseNpgsql(dbOptions.DbConnectionString, o =>
            {
                o.CommandTimeout(7200);
            });

            return new OSItemIndexDbContext(builder.Options);
        }
    }
}
