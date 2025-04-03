using SalesPrediction.Entities;
using SalesPrediction.Response;

namespace SalesPrediction.Interface.IService
{
    public interface ITraderLoginService
    {
        BaseResponse<TraderResponseModel> Login(TraderLoginModel responseModel);
    }
}
