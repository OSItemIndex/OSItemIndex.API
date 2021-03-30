using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading.Tasks;

namespace OSItemIndex.API.HealthChecks
{
    public interface IItemsIntegrityHealthCheck
    {
        Task<HealthCheckResult> CreateReportAsync();
    }
}
