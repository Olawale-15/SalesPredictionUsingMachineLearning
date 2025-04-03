using SalesPrediction.Entities;
using SalesPrediction.Response;

namespace SalesPrediction.Interface.IService
{
    public interface ITraderService
    {
        BaseResponse CreateTrader(Trader trader);
     //   BaseResponse DeleteTrader(int id);
        BaseResponse UpdateTrader(int id, Trader trader);
       // BaseResponse<Trader> GetTraderById(int id);
        BaseResponse<ICollection<Trader>> GetAllTrader();

    }
}
