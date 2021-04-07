using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Threading;
using System.Threading.Tasks;
using OSItemIndex.API.Data;

namespace OSItemIndex.API.HealthChecks
{
    public class OSItemIndexDbCheck : IHealthCheck
    {
        private readonly IDbContextHelper _dbContextHelper;

        public OSItemIndexDbCheck(IDbContextHelper dbContextHelper)
        {
            _dbContextHelper = dbContextHelper;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            using (var factory = _dbContextHelper.GetFactory())
            {
                try
                {
                    var dbContext = factory.GetDbContext();

                    return await dbContext.Database.CanConnectAsync(cancellationToken)
                        ? HealthCheckResult.Healthy()
                        : HealthCheckResult.Unhealthy();
                }
                catch (Exception e)
                {
                    return HealthCheckResult.Unhealthy(exception: e);
                }
            }
        }
    }
}
