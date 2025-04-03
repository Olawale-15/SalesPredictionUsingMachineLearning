using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesPrediction.Entities;
using SalesPrediction.Interface.IService;
using System.Diagnostics;

namespace SalesPrediction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TraderController : ControllerBase
    {
        private readonly ITraderService _traderService;

        public TraderController(ITraderService traderService)
        {
            _traderService = traderService;
        }

        [HttpPost("Register")]
        public IActionResult Register(Trader traderRequest)
        {
            var createTrader = _traderService.CreateTrader(traderRequest);
            if (!createTrader.Status)
            {
                return BadRequest();
            }
            return Ok(createTrader.Message);
        }
    }
}
