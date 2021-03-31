using Microsoft.Extensions.Diagnostics.HealthChecks;
using OSItemIndex.API.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;

namespace OSItemIndex.API.HealthChecks
{
    public class ItemsIntegrityCheck : IHealthCheck
    {
        private readonly IItemsService _service;

        public ItemsIntegrityCheck(IItemsService itemsService)
        {
            _service = itemsService;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var itemsInTable = await _service.CountItemsAsync();
                var data = new ItemsIntegrityHealthCheckData
                {
                    TotalItemsInDb = itemsInTable,
                };

                var json = JsonSerializer.Serialize(data);
                var dict = JsonSerializer.Deserialize<Dictionary<string, object>>(json);

                return data.TotalItemsInDb >= 20000
                    ? HealthCheckResult.Healthy("Items table healthy", dict)
                    : HealthCheckResult.Degraded("Items table not as populated as expected (20000)", data: dict);
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Degraded("Check caught exception!", ex);
            }
        }
    }
}
