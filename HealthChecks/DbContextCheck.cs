using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OSItemIndex.API.HealthChecks
{
    public class DbContextCheck : IHealthCheck
    {
        public IServiceScopeFactory ScopeFactory { get; }

        public DbContextCheck(IServiceScopeFactory scopeFactory)
        {
            ScopeFactory = scopeFactory;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            using (var scope = ScopeFactory.CreateScope())
            {
                try
                {
                    var db = scope.ServiceProvider.GetRequiredService<OSItemIndexDbContext>();
                    await db.Items.FirstOrDefaultAsync(cancellationToken);
                    return HealthCheckResult.Healthy();
                }
                catch (Exception)
                {
                    return HealthCheckResult.Unhealthy();
                }
            }
        }
    }
}
