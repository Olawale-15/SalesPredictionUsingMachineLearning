using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesPrediction.Entities;
using SalesPrediction.Interface.IService;
using SalesPrediction.Response;
using SalesPrediction.Service;

namespace SalesPrediction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TraderLoginController : ControllerBase
    {
        private readonly ITraderLoginService _traderLoginService;

        public TraderLoginController(ITraderLoginService traderLoginService)
        {
            _traderLoginService = traderLoginService;
        }

        [HttpPost("Login")]
        public IActionResult Login(TraderLoginModel loginRequest)
        {
            var checkEmail = _traderLoginService.Login(loginRequest);
            if (!checkEmail.Status)
            {
                return BadRequest(new BaseResponse<TraderResponseModel>
                {
                    Message = checkEmail.Message,
                    Status = false
                });
            }

            return Ok(checkEmail.Data);
        }
    }
}
