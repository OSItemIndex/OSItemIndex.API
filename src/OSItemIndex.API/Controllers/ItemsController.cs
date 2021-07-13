using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OSItemIndex.API.Models;
using OSItemIndex.API.Services;
using OSItemIndex.Data;
using Serilog;

// curl https://localhost:5001/items -H "Accept-Encoding: gzip" -o nul

namespace OSItemIndex.API.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsService _itemsService;

        public ItemsController(IItemsService itemsService)
        {
            _itemsService = itemsService;
        }

        [HttpGet(Name = "GetItems")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<OsrsBoxItem>>> GetItems([FromQuery] ItemQuery query)
        {
            return Ok(await _itemsService.GetItemsAsync(Request.QueryString.HasValue ? query : null));
        }

        [HttpGet("simple", Name = "GetItemsSimple")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<OsrsBoxItem>>> GetItemsSimple([FromQuery] ItemQuery query)
        {
            var result = await _itemsService.GetItemsAsync(Request.QueryString.HasValue ? query : null,
                                                           item => new OsrsBoxItem
                                                           {
                                                               Id = item.Id,
                                                               Name = item.Name,
                                                               Duplicate = item.Duplicate,
                                                               Noted = item.Noted,
                                                               Stackable = item.Stackable,
                                                               Placeholder = item.Placeholder,
                                                               TradeableOnGe = item.TradeableOnGe,
                                                               LastUpdated = item.LastUpdated
                                                           });
            return Ok(result);
        }

        [HttpGet("{id:int}", Name = "GetItemById")]
        [Produces("application/json")]
        public async Task<ActionResult<OsrsBoxItem>> GetItemById(
            [Range(0, int.MaxValue, ErrorMessage = "ID cannot be negative")]
            int id)
        {
            return Ok(await _itemsService.GetItemAsync(id));
        }

        /*[HttpGet("info", Name = "GetItemsInfo")]
        [Produces("application/json")]
        public async Task<ActionResult<OsrsBoxItem>> GetVersion()
        {
            return Ok(await _itemsService.GetVersionAsync());
        }

        [HttpPost(Name = "PostItems")]
        [RequestSizeLimit(1_000_000_000)]
        public async Task<ActionResult<OsrsBoxItem>> UpdateItems([FromBody] IDictionary<string, OsrsBoxItem> items, [FromQuery] string version)
        {
            await _itemsService.UpdateItemsAsync(items.Values, new EntityRepoVersion { Version = version });
            return Ok(version);
        }

        [HttpPost("dict", Name = "PostItemsDict")]
        [RequestSizeLimit(1_000_000_000)]
        public async Task<ActionResult<OsrsBoxItem>> UpdateItems(IDictionary<string, OsrsBoxItem> items)
        {
            await _itemsService.UpdateItemsAsync(items.Values, null);
            return Ok(items.Values.Count);
        }*/
    }
}
