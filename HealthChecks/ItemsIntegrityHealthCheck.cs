using Microsoft.Extensions.Diagnostics.HealthChecks;
using OSItemIndex.API.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;

namespace OSItemIndex.API.HealthChecks
{
    public class ItemsIntegrityHealthCheck : IHealthCheck, IItemsIntegrityHealthCheck
    {
        private readonly IItemsService _service;

        public ItemsIntegrityHealthCheck(IItemsService itemsService)
        {
            _service = itemsService;
        }

        public async Task<HealthCheckResult> CreateReportAsync()
        {
            try
            {
                using(var itemsInTable = _service.CountItemsAsync())
                using(var itemsWithNamesInTable = _service.CountItemsWithNamesAsync())
                {
                    var data = new ItemsIntegrityHealthCheckData
                    {
                        TotalItemsInDb = await itemsInTable,
                        TotalItemsWithNamesInDb = await itemsWithNamesInTable
                    };

                    string description = null;
                    var json = JsonSerializer.Serialize(data);
                    var dict = JsonSerializer.Deserialize<Dictionary<string, object>>(json);

                    if (data.TotalItemsInDb <= 20000 || data.TotalItemsWithNamesInDb <= 20000)
                        description = "Items table not as populated as expected (20000)";

                    if (data.TotalItemsInDb != data.TotalItemsWithNamesInDb)
                        description = "Items table has item property values missing";

                    if (description != null)
                        return HealthCheckResult.Degraded(description, data: dict);

                    return HealthCheckResult.Healthy("Items table healthy", dict);
                }
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Degraded("Check caught exception!", ex);
            }
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return CreateReportAsync();
        }
    }
}
