using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OSItemIndex.API.Models;
using OSItemIndex.API.Services;
using OSItemIndex.Data;

namespace OSItemIndex.API.Controllers
{
    [Route("items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsService _itemsService;

        public ItemsController(IItemsService itemsService)
        {
            _itemsService = itemsService;
        }

        /// <summary>
        ///     Returns a array of all items in the current database, with optional query filtering
        /// </summary>
        [HttpGet] // GET: items
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<OsrsBoxItem>>> GetItems([FromQuery] ItemQuery query)
        {
            return Ok(await _itemsService.GetItemsAsync(Request.QueryString.HasValue ? query : null));
        }

        /// <summary>
        ///     Returns an item matching the specified Item ID
        /// </summary>
        /// <param name="id">Item ID</param>
        [HttpGet] // GET: items/{id}
        [Route("{id:int}")]
        [Produces("application/json")]
        public async Task<ActionResult<OsrsBoxItem>> GetItem(int id)
        {
            var result = await _itemsService.GetItemAsync(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("/version")] // GET: items/version
        public async Task<ActionResult> GetVersion()
        {
            return Ok();
        }
    }
}
