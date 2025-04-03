using SalesPrediction.Entities;

namespace SalesPrediction.Interface.IRepository
{
    public interface IRoleRepository
    {
        Role CreateRole(Role role);
        Role? GetRoleName(string roleName);
    }
}
