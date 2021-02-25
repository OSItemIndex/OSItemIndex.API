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
        private readonly OSItemIndexContext _context;

        public ItemsController(OSItemIndexContext context)
        {
            _context = context;
        }

        // GET: api/items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            return await _context.Items.ToListAsync();
        }

        // GET: api/items/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            var i = new Item();
            return i;
        }

        // POST: api/items
        [HttpPost]
        public async Task<IActionResult> PostItem()
        {
            
            return Ok();
            //return await _context.Items.FindAsync(id);
        }
    }
}
