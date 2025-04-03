using SalesPrediction.Context;
using SalesPrediction.Entities;
using SalesPrediction.Interface.IRepository;

namespace SalesPrediction.Repository
{
    public class TraderRepository : ITraderRepository
    {
        public Trader CreateTrader(Trader trader)
        {
            ApplicationContext.TraderContext.Add(trader);
            return trader;
        }

        public bool DeleteTrader(Trader trader)
        {
            ApplicationContext.TraderContext.Remove(trader);
            return true;
        }

        public IList<Trader> GetAllTraders()
        {
            var getAllTrader = ApplicationContext.TraderContext.ToList();
            return getAllTrader;

        }

        //public Trader? GetTraderById(int traderId)
        //{
        //    var getTraderById = ApplicationContext.TraderContext.FirstOrDefault(x => x.Id == traderId);
        //    return getTraderById;
        //}

        public Trader? GetByEmail(string email)
        {
            var getByEmail = ApplicationContext.TraderContext.FirstOrDefault(s => s.Email == email);
            return getByEmail;
        }


        //public void Update(Trader trader)
        //{
        //    var existingTrader = ApplicationContext.TraderContext.FirstOrDefault(x => x.Id == trader.Id);
        //    if(existingTrader != null)
        //    {
        //        existingTrader.FirstName = trader.FirstName;
        //        existingTrader.LastName = trader.LastName;
        //        existingTrader.PhoneNumber = trader.PhoneNumber;
        //        existingTrader.Email = trader.Email;
        //        existingTrader.City = trader.City;
        //        existingTrader.State = trader.State;
        //        existingTrader.Country = trader.Country;
        //        existingTrader.ZipCode = trader.ZipCode;
        //    }
        //    else
        //    {
        //        throw new KeyNotFoundException("Id not match");
        //    }
        //}
    }
}
