using Microsoft.EntityFrameworkCore;
using OSItemIndex.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSItemIndex.API
{
    public class OSItemIndexDbContext : DbContext
    {
        public DbSet<OSRSBoxItem> Items { get; set; }
        public DbSet<RealtimePrice> PricesRealtime { get; set; }

        public OSItemIndexDbContext(DbContextOptions<OSItemIndexDbContext> options)
            : base(options)
        {
            
        }
    }
}
