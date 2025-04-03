using SalesPrediction.Entities;

namespace SalesPrediction.Context
{
    public static class ApplicationContext
    {
        public static ICollection<Trader> TraderContext = new List<Trader>();
        public static ICollection<Role> RoleContext = new List<Role>();
    }
}
