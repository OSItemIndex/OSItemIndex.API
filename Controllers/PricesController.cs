using Microsoft.AspNetCore.Mvc;
using OSItemIndex.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSItemIndex.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricesController : ControllerBase
    {
        private readonly IPricesService _service;

        public PricesController(IPricesService pricesService)
        {
            _service = pricesService;
        }
    }
}
