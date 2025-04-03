using SalesPrediction.Entities;

namespace SalesPrediction.Interface.IRepository
{
    public interface ITraderRepository
    {
        Trader CreateTrader(Trader trader);
        bool DeleteTrader(Trader trader);
       // Trader? GetTraderById(int traderId);
        IList<Trader> GetAllTraders();
        //void Update(Trader updateTrader);
        Trader? GetByEmail(string email);
    }
}
