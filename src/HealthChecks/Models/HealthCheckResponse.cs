using System;
using System.Collections.Generic;

namespace OSItemIndex.API.HealthChecks
{
    public class HealthCheckResponse
    {
        public string Status { get; set; }
        public IEnumerable<HealthCheckReport> Checks { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
