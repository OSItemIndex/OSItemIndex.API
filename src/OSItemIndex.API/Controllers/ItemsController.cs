using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OSItemIndex.API.Models;
using OSItemIndex.API.Services;
using OSItemIndex.Data;

// curl https://localhost:5001/items -H "Accept-Encoding: gzip" -o nul

namespace OSItemIndex.API.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : Controller
    {
        private readonly IItemsService _itemsService;

        public ItemsController(IItemsService itemsService)
        {
            _itemsService = itemsService;
        }

        [HttpGet(Name = "GetItems")]
        [Produces("application/json")]
        [ResponseCache(VaryByHeader = "Accept-Encoding", Duration = 600)]
        public async Task<ActionResult<IEnumerable<OsrsBoxItem>>> GetItems([FromQuery] ItemQuery query)
        {
            return Json(await _itemsService.GetItemsAsync(Request.QueryString.HasValue ? query : null));
        }

        [HttpGet("simple", Name = "GetItemsSimple")]
        [Produces("application/json")]
        [ResponseCache(VaryByHeader = "Accept-Encoding", Duration = 600)]
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
            return Json(result, new JsonSerializerOptions { IgnoreNullValues = true });
        }

        [HttpGet("{id:int}", Name = "GetItemById")]
        [Produces("application/json")]
        public async Task<ActionResult<OsrsBoxItem>> GetItemById(
            [Range(0, int.MaxValue, ErrorMessage = "ID cannot be negative")]
            int id)
        {
            return Json(await _itemsService.GetItemAsync(id));
        }
    }
}
