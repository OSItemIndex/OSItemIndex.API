using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OSItemIndex.API.Models;
using OSItemIndex.API.Services;

namespace OSItemIndex.API.Controllers
{
    [Route("items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsService _itemsService;

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="itemsService"></param>
        public ItemsController(IItemsService itemsService)
        {
            _itemsService = itemsService;
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        [HttpGet] // GET: items
        public async Task<ActionResult> GetItems(ItemQuery? q = null)
        {
            /*var result = !Request.QueryString.HasValue
                ? await _itemsService.GetItemsAsync()
                : await _itemsService.GetItemsAsync(name,
                                                    duplicate,
                                                    noted,
                                                    placeholder,
                                                    stackable,
                                                    tradeableOnGe);*/
            return Ok(await _itemsService.GetItemsAsync());
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet] // GET: items/{id}
        [Route("{id:int}")]
        public async Task<ActionResult> GetItem(int id)
        {
            var result = await _itemsService.GetItemAsync(id);
            return Ok(result);
        }
    }
}
