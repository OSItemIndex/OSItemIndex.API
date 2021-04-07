using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OSItemIndex.API.Data;
using OSItemIndex.API.Extensions;
using OSItemIndex.API.Models;
using OSItemIndex.API.Services;
using Serilog;

namespace OSItemIndex.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RealtimeController : ControllerBase
    {
        private readonly IRealtimePricesService _service;
        private readonly IDbContextHelper _dbContextHelper;

        public RealtimeController(IRealtimePricesService pricesService, IDbContextHelper dbContextHelper)
        {
            _service = pricesService;
            _dbContextHelper = dbContextHelper;
        }

        [Route("latest")]
        [HttpPost] // POST: realtime/latest
        [RequestSizeLimit(int.MaxValue)]
        public async Task<IActionResult> PostLatest(IEnumerable<RealtimePrice> prices)
        {
            return Ok(await _service.UpsertAndCommitPricesAsync(prices));
        }

        [Route("5m")]
        [HttpPost] // POST: realtime/5m
        [RequestSizeLimit(int.MaxValue)]
        public async Task<IActionResult> PostFiveMin(IEnumerable<RealtimePrice> prices)
        {
            /*var realtimePrices = prices.ToList();
            using (var factory = _dbContextHelper.GetFactory())
            {
                var dbContext = factory.GetDbContext();
                await dbContext.PricesRealtime.UpsertRangeAsync(realtimePrices, (a, b) => a.Id == b.Id);
                var r = await dbContext.SaveChangesAsync();
                return Ok();
            }*/
            return Ok(await _service.UpsertAndCommitPricesAsync(prices));
        }

        [Route("1h")]
        [HttpPost] // POST: realtime/1h
        [RequestSizeLimit(int.MaxValue)]
        public async Task<IActionResult> PostOneHour(IEnumerable<RealtimePrice> prices)
        {
            return Ok(await _service.UpsertAndCommitPricesAsync(prices));
        }
    }
}
