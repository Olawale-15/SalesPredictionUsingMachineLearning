using SalesPrediction.Entities;

namespace SalesPrediction.Interface.IJWTSettingsService
{
    public interface IJWTSettings
    {
        string GenerateToken(Trader trader);
       // Trader? GetTraderById(int id);
    }
}
