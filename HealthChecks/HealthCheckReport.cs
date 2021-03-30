using System;
using System.Collections.Generic;

namespace OSItemIndex.API.HealthChecks
{
    public class HealthCheckReport
    {
        public string Status { get; set; }
        public string Component { get; set; }
        public string Description { get; set; }
        public string Exception { get; set; }
        public IReadOnlyDictionary<string, object> Data { get; set; }
    }
}
