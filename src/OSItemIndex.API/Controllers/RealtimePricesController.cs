using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OSItemIndex.API.Models;
using OSItemIndex.Data;

namespace OSItemIndex.API.Controllers
{
    [ApiController]
    [Route("prices/realtime")]
    public class PricesRealtimeController : Controller
    {

        [HttpGet(Name = "GetPrices")]
        [Produces("application/json")]
        [ResponseCache(VaryByHeader = "Accept-Encoding", Duration = 30)]
        public async Task<ActionResult<IEnumerable<RealtimeItemPrice>>> GetPrices()
        {
            return Json(await _itemsService.GetItemsAsync(Request.QueryString.HasValue ? query : null));
        }

    }
}
