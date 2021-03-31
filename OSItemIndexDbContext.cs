using Microsoft.EntityFrameworkCore;
using OSItemIndex.API.Models;

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
