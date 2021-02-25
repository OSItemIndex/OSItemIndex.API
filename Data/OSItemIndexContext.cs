using Microsoft.EntityFrameworkCore;
using OSItemIndex.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSItemIndex.API
{
    public class OSItemIndexContext : DbContext
    {
        public DbSet<Item> Items { get; set; }

        public OSItemIndexContext(DbContextOptions<OSItemIndexContext> options) : base(options)
        {

        }
    }
}
