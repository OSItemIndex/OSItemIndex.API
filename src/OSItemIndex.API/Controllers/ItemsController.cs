using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OSItemIndex.API.Services;

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

        /*[HttpGet] // GET: items
        public async Task<ActionResult<IEnumerable<OSRSBoxItem>>> GetItems()
        {
            return Ok(await _itemsService.GetItemsAsync());
        }

        [HttpGet] // GET: items/{id}
        [Route("{id:int}")]
        public async Task<ActionResult<OSRSBoxItem>> GetItem(int id)
        {
            return Ok(await _itemsService.GetItemAsync(id));
        }*/
    }
}
