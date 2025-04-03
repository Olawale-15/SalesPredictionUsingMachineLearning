using SalesPrediction.Context;
using SalesPrediction.Entities;
using SalesPrediction.Interface.IRepository;

namespace SalesPrediction.Repository
{
    public class RoleRepository : IRoleRepository
    {
        public Role CreateRole(Role role)
        {
            ApplicationContext.RoleContext.Add(role);
            return role;
        }

        public Role? GetRoleName(string roleName)
        {
            var getRoleName = ApplicationContext.RoleContext.FirstOrDefault(x => x.RoleName == roleName);
            return getRoleName;
        }
    }
}
