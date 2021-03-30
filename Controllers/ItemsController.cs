using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OSItemIndex.API.Models;
using OSItemIndex.API.Services;
using Serilog;

namespace OSItemIndex.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsService _itemsService;

        public ItemsController(IItemsService itemsService)
        {
            _itemsService = itemsService;
        }

        [HttpGet] // GET: items
        public async Task<ActionResult<IEnumerable<OSRSBoxItem>>> GetItems()
        {
            return Ok(await _itemsService.GetItemsAsync());
        }

        [HttpGet] // GET: items/{id}
        [Route("{id:int}")]
        public async Task<ActionResult<OSRSBoxItem>> GetItem(int id)
        {
            return Ok(await _itemsService.GetItemAsync(id));
        }

        [HttpPost] // POST: items
        [RequestSizeLimit(int.MaxValue)]
        public async Task<IActionResult> PostItem(IEnumerable<OSRSBoxItem> items)
        {
            return Ok(await _service.UpsertAndCommitItemsAsync(items));
        }

        [HttpGet] // GET: api/items/stats
        [Route("stats")]
        public async Task<IActionResult> GetStatistics()
        {
            return Ok(await _service.GetStatisicsAsync());
        }
    }
}
