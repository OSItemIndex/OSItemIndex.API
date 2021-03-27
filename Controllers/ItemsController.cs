using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OSItemIndex.API.Models;
using OSItemIndex.API.Services;

namespace OSItemIndex.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsService _service;
        internal static DateTime lastUpsert; 

        public ItemsController(IItemsService itemsService, OSItemIndexDbContext context)
        {
            _service = itemsService;
        }

        [HttpGet] // GET: api/items
        public async Task<ActionResult<IEnumerable<OSRSBoxItem>>> GetItems()
        {
            return Ok(await _service.GetItemsAsync());
        }

        [HttpGet] // GET: api/items/{id}
        [Route("{id:int}")]
        public async Task<ActionResult<OSRSBoxItem>> GetItem(int id)
        {
            return Ok(await _service.GetItemAsync(id));
        }

        [HttpPost] // POST: api/items
        [RequestSizeLimit(int.MaxValue)]
        public async Task<IActionResult> PostItem(IEnumerable<OSRSBoxItem> items)
        {
            lastUpsert = DateTime.Now;
            return Ok(await _service.UpsertAndCommitItemsAsync(items));
        }

/*        [HttpGet] // GET: api/items/stats
        [Route("stats")]
        public async Task<IActionResult> GetStatistics()
        {
            return Ok(lastUpsert.ToString());
        }*/
    }
}
