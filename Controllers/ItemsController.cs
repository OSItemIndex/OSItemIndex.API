using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OSItemIndex.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSItemIndex.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly OSItemIndexDbContext db;

        public ItemsController(OSItemIndexDbContext dbContext)
        {
            db = dbContext;
        }

        // GET: api/items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OSRSBoxItem>>> GetItems()
        {
            db.Items.Where
            return await db.Items.ToListAsync();
        }

        // GET: api/items/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<OSRSBoxItem>> GetItem(int id)
        {
            var i = new OSRSBoxItem();
            return i;
        }

        // POST: api/items
        [HttpPost]
        public async Task<IActionResult> PostItem(OSRSBoxItem[] items)
        {
            try
            {
                db.UpdateRange(items);
                db.SaveChanges();
                return Ok();
            } catch (Exception ex)
            {

            }
            return BadRequest();
        }
    }
}
