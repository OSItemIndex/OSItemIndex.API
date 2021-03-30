using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSItemIndex.API.HealthChecks
{
    public class ItemsIntegrityHealthCheckData
    {
        public int TotalItemsInDb { get; set; }
        public int TotalItemsWithNamesInDb { get; set; }
    }
}
